namespace Othaura {

    using System.Collections.Generic;
    using UnityEngine;

    public class GameMap {

        /// <summary>
        /// Size of the map in tiles
        /// </summary>
        public Vector2i size;

        /// <summary>
        /// The tiles that make up the map
        /// </summary>
        public readonly Tile[,] terrain;

        public GameMap(Vector2i size) {

            this.size = size;
            terrain = new Tile[size.width, size.height];
            InitializeTiles();
        }

        private void InitializeTiles() {

            // Initialize the map to all "filled" tiles
            for (int x = 0; x < size.width; x++) {

                for (int y = 0; y < size.height; y++) {

                    SetTile(C.LAYER_TERRAIN, new Vector2i(x, y), new Tile(Tile.TEMPLATE_VOID));
                }
            }
        }

        public bool IsBlocked(Vector2i pos) {

            if (pos.x < 0 || pos.y < 0 || pos.x >= size.width || pos.y >= size.height) {
                
                return true;
            }

            var tile = RB.MapDataGet<Tile>(C.LAYER_TERRAIN, pos);
            //var tile = terrain[pos.x, pos.y];
            
            if (tile != null && tile.blocked) {

                return true;
            }

            return false;
        }

        public void MakeMap(int maxRooms, int roomMinSize, int roomMaxSize, int mapWidth, int mapHeight, Entity player, List<Entity> entities, int maxMonstersPerRoom) {

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

                    PlaceEntities(newRoom, entities, maxMonstersPerRoom);

                    // Add the new room to list of rooms
                    rooms.Add(newRoom);
                }
            }
            
            BitMasking.BeautifyMap(C.MAP_WIDTH, C.MAP_HEIGHT, C.LAYER_TERRAIN, rooms);
        }

        private void CreateRoom(Rect2i room) {

            for (int y = room.y + 1; y < room.max.y; y++) { 

                for (int x = room.x + 1; x < room.max.x; x++) {

                    SetTile(C.LAYER_TERRAIN, new Vector2i(x, y), new Tile(Tile.TEMPLATE_FLOOR_DIRT_BROWN));
                }
            }
        }

        private void CreateHTunnel(int x1, int x2, int y) {

            int xStart = System.Math.Min(x1, x2);
            int xEnd = System.Math.Max(x1, x2);

            for (int x = xStart; x <= xEnd; x++) {

                SetTile(C.LAYER_TERRAIN, new Vector2i(x, y), new Tile(Tile.TEMPLATE_FLOOR_DIRT_BROWN));
            }
        }

        private void CreateVTunnel(int y1, int y2, int x) {

            int yStart = System.Math.Min(y1, y2);
            int yEnd = System.Math.Max(y1, y2);

            for (int y = yStart; y <= yEnd; y++) {

                SetTile(C.LAYER_TERRAIN, new Vector2i(x, y), new Tile(Tile.TEMPLATE_FLOOR_DIRT_BROWN));
            }
        }

        public static void SetTile(int layer, Vector2i pos, Tile tile) {

            RB.MapDataSet<Tile>(layer, pos, tile);

            if (tile.sprite.id != S.EMPTY.id) {

                RB.MapSpriteSet(layer, pos, tile.sprite, tile.color);
            }

            else {
                
                RB.MapSpriteSet(layer, pos, RB.SPRITE_EMPTY);
            }
        }

        public static void SetTile(int layer, Vector2i pos, Tile tile, int rotation) {

            RB.MapDataSet<Tile>(layer, pos, tile);
            
            if (rotation == 90) {

                if (tile.sprite.id != S.EMPTY.id) {
                    RB.MapSpriteSet(layer, pos, tile.sprite, tile.color, RB.ROT_90_CW);
                }
                else {                    
                    RB.MapSpriteSet(layer, pos, RB.SPRITE_EMPTY);
                }
            } else if (rotation == 180) {
                if (tile.sprite.id != S.EMPTY.id) {
                    RB.MapSpriteSet(layer, pos, tile.sprite, tile.color, RB.ROT_180_CW);
                }
                else {                    
                    RB.MapSpriteSet(layer, pos, RB.SPRITE_EMPTY);
                }
            } else if (rotation == 270) {
                if (tile.sprite.id != S.EMPTY.id) {
                    RB.MapSpriteSet(layer, pos, tile.sprite, tile.color, RB.ROT_270_CW);
                }
                else {
                    RB.MapSpriteSet(layer, pos, RB.SPRITE_EMPTY);
                }
            } else {
                if (tile.sprite.id != S.EMPTY.id) {

                    RB.MapSpriteSet(layer, pos, tile.sprite, tile.color, RB.ROT_90_CW);
                }

                else {
                    
                    RB.MapSpriteSet(layer, pos, RB.SPRITE_EMPTY);
                }
            }        
        }

        private void PlaceEntities(Rect2i room, List<Entity> entities, int maxMonstersPerRoom) {
            
            var numOfMonsters = Random.Range(0, maxMonstersPerRoom + 1);

            for (int i = 0; i < numOfMonsters; i++) {

                var randomPos = new Vector2i(
                    Random.Range(room.min.x + 1, room.max.x),
                    Random.Range(room.min.y + 1, room.max.y));

                bool exists = false;

                for (int j = 0; j < entities.Count; j++) {

                    if (entities[j].pos == randomPos) {

                        exists = true;
                        break;
                    }
                }

                if (!exists) {

                    Entity monster;

                    if (Random.Range(0, 100) < 80) {

                        monster = new Entity(randomPos, S.RAT, new Color32(0xc0, 0x79, 0x58, 255), C.FSTR.Clear().Append("Rat"), true);
                    }

                    else {

                        monster = new Entity(randomPos, S.SKELETON, new Color32(0xdb, 0xd3, 0xc3, 255), C.FSTR.Clear().Append("Skeleton"), true);
                    }

                    entities.Add(monster);
                }
            }
        }       
    }
}


