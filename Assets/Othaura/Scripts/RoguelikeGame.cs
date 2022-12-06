namespace Othaura {

    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    /// <summary>
    /// Othaura
    /// </summary>
    public class RoguelikeGame : RB.IRetroBlitGame {

        public Assets assets = new Assets();

        private Entity mPlayer;
        private Entity mNPC;
        private List<Entity> mEntities;

        private GameMap mGameMap; 

        private Camera mCamera;       

        // refactor these into C.cs
        private const int ROOM_MIN_SIZE = 6;
        private const int ROOM_MAX_SIZE = 10;
        private const int MAX_ROOMS = 30;


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

            // You can load a spritesheet here
            RB.SpriteSheetSet(assets.spriteSheet);

            var rand = new System.Random();
            UnityEngine.Random.InitState(rand.Next());

            C.SCREEN_WIDTH = RB.DisplaySize.width / assets.spriteSheet.grid.cellSize.width;
            C.SCREEN_HEIGHT = RB.DisplaySize.height / assets.spriteSheet.grid.cellSize.height;

            //why are these /2?
            mPlayer = new Entity(new Vector2i (C.SCREEN_WIDTH/2, C.SCREEN_HEIGHT/2), S.HERO1, Color.white);
            mNPC = new Entity(new Vector2i (C.SCREEN_WIDTH/2, C.SCREEN_HEIGHT/2), S.HERO2, Color.white);

            mEntities = new List<Entity>();
            mEntities.Add(mPlayer);
            mEntities.Add(mNPC);

            // make the map
            mGameMap = new GameMap(new Vector2i(C.MAP_WIDTH, C.MAP_HEIGHT));
            mGameMap.MakeMap(MAX_ROOMS, ROOM_MIN_SIZE, ROOM_MAX_SIZE, C.MAP_WIDTH, C.MAP_HEIGHT, mPlayer);
            

            // set the camera 
            mCamera = new Camera();
            mCamera.SetPos(mPlayer);

            // Temporarily place enemy somewhere valid
            int attempts = 1000;
            while (attempts > 0)
            {
                var pos = new Vector2i(UnityEngine.Random.Range(0, C.MAP_WIDTH), UnityEngine.Random.Range(0, C.MAP_HEIGHT));
                var tile = RB.MapDataGet<Tile>(C.LAYER_TERRAIN, pos);
                if (tile != null && tile.blocked == false && pos != mPlayer.pos)
                {
                    mNPC.pos = pos;
                    break;
                }

                attempts--;
            }


            //Setting the map sprite sheets according to layer.   
            RB.MapLayerSpriteSheetSet(C.LAYER_TERRAIN, assets.spriteSheet); 
            
            // Collect any garbage created during initilization to avoid a performance hiccup later.
            System.GC.Collect();

            return true;

        }

        /// <summary>
        /// Update, your game logic should live here. Update is called at a fixed interval of 60 times per second.
        /// </summary>
        ///
        public void Update() {            
            
            if (RB.KeyPressed(KeyCode.Escape)) {
                Application.Quit();
            }

            Vector2i delta = Vector2i.zero;

            //Player movement
            if (RB.KeyPressed(KeyCode.W) || RB.KeyPressed(KeyCode.Keypad8)) {
                delta.y--;
            }

            else if (RB.KeyPressed(KeyCode.S) || RB.KeyPressed(KeyCode.Keypad2)) {
                delta.y++;
            }

            else if (RB.KeyPressed(KeyCode.A) || RB.KeyPressed(KeyCode.Keypad4)) {
                delta.x--;
            }

            else if (RB.KeyPressed(KeyCode.D) || RB.KeyPressed(KeyCode.Keypad6)) {
                delta.x++;
            }

            else if (RB.KeyPressed(KeyCode.Q) || RB.KeyPressed(KeyCode.Keypad7)) {
                delta.x--;
                delta.y--;
            }

            else if (RB.KeyPressed(KeyCode.E) || RB.KeyPressed(KeyCode.Keypad9)) {
                delta.x++;
                delta.y--;
            }

            else if (RB.KeyPressed(KeyCode.Z) || RB.KeyPressed(KeyCode.Keypad1)) {
                delta.x--;
                delta.y++;
            }

            else if (RB.KeyPressed(KeyCode.C) || RB.KeyPressed(KeyCode.Keypad3)) {
                delta.x++;
                delta.y++;
            }

            // Only move if way is clear
            if (!mGameMap.IsBlocked(mPlayer.pos + delta))
            {
                mPlayer.Move(delta);
            }

            mCamera.Follow(mPlayer);


        }

        /// <summary>
        /// Render, your drawing code should go here.
        /// </summary>
        public void Render() {

            RB.Clear(new Color32(0x47, 0x2d, 0x3c, 255));
                     
            mCamera.Apply();
            RenderFunctions.RenderAll(mEntities);
            
        }
    }
}