namespace Othaura {

    using System.Collections.Generic;
    using UnityEngine;

    public class GameMap {

        public Vector2i size;

        public GameMap(Vector2i size) {

            this.size = size;
            InitializeTiles();
        }

        private void InitializeTiles() {

            // Initialize the map to all "filled" tiles
            for (int x = 0; x < size.width; x++) {

                for (int y = 0; y < size.height; y++) {

                    SetTile(C.LAYER_TERRAIN, new Vector2i(x, y), new Tile(Tile.TEMPLATE_WALL_BRICK));
                }
            }
        }

        public bool IsBlocked(Vector2i pos) {

            var tile = RB.MapDataGet<Tile>(C.LAYER_TERRAIN, pos);
            if (tile != null && tile.blocked) {

                return true;
            }

            return false;
        }

        public void MakeMap(int maxRooms, int roomMinSize, int roomMaxSize, int mapWidth, int mapHeight, Entity player) {

            List<Rect2i> rooms = new List<Rect2i>();

            for (int r = 0; r < maxRooms; r++) {

                // Random width and height
                int width = Random.Range(roomMinSize, roomMaxSize + 1);
                int height = Random.Range(roomMinSize, roomMaxSize + 1);

                // Random position
                int x = Random.Range(0, mapWidth - width);
                int y = Random.Range(0, mapHeight - height);

                var newRoom = new Rect2i(x, y, width, height);

                // Check if the room intersects with other rooms
                bool intersects = false;
                for (int i = 0; i < rooms.Count; i++) {

                    if (newRoom.Intersects(rooms[i])) {

                        intersects = true;
                        break;
                    }
                }

                // No intersections, it's safe to create this room
                if (!intersects) {

                    CreateRoom(newRoom);

                    var roomCenter = newRoom.center;

                    if (rooms.Count == 0) {

                        // This is the first room, put the player in the center of it
                        player.pos = roomCenter;
                    }

                    else {

                        // All rooms after the first one should be connected with a tunnel to the previous room
                        var prevRoomCenter = rooms[rooms.Count - 1].center;

                        if (Random.Range(0, 2) == 1) {

                            CreateHTunnel(prevRoomCenter.x, roomCenter.x, prevRoomCenter.y);
                            CreateVTunnel(prevRoomCenter.y, roomCenter.y, roomCenter.x);
                        }

                        else {

                            CreateVTunnel(prevRoomCenter.y, roomCenter.y, prevRoomCenter.x);
                            CreateHTunnel(prevRoomCenter.x, roomCenter.x, roomCenter.y);
                        }
                    }

                    // Add the new room to list of rooms
                    rooms.Add(newRoom);
                }
            }
        }

        private void CreateRoom(Rect2i room) {

            for (int y = room.y + 1; y < room.max.y; y++) { 

                for (int x = room.x + 1; x < room.max.x; x++) {

                    SetTile(C.LAYER_TERRAIN, new Vector2i(x, y), new Tile(Tile.TEMPLATE_FLOOR_COBBLESTONE));
                }
            }
        }

        private void CreateHTunnel(int x1, int x2, int y) {

            int xStart = System.Math.Min(x1, x2);
            int xEnd = System.Math.Max(x1, x2);

            for (int x = xStart; x <= xEnd; x++) {

                SetTile(C.LAYER_TERRAIN, new Vector2i(x, y), new Tile(Tile.TEMPLATE_FLOOR_COBBLESTONE));
            }
        }

        private void CreateVTunnel(int y1, int y2, int x) {

            int yStart = System.Math.Min(y1, y2);
            int yEnd = System.Math.Max(y1, y2);

            for (int y = yStart; y <= yEnd; y++) {

                SetTile(C.LAYER_TERRAIN, new Vector2i(x, y), new Tile(Tile.TEMPLATE_FLOOR_COBBLESTONE));
            }
        }

        private void SetTile(int layer, Vector2i pos, Tile tile) {

            RB.MapDataSet<Tile>(layer, pos, tile);

            if (tile.sprite.id != S.EMPTY.id) {

                RB.MapSpriteSet(layer, pos, tile.sprite, tile.color);
            }

            else {
                
                RB.MapSpriteSet(layer, pos, RB.SPRITE_EMPTY);
            }
        }


    }
}


