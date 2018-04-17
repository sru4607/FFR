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
        Dictionary<string, World> maps;
        World currentWorld;
        Button[] buttons;
        int indexActiveButton;
        KeyboardState kbState;
        KeyboardState prevkbState;
        
        
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
            currentState = MainGameState.Menu ;

            // Values used for debugging purposes
            debugger = new Debug(allTextures, GraphicsDevice);
            debugger.InstantiateAll();

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            kbState = Keyboard.GetState();
            prevkbState = kbState;
            // Initialize the maps list
            maps = new Dictionary<string, World>();
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
            allTextures.Add("Menu", Content.Load<Texture2D>("Menu"));
            allTextures.Add("StartButton", Content.Load<Texture2D>("StartButton"));
            allTextures.Add("ExitButton", Content.Load<Texture2D>("ExitButton"));
            allTextures.Add("PauseMenu", Content.Load<Texture2D>("PauseMenu"));
            allTextures.Add("ResumeButton", Content.Load<Texture2D>("ResumeButton"));
            allTextures.Add("MainMenuButton", Content.Load<Texture2D>("MainMenuButton"));

            // Load tiles systemmatically
            // BrickWall
            for (int i = 0; i < 16; i++)
                allTextures.Add("BrickWall.png" + i, Content.Load<Texture2D>("Tiles/BrickWall" + i));

            // BrickWallBlue
            for (int i = 0; i < 16; i++)
                allTextures.Add("BrickWallBlue.png" + i, Content.Load<Texture2D>("Tiles/BrickWallBlue" + i));

            // BrickWallRed
            for (int i = 0; i < 16; i++)
                allTextures.Add("BrickWallRed.png" + i, Content.Load<Texture2D>("Tiles/BrickWallRed" + i));

            // holder
            for (int i = 0; i < 16; i++)
                allTextures.Add("holder.png" + i, Content.Load<Texture2D>("Tiles/holder" + i));

            // temp
            for (int i = 0; i < 16; i++)
                allTextures.Add("temp.png" + i, Content.Load<Texture2D>("Tiles/temp" + i));

            // Walls
            for (int i = 0; i < 16; i++)
                allTextures.Add("Walls.png" + i, Content.Load<Texture2D>("Tiles/Walls" + i));

            // WallsGreen
            for (int i = 0; i < 16; i++)
                allTextures.Add("WallsGreen.png" + i, Content.Load<Texture2D>("Tiles/WallsGreen" + i));

            // WallsRed
            for (int i = 0; i < 16; i++)
                allTextures.Add("WallsRed.png" + i, Content.Load<Texture2D>("Tiles/WallsRed" + i));

            // Load maps
            maps = new Dictionary<string, World>();
            maps.Add("Map1", new World(allTextures, "Map1", "Content/Map1.map"));

            currentWorld = maps["Map1"];

            //levelOne = new World(allTextures, "Level One", "level1.txt");

            SwitchToMainMenu();

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
            prevkbState = kbState;
            kbState = Keyboard.GetState();

            switch (currentState)
            {
                case (MainGameState.Debug):
                    {
                        debugger.UpdateAll(gameTime);
                        break;
                    }
                case (MainGameState.InGame):
                    {
                        currentWorld.UpdateAll(gameTime);
                        //Pauses the game if the player presses the escape key
                        if (kbState.IsKeyDown(Keys.Escape) && prevkbState.IsKeyUp(Keys.Escape))
                            SwitchToPauseMenu();
                        break;
                    }
                case (MainGameState.Menu):
                    {
                        //Checks which button is pressed or if a new button needs to be active
                        ButtonLogic();
                        break;
                    }
                case (MainGameState.Pause):
                    {
                        //Checks which button is pressed or if a new button needs to be active
                        ButtonLogic();
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
                        currentWorld.Draw(spriteBatch);
                        break;
                    }
                case (MainGameState.Menu):
                    {
                        spriteBatch.Draw(allTextures["Menu"], new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                        DrawButtons();
                        break;
                    }
                //The textures in the pause method are all missing; the assets need to be created again
                case (MainGameState.Pause):
                    {
                        spriteBatch.Draw(allTextures["PauseMenu"], new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                        DrawButtons();
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

        /// <summary>
        /// A helper method that will initialize the main menu whenever it is called
        /// </summary>
        public void SwitchToMainMenu()
        {
            currentState = MainGameState.Menu;
            int height = GraphicsDevice.Viewport.Height;
            int width = GraphicsDevice.Viewport.Width;
            buttons = new Button[2];
            Texture2D startButton = allTextures["StartButton"];
            buttons[0] = new Button(startButton, "StartButton", new Rectangle(width / 2 - startButton.Width / 2, height * 6 / 10 - startButton.Height / 2, startButton.Width, startButton.Height));
            Texture2D exitButton = allTextures["ExitButton"];
            buttons[1] = new Button(exitButton, "ExitButton", new Rectangle(width / 2 - exitButton.Width / 2, height * 3 / 4 - exitButton.Height / 2, exitButton.Width, exitButton.Height));
            indexActiveButton = 0;
        }

        /// <summary>
        /// A helper method that will intialize the pause menu whenever it is called
        /// </summary>
        public void SwitchToPauseMenu()
        {
            currentState = MainGameState.Pause;
            int height = GraphicsDevice.Viewport.Height;
            int width = GraphicsDevice.Viewport.Width;
            buttons = new Button[2];
            Texture2D resumeButton = allTextures["ResumeButton"];
            buttons[0] = new Button(resumeButton, "ResumeButton", new Rectangle(width / 2 - resumeButton.Width / 2, height * 6 / 10 - resumeButton.Height / 2, resumeButton.Width, resumeButton.Height));
            Texture2D mainMenuButton = allTextures["MainMenuButton"];
            buttons[1] = new Button(mainMenuButton, "MainMenuButton", new Rectangle(width / 2 - mainMenuButton.Width / 2, height * 3 / 4 - mainMenuButton.Height / 2, mainMenuButton.Width, mainMenuButton.Height));
            indexActiveButton = 0;
        }

        /// <summary>
        /// A helper method to draw all of the buttons in the buttons array
        /// </summary>
        public void DrawButtons()
        {
            for (int c = 0; c < buttons.Length; c++)
            {
                if (indexActiveButton == c)
                    spriteBatch.Draw(buttons[c].Texture, buttons[c].Rectangle, Color.White);
                else
                    spriteBatch.Draw(buttons[c].Texture, buttons[c].Rectangle, Color.Gray);
            }
        }

        /// <summary>
        /// A helper method for all of the button logic necessary for updating
        /// </summary>
        public void ButtonLogic()
        {
            //Adjusts the current active button if up or down arrow is pressed
            if (kbState.IsKeyDown(Keys.Down) && prevkbState.IsKeyUp(Keys.Down) && kbState.IsKeyUp(Keys.Up))
            {
                if (indexActiveButton == buttons.Length - 1)
                    indexActiveButton = 0;
                else
                    indexActiveButton++;
            }
            else if (kbState.IsKeyDown(Keys.Up) && prevkbState.IsKeyUp(Keys.Up) && kbState.IsKeyUp(Keys.Down))
            {
                if (indexActiveButton == 0)
                    indexActiveButton = buttons.Length - 1;
                else
                    indexActiveButton--;
            }
            //If the games is paused and the player presses the escape key, resumes the game (allowing them to easily toggle pause and unpause with escape)
            else if (kbState.IsKeyDown(Keys.Escape) && prevkbState.IsKeyUp(Keys.Escape) && currentState == MainGameState.Pause)
                currentState = MainGameState.InGame;
            //Switches the game state when a certain button is pressed
            else if (kbState.IsKeyDown(Keys.Enter) && prevkbState.IsKeyUp(Keys.Enter))
            {
                string buttonText = buttons[indexActiveButton].Name;
                if (buttonText == "StartButton")
                    currentState = MainGameState.InGame;
                else if (buttonText == "ExitButton")
                    Exit();
                else if (buttonText == "ResumeButton")
                    currentState = MainGameState.InGame;
                else if (buttonText == "MainMenuButton")
                    SwitchToMainMenu();
            }
        }
    }
}
