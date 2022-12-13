namespace Othaura {

    using System.Collections.Generic;

    /// <summary>
    /// Contains sprite ids of all sprites being used in the game. This is faster than passing sprite names
    /// by their string names every time.
    /// </summary>
    public static class S {
        
        /// <summary>
        /// Hero 16x16
        /// </summary>
        public static PackedSpriteID HERO1 = RB.PackedSpriteID("warrior_m_1");

        //Monsters
        /// <summary>
        /// Rat 24x24
        /// </summary>
        public static PackedSpriteID RAT = RB.PackedSpriteID("rat_1");

        /// <summary>
        /// skeleton 24x24
        /// </summary>
        public static PackedSpriteID SKELETON = RB.PackedSpriteID("skeleton_1");

        /// <summary>
        /// Empty Space
        /// </summary>
        public static PackedSpriteID EMPTY = new PackedSpriteID(0);

        /// <summary>
        /// Void Space
        /// </summary>
        public static PackedSpriteID VOID = RB.PackedSpriteID("voidSpace01");
        //public static PackedSpriteID VOID = RB.PackedSpriteID("Grey01/GreyWall01");

        /// <summary>
        /// Fog
        /// </summary>
        public static PackedSpriteID FOG = RB.PackedSpriteID("fog");

        /// <summary>
        /// 
        /// </summary>
        public static PackedSpriteID WALL_CRYPT_NORTH = RB.PackedSpriteID("crypt/tile017");

        /// <summary>
        /// 
        /// </summary>
        public static PackedSpriteID WALL_CRYPT_EAST = RB.PackedSpriteID("crypt/tile009"); 

        /// <summary>
        /// 
        /// </summary>
        public static PackedSpriteID WALL_CRYPT_WEST = RB.PackedSpriteID("crypt/tile009");
        /// <summary>
        /// 
        /// </summary>
        public static PackedSpriteID WALL_CRYPT_SOUTH = RB.PackedSpriteID("crypt/tile017");

              

        /// <summary>
        /// dirt floor
        /// </summary>
        public static PackedSpriteID FLOOR_DIRT_BROWN = RB.PackedSpriteID("ground_dirt_brown");

        
    }
}

