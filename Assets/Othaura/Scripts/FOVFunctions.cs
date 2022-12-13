namespace Othaura {

    using UnityEngine;
    using System.Collections.Generic;

    public static class FOVFunctions {

        public static void InitializeFOV(GameMap map) {

            for (int x = 0; x < map.size.width; x++) {

                for (int y = 0; y < map.size.height; y++) {

                    RB.MapDataSet<FOVTile>(C.LAYER_VISIBILITY, new Vector2i(x, y), new FOVTile());
                }
            }
        }

        public static void RecomputeFov(GameMap map, Entity origin, int radius) {

            // Turn all tiles invisible first
            for (int x = 0; x < map.size.width; x++) {

                for (int y = 0; y < map.size.height; y++) {

                    var tile = RB.MapDataGet<FOVTile>(C.LAYER_VISIBILITY, new Vector2i(x, y));
                    tile.visible = false;
                }
            }

            // Turn the tile at origin visible
            RB.MapDataGet<FOVTile>(C.LAYER_VISIBILITY, origin.pos).visible = true;

            foreach (var dir in Direction.Diagonal) {

                CastLight(map, origin.pos, radius, 1, 1.0f, 0.0f, 0, dir.x, dir.y, 0);
                CastLight(map, origin.pos, radius, 1, 1.0f, 0.0f, dir.x, 0, 0, dir.y);
            }

            UpdateTilemap(map);
        }

        public static bool IsInFOV(Vector2i pos) {

            var fovTile = RB.MapDataGet<FOVTile>(C.LAYER_VISIBILITY, pos);
            return fovTile.visible;
        }

        private static void CastLight(GameMap map, Vector2i startPos, float radius, int row, float start, float end, int xx, int xy, int yx, int yy) {

            var newStart = 0.0f;
            if (start < end) {

                return;
            }

            var maxWidth = map.size.width;
            var maxHeight = map.size.height;

            var blocked = false;

            int distance;
            for (distance = row; distance <= radius && !blocked; distance++) {

                Vector2i delta;
                delta.y = -distance;
                for (delta.x = -distance; delta.x <= 0; delta.x++) {

                    Vector2i currentPos = new Vector2i(startPos.x + delta.x * xx + delta.y * xy, startPos.y + delta.x * yx + delta.y * yy);

                    var leftSlope = (delta.x - 0.5f) / (delta.y + 0.5f);
                    var rightSlope = (delta.x + 0.5f) / (delta.y - 0.5f);

                    if (!(currentPos.x >= 0 && currentPos.y >= 0 && currentPos.x < maxWidth && currentPos.y < maxHeight) || start < rightSlope) {

                        continue;
                    }
                    else if (end > leftSlope) {
                        break;
                    }

                    var currentFOVTile = RB.MapDataGet<FOVTile>(C.LAYER_VISIBILITY, currentPos);
                    var currentTile = RB.MapDataGet<Tile>(C.LAYER_TERRAIN, currentPos);

                    currentFOVTile.visible = true;
                    currentFOVTile.explored = true;

                    if (blocked) { 

                        // Previous cell was a blocking one
                        if (currentTile.blockSight) {

                            // Hit a wall
                            newStart = rightSlope;
                            continue;
                        }
                        else {
                            blocked = false;
                            start = newStart;
                        }
                    }
                    
                    else {
                        
                        if (currentTile.blockSight && distance < radius) {

                            // Hit a wall within sight line
                            blocked = true;
                            CastLight(map, startPos, radius, distance + 1, start, leftSlope, xx, xy, yx, yy);
                            newStart = rightSlope;
                        }
                    }
                }
            }
        }

        private static void UpdateTilemap(GameMap map) {

            var exploredColor = C.COLOR_INVISIBLE;
            exploredColor.a = 200;

            Vector2i pos = new Vector2i(0, 0);
            for (pos.x = 0; pos.x < map.size.width; pos.x++) {

                for (pos.y = 0; pos.y < map.size.height; pos.y++) {

                    var currentFOVTile = RB.MapDataGet<FOVTile>(C.LAYER_VISIBILITY, pos);

                    if (currentFOVTile.visible) {

                        RB.MapSpriteSet(C.LAYER_VISIBILITY, pos, RB.SPRITE_EMPTY);
                    }
                    else {

                        if (currentFOVTile.explored) {

                            RB.MapSpriteSet(C.LAYER_VISIBILITY, pos, S.FOG, exploredColor);
                        }
                        else {
                            
                            RB.MapSpriteSet(C.LAYER_VISIBILITY, pos, S.FOG, C.COLOR_INVISIBLE);
                        }
                    }
                }
            }
        }
    }
}
