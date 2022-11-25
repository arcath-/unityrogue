namespace Othaura {

    using System.Collections.Generic;

    /// <summary>
    /// Contains sprite ids of all sprites being used in the game. This is faster than passing sprite names
    /// by their string names every time.
    /// </summary>
    public static class S {
        
        /// <summary>
        /// Colored Hero 1 24x24
        /// </summary>
        public static PackedSpriteID HERO1 = RB.PackedSpriteID("hero1");

        /// <summary>
        /// Colored Hero 2 24x24
        /// </summary>
        public static PackedSpriteID HERO2 = RB.PackedSpriteID("hero2");

        /// <summary>
        /// Empty Space
        /// </summary>
        public static PackedSpriteID EMPTY = new PackedSpriteID(0);

        /// <summary>
        /// Cracked Brick wall
        /// </summary>
        public static PackedSpriteID WALL_BRICK01 = RB.PackedSpriteID("wallStoneLeftRight01");

        /// <summary>
        /// cobblestone floor
        /// </summary>
        public static PackedSpriteID FLOOR_COBBLESTONE01 = RB.PackedSpriteID("floorCobblestoneBrown01");


    }
}

