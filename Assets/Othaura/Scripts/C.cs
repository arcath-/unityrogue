namespace Othaura {

    using UnityEngine;

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
        //80

        /// <summary>
        /// Map Height
        /// </summary>
        public static int MAP_HEIGHT = 45;
        //45

        /// <summary>
        /// Terrain Layer
        /// </summary>
        public static int LAYER_TERRAIN = 0;

        /// <summary>
        /// Terrain Layer
        /// </summary>
        public static int LAYER_VISIBILITY = 1;

        public static Color32 COLOR_BACKGROUND = new Color32(0x47, 0x2d, 0x3c, 255);
        public static Color32 COLOR_INVISIBLE = new Color32(0x47 / 2, 0x2d / 2, 0x3c / 2, 255);

        public static FastString FSTR = new FastString(8192);

        /// <summary>
        /// old values were calling RB.SpriteSize(0).width and .height but not sure why. changed to static 24 for pixel size
        /// </summary>
        public static int PIXEL_SIZE = 48;

    }
}

