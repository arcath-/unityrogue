namespace Othaura {

    using UnityEngine;

    public class Tile {

        /// <summary>
        /// Tile type
        /// </summary>
        public Type type;
        
        /// <summary>
        /// Whether it blocks movement
        /// </summary>
        public bool blocked;
        
        /// <summary>
        /// Whether it blocks sight
        /// </summary>
        public bool blockSight;

        /// <summary>
        /// Sprite id to use when rendering
        /// </summary>
        public PackedSpriteID sprite;

        /// <summary>
        /// Color to use when rendering
        /// </summary>
        public Color32 color;

        /// <summary>
        /// Tile type
        /// </summary>
        public enum Type {
            
            /// <summary>
            /// Empty tile
            /// </summary>
            EMPTY = 0,

            /// <summary>
            /// Void tile
            /// </summary>
            VOID,

            /// <summary>
            /// Wall tile
            /// </summary>
            WALL,

            /// <summary>
            /// Floor tile
            /// </summary>
            FLOOR,

            
        }

        public static readonly Tile TEMPLATE_EMPTY = new Tile(Type.EMPTY, S.EMPTY, Color.grey, false);
        public static readonly Tile TEMPLATE_VOID = new Tile(Type.VOID, S.VOID, Color.white, true);

        // Wolf Grey Dungeon Walls
        public static readonly Tile TEMPLATE_WALL_CRYPT_NS = new Tile(Type.WALL, S.WALL_CRYPT_NS, Color.white, true);
        public static readonly Tile TEMPLATE_WALL_CRYPT_EW = new Tile(Type.WALL, S.WALL_CRYPT_EW, Color.white, true);
        public static readonly Tile TEMPLATE_WALL_CRYPT_TC = new Tile(Type.WALL, S.WALL_CRYPT_TC, Color.white, true);
        public static readonly Tile TEMPLATE_WALL_CRYPT_BC = new Tile(Type.WALL, S.WALL_CRYPT_BC, Color.white, true);

        // Test Walls
        public static readonly Tile TEMPLATE_WALL_TEST_N = new Tile(Type.WALL, S.WALL_TEST_N, Color.white, true);
        public static readonly Tile TEMPLATE_WALL_TEST_S = new Tile(Type.WALL, S.WALL_TEST_S, Color.white, true);
        public static readonly Tile TEMPLATE_WALL_TEST_E = new Tile(Type.WALL, S.WALL_TEST_E, Color.white, true);
        public static readonly Tile TEMPLATE_WALL_TEST_W = new Tile(Type.WALL, S.WALL_TEST_W, Color.white, true);
    
        //Floors
        public static readonly Tile TEMPLATE_FLOOR_DIRT_BROWN = new Tile(Type.FLOOR, S.FLOOR_DIRT_BROWN, Color.white, false);

        
        /// <summary>
        /// Constructor
        /// </summary>
        public Tile()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="template">Template tile to copy</param>
        public Tile(Tile template) {

            type = template.type;
            blocked = template.blocked;
            blockSight = template.blockSight;

            sprite = template.sprite;
            color = template.color;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Tile type</param>
        /// <param name="sprite">Sprite to use when rendering this tile</param>
        /// <param name="color">Color of the tile</param>
        /// <param name="blocked">Whether the tile is blocking</param>
        public Tile(Type type, PackedSpriteID sprite, Color32 color, bool blocked) {
            
            this.type = type;
            this.blocked = blocked;
            this.blockSight = blocked;

            this.sprite = sprite;
            this.color = color;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">Sprite to use when rendering this tile</param>
        /// <param name="color">Color of the tile</param>
        /// <param name="blocked">Whether the tile is blocking</param>
        /// <param name="blockSight">Whether the tile blocks sight (field of view)</param>
        public Tile(PackedSpriteID sprite, Color32 color, bool blocked, bool blockSight) {
            
            this.blocked = blocked;
            this.blockSight = blockSight;

            this.sprite = sprite;
            this.color = color;
        }
    }
}


