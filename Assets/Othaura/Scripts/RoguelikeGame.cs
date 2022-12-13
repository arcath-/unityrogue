namespace Othaura {

    //using System;
    //using System.Collections;
    using System.Collections.Generic;
    
    using UnityEngine;
    

    /// <summary>
    /// Othaura
    /// </summary>
    public class RoguelikeGame : RB.IRetroBlitGame {

        public Assets assets = new Assets();

        private const int ROOM_MIN_SIZE = 6;
        private const int ROOM_MAX_SIZE = 10;
        private const int MAX_ROOMS = 30;
        private const int MAX_MONSTERS_PER_ROOM = 3;

        private Entity mPlayer;
        private Entity mNPC;
        private List<Entity> mEntities;

        private int mFOVRadius;
        private bool mFOVRecompute = true;

        private GameMap mGameMap; 

        //private Console mConsole;

        private Camera mCamera; 

        private GameState mGameState;

        private enum GameState
        {
            PLAYER_TURN,
            ENEMY_TURN
        }

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

            Console.Initialize(new Vector2i(RB.DisplaySize.width / 2, RB.DisplaySize.height / 3));
            //mConsole = new Console(new Vector2i(RB.DisplaySize.width / 2, RB.DisplaySize.height / 3));

             // Set FOV radius to the whole screen
            mFOVRadius = C.SCREEN_WIDTH / 2 + 2;

            //why are these /2?
            mPlayer = new Entity(new Vector2i (C.SCREEN_WIDTH/2, C.SCREEN_HEIGHT/2), S.HERO1, Color.white,C.FSTR.Clear().Append("Player"));
            
            mEntities = new List<Entity>();
            mEntities.Add(mPlayer);
            
            // make the map
            mGameMap = new GameMap(new Vector2i(C.MAP_WIDTH, C.MAP_HEIGHT));
            mGameMap.MakeMap(MAX_ROOMS, ROOM_MIN_SIZE, ROOM_MAX_SIZE, C.MAP_WIDTH, C.MAP_HEIGHT, mPlayer, mEntities, MAX_MONSTERS_PER_ROOM);
            //BitMasking.BeautifyMap(mGameMap);
            
            //FOV init
            FOVFunctions.InitializeFOV(mGameMap);
            mFOVRecompute = true;

            // set the camera 
            mCamera = new Camera();
            mCamera.SetPos(mPlayer);

            //Setting the map sprite sheets according to layer.   
            RB.MapLayerSpriteSheetSet(C.LAYER_TERRAIN, assets.spriteSheet);
            RB.MapLayerSpriteSheetSet(C.LAYER_VISIBILITY, assets.spriteSheet); 
            
            // Collect any garbage created during initilization to avoid a performance hiccup later.
            System.GC.Collect();

            mGameState = GameState.PLAYER_TURN;

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

            if (mGameState == GameState.PLAYER_TURN)
            {
                if (DoPlayerTurn())
                {
                    mGameState = GameState.ENEMY_TURN;
                }
            }
            else
            {
                DoEnemyTurn();
                mGameState = GameState.PLAYER_TURN;
            }
            

            mCamera.Follow(mPlayer);

            if (mFOVRecompute) {

                FOVFunctions.RecomputeFov(mGameMap, mPlayer, mFOVRadius);
                mFOVRecompute = false;
            }
        }

        /// <summary>
        /// Do players turn, return true is player moved
        /// </summary>
        /// <returns>True if moved</returns>
        public bool DoPlayerTurn() {

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

            // Only move if way is clear, may cause an enemy turn if you run into a wall
            // TODO: test this
            if ((delta.x != 0 || delta.y != 0) && !mGameMap.IsBlocked(mPlayer.pos + delta)) {

                var destPos = mPlayer.pos + delta;
                var targetEntity = EntityFunctions.GetBlockingEntityAtPos(mEntities, destPos);

                if (targetEntity != null) {

                    Console.Log(C.FSTR.Clear().Append("You kick the @FFFF50").Append(targetEntity.name)
                    .Append("@- in the shins, much to its annoyance!"));
                }

                else {

                    mPlayer.Move(delta);
                    mFOVRecompute = true;
                }
                return true;

            }
            return false;
        }

        /// <summary>
        /// Do enemy turn...
        /// </summary>
        public void DoEnemyTurn() {

            for (int i = 0; i < mEntities.Count; i++) {

                var entity = mEntities[i];
                if (entity != mPlayer) {

                    if (Random.Range(0, 200 + 1) > 199) {
                        Console.Log(C.FSTR.Clear().Append("The @FFFF50").Append(entity.name)
                        .Append("@- ponders the meaning of its existence."));
                    }
                }
            }
        }



        /// <summary>
        /// Render, your drawing code should go here.
        /// </summary>
        public void Render() {

            RB.Clear(C.COLOR_BACKGROUND);
                     
            mCamera.Apply();
            RenderFunctions.RenderAll(mGameMap, mEntities);

            RB.CameraReset();
            Console.Render();
            
        }
    }
}