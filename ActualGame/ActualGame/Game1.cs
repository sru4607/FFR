using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ActualGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    enum MainGameState {Debug, Menu, Pause, Quit, InGame, GameOver }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Enemy testEnemy; // Unused - kept for example documentation
        Display mainDisplay;
        World levelOne;
        MainGameState currentState;
        Dictionary<string, Texture2D> allTextures;
        Debug debugger;
        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initialize an array of textures for use
            allTextures = new Dictionary<string, Texture2D>();

            // Generic enemy used to test bugs/features
            // testEnemy = new Enemy();

            // Base game logic
            mainDisplay = new Display(GraphicsDevice);
            base.Initialize();

            // Set the initial state of the game
            currentState = MainGameState.Debug;

            // Values used for debugging purposes
            debugger = new Debug(allTextures);
            debugger.InstantiateAll();

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            levelOne = new World("Level One", "level1.txt");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Import levels
            //levelOne.Import();

            // Add all necessary textures to the dictionary
            allTextures.Add("Floor", Content.Load<Texture2D>("missingtexture"));
            allTextures.Add("PenPen", Content.Load<Texture2D>("PenPen"));
            allTextures.Add("missingtexture", Content.Load<Texture2D>("missingtexture"));
            allTextures.Add("Enemy", Content.Load<Texture2D>("missingtexture"));

            // Sync in-game objects with their dictionary textures
            // EX: testEnemy.LoadTexture(allTextures["missingtexture"]);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            debugger.UpdateAll();
            // TODO: Add your update logic here
            switch (currentState)
            {
                case (MainGameState.Debug):
                    {
                        debugger.UpdateAll(gameTime);
                        break;
                        //testEnemy.Update();
                    }
                case (MainGameState.InGame):
                    {

                        break;
                    }
                case (MainGameState.Menu):
                    {

                        break;
                    }
                case (MainGameState.Pause):
                    {

                        break;
                    }
                case (MainGameState.Quit):
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Matrix temp = mainDisplay.MainCam.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: temp);
            switch(currentState)
            {
                case (MainGameState.Debug):
                {
                    debugger.Draw(spriteBatch);
                    break;
                }
                case (MainGameState.InGame):
                {
                        
                    break;
                }
                case (MainGameState.Menu):
                {

                    break;
                }
                case (MainGameState.Pause):
                {

                    break;
                }
                case (MainGameState.Quit):
                {
                    break;
                }
                default:
                {
                    break;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
