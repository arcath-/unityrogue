namespace Othaura {
    
    /// <summary>
    /// Contains all game assets
    /// </summary>
    public class Assets {

        /// <summary>
        /// Sprite sheet
        /// </summary>
        public SpriteSheetAsset spriteSheet = new SpriteSheetAsset();

        /// <summary>
        /// Load all assets
        /// </summary>
        public void LoadAll() {

            spriteSheet.Load("SpritePack", SpriteSheetAsset.SheetType.SpritePack);
            //spriteSheet.grid = new SpriteGrid(new Vector2i(24, 24));

            RB.SpriteSheetSet(spriteSheet);            
        }
    }
}

