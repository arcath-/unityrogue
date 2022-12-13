namespace Othaura {

    using System.Collections.Generic;
    using UnityEngine;

    public class BitMasking {
        
        public Vector2i size;

        
        public static void BeautifyMap(int mapWidth, int mapHeight, int terrainLayer, List<Rect2i> map ) {

            Vector2i pos = new Vector2i(0, 0);
            
            for (pos.x = 0; pos.x < mapWidth; pos.x++) {

                for (pos.y = 0; pos.y < mapHeight; pos.y++){

                    var tile = RB.MapDataGet<Tile>(terrainLayer, pos );
                    if (tile.type == Tile.Type.FLOOR) {
                                               
                        //GameMap.SetTile(terrainLayer, new Vector2i(pos.x, pos.y), new Tile(Tile.TEMPLATE_EMPTY));                        

                    
                        bool N = IsBlocked( (new Vector2i(pos.x, pos.y -1) ) );
                        bool S = IsBlocked( (new Vector2i(pos.x, pos.y +1) ) );
                        bool E = IsBlocked( (new Vector2i(pos.x +1, pos.y) ) );
                        bool W = IsBlocked( (new Vector2i(pos.x -1, pos.y) ) );

                        // corners also need the asc cardinals.
                        bool NW = IsBlocked( (new Vector2i(pos.x -1, pos.y -1) ) ) &&W &&N;
                        bool SW = IsBlocked( (new Vector2i(pos.x -1, pos.y +1) ) ) &&S &&W;
                        bool NE = IsBlocked( (new Vector2i(pos.x +1, pos.y -1) ) ) &&N &&E;
                        bool SE = IsBlocked( (new Vector2i(pos.x +1, pos.y +1) ) ) &&S &&E;

                        // Checking to see what is actually blocked from the tile and going clockwise starting with 12 oclock. 
                        // North        = 2^0 = 1
                        // North East   = 2^1 = 2
                        // East         = 2^2 = 4
                        // South East   = 2^3 = 8
                        // South        = 2^4 = 16
                        // South West   = 2^5 = 32
                        // West         = 2^6 = 64
                        // North West   = 2^7 = 128

                        int value = N   ? 1 : 0;
                        value += NE     ? 2 : 0;
                        value += E      ? 4 : 0;
                        value += SE     ? 8 : 0;
                        value += S      ? 16 : 0;
                        value += SW     ? 32 : 0;
                        value += W      ? 64 : 0;
                        value += NW     ? 128 : 0;

                        switch(value) {

                            case 1:
                                GameMap.SetTile(terrainLayer, new Vector2i(pos.x, pos.y-1), new Tile(Tile.TEMPLATE_WALL_CRYPT_NORTH));
                                break;                            
                            case 4:
                                GameMap.SetTile(terrainLayer, new Vector2i(pos.x+1, pos.y), new Tile(Tile.TEMPLATE_WALL_CRYPT_EAST));
                                break;
                            case 16:
                                GameMap.SetTile(terrainLayer, new Vector2i(pos.x, pos.y+1), new Tile(Tile.TEMPLATE_WALL_CRYPT_SOUTH));
                                break;
                            case 64:
                                GameMap.SetTile(terrainLayer, new Vector2i(pos.x-1, pos.y), new Tile(Tile.TEMPLATE_WALL_CRYPT_WEST));
                                break;
                            
                            default:
                                break;


                        }
                        
                    

                        
                        
                    
                        

                    }

                }
            }

        }

        // Is the tile blocked or not? I don't know, why are you asking me?
        public static bool IsBlocked(Vector2i pos) {
            
            var tile = RB.MapDataGet<Tile>(C.LAYER_TERRAIN, pos);
                        
            if (tile != null && tile.blocked) {

                return true;
            }

            return false;
        }
    }
}
