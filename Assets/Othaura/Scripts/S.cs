namespace Othaura {

    using System.Collections.Generic;

    /// <summary>
    /// Contains sprite ids of all sprites being used in the game. This is faster than passing sprite names
    /// by their string names every time.
    /// </summary>
    public static class S {
        
        /// <summary>
        /// Hero 1
        /// </summary>
        public static PackedSpriteID HERO1 = RB.PackedSpriteID("hero1");

        /// <summary>
        /// Hero 2
        /// </summary>
        public static PackedSprite HERO2 = RB.PackedSpriteGet("hero2");
    }
}

