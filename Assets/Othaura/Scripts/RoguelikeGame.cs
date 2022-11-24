namespace Othaura {

    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    /// <summary>
    /// Othaura
    /// </summary>
    public class RoguelikeGame : RB.IRetroBlitGame {

        private Vector2i mPlayerPos;        
        public Assets assets = new Assets();

        /// <summary>
        /// Query hardware. Here you initialize your retro game hardware.
        /// </summary>
        /// <returns>Hardware settings</returns>
        public RB.HardwareSettings QueryHardware() {

            var hw = new RB.HardwareSettings();
            
            // Set your display size
            hw.DisplaySize = new Vector2i(1280/2, 720/2);

            // Set tilemap maximum size, default is 256, 256. Keep this close to your minimum required size to save on memory
            //// MapSize = new Vector2i(256, 256)

            // Set tilemap maximum layers, default is 8. Keep this close to your minimum required size to save on memory
            //// MapLayers = 8       

            return hw;
        }

        /// <summary>
        /// Initialize your game here.
        /// </summary>
        /// <returns>Return true if successful</returns>
        public bool Initialize() {    

            assets.LoadAll();      

            C.SCREEN_WIDTH = RB.DisplaySize.width / assets.spriteSheet.grid.cellSize.width;
            C.SCREEN_HEIGHT = RB.DisplaySize.height / assets.spriteSheet.grid.cellSize.height;

            mPlayerPos = new Vector2i(C.SCREEN_WIDTH / 2, C.SCREEN_HEIGHT / 2);

            // Collect any garbage created during initilization to avoid a performance hiccup later.
            System.GC.Collect();

            return true;

        }

        /// <summary>
        /// Update, your game logic should live here. Update is called at a fixed interval of 60 times per second.
        /// </summary>
        ///
        public void Update() {

            if (RB.KeyDown(KeyCode.Escape)) {
                Application.Quit();
            }

            if (RB.KeyDown(KeyCode.W) || RB.KeyDown(KeyCode.Keypad8)) {
                mPlayerPos.y--;
            }

            else if (RB.KeyDown(KeyCode.S) || RB.KeyDown(KeyCode.Keypad2)) {
                mPlayerPos.y++;
            }

            else if (RB.KeyDown(KeyCode.A) || RB.KeyDown(KeyCode.Keypad4)) {
                mPlayerPos.x--;
            }

            else if (RB.KeyDown(KeyCode.D) || RB.KeyDown(KeyCode.Keypad6)) {
                mPlayerPos.x++;
            }
        }

        /// <summary>
        /// Render, your drawing code should go here.
        /// </summary>
        public void Render() {

            RB.Clear(new Color32(0x47, 0x2d, 0x3c, 255));

            //RB.DrawSprite(Sprite.HERO, new Vector2i(mPlayerPos.x * RB.SpriteSize(0).width, mPlayerPos.y * RB.SpriteSize(0).width));
           
            //draw character
            //var position = new Vector2i(mPlayerPos.x * assets.spriteSheet.grid.cellSize.width, mPlayerPos.y * assets.spriteSheet.grid.cellSize.height);
            var position = new Vector2i(mPlayerPos.x * 6, mPlayerPos.y * 6);
            
            //draw the sprite
            RB.DrawSprite(S.HERO2, position);
            
        }
    }
}