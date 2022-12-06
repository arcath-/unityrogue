namespace Othaura {

    /// <summary>
    /// Collection of global constants and statics
    /// </summary>
    public static class C {
        
        /// <summary>
        /// Screen Width
        /// </summary>
        public static int SCREEN_WIDTH;

        /// <summary>
        /// Screen Height
        /// </summary>
        public static int SCREEN_HEIGHT;

        /// <summary>
        /// Map Width
        /// </summary>
        public static int MAP_WIDTH = 80;

        /// <summary>
        /// Map Height
        /// </summary>
        public static int MAP_HEIGHT = 45;

        /// <summary>
        /// Terrain Layer
        /// </summary>
        public static int LAYER_TERRAIN = 1;

        /// <summary>
        /// old values were calling RB.SpriteSize(0).width and .height but not sure why. changed to static 6
        /// </summary>
        public static int MOVE_MULTIPLE = 24;

    }
}

