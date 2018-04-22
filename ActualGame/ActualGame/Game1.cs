using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace ActualGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    enum MainGameState {Debug, Menu, Pause, InGame, GameOver }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Enemy testEnemy; // Unused - kept for example documentation
        Display mainDisplay;
        MainGameState currentState;
        Dictionary<string, Texture2D> allTextures;
        Debug debugger;
        public static Dictionary<string, World> maps;
        World currentWorld;
        Button[] buttons;
        int indexActiveButton;
        KeyboardState kbState;
        KeyboardState prevkbState;
        MouseState mState;
        Player player;
        Dictionary<string, Soundtrack> tracks;
        Soundtrack currentTrack;
        public static double fps;
        public static double secondsPerFrame;
        MouseState prevMouse;
        MouseState currentMouse;
        Controls controls;
        
        
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

            tracks = new Dictionary<string, Soundtrack>();

            

            kbState = Keyboard.GetState();
            prevkbState = kbState;
            // Initialize the maps list
            maps = new Dictionary<string, World>();

            //Initializes all of the controls
            controls = new Controls();


            // DO NOT WRITE CODE BELOW HERE
            // Base game logic

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Set up animation variables
            fps = 30.0;
            secondsPerFrame = 1.0f / fps;

            // Import levels
            //levelOne.Import();

            // Add all necessary textures to the dictionary
            allTextures.Add("Floor", Content.Load<Texture2D>("missingtexture"));
            allTextures.Add("PenPen", Content.Load<Texture2D>("PenPen"));
            allTextures.Add("PenPenWalking", Content.Load<Texture2D>("PenPenWalking"));
            allTextures.Add("missingtexture", Content.Load<Texture2D>("missingtexture"));
            allTextures.Add("Enemy", Content.Load<Texture2D>("Penguin"));
            allTextures.Add("Menu", Content.Load<Texture2D>("Menu"));
            allTextures.Add("StartButton", Content.Load<Texture2D>("StartButton"));
            allTextures.Add("ExitButton", Content.Load<Texture2D>("ExitButton"));
            allTextures.Add("PauseMenu", Content.Load<Texture2D>("PauseMenu"));
            allTextures.Add("ResumeButton", Content.Load<Texture2D>("ResumeButton"));
            allTextures.Add("MainMenuButton", Content.Load<Texture2D>("MainMenuButton"));
            allTextures.Add("GameOverBackground", Content.Load<Texture2D>("GameOverBackground"));
            allTextures.Add("GameOverRetry", Content.Load<Texture2D>("GameOverRetry"));
            allTextures.Add("GameOverExit", Content.Load<Texture2D>("GameOverExit"));
            allTextures.Add("Heart", Content.Load<Texture2D>("Heart"));

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
            maps.Add("Map1", new World(allTextures, "Map1", "Content/Map1.map"));
            maps.Add("RedMap", new World(allTextures, "RedMap", "Content/RedMap.map"));
            maps.Add("BlueMap", new World(allTextures, "BlueMap", "Content/BlueMap.map"));
            maps.Add("PathTest", new World(allTextures, "PathTest", "Content/PathTest.map"));

            currentWorld = maps["BlueMap"];
            World.Current = currentWorld;
            //levelOne = new World(allTextures, "Level One", "level1.txt");

            // Change the game to fullscreen and start the main menu
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            SwitchToMainMenu();

            Soundtrack elecTown = new Soundtrack(Content.Load<Song>("Electown"), 150, 16, 4);
            elecTown.SetLead(Content.Load<Song>("Electown_Lead"), 150, 4, 4);

            tracks.Add("Electown", elecTown);

            currentTrack = elecTown;

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
            mState = Mouse.GetState();

            switch (currentState)
            {
                case (MainGameState.Debug):
                    {
                        if(debugger == null)
                        {
                            // Values used for debugging purposes
                            debugger = new Debug(allTextures, GraphicsDevice);
                            debugger.InstantiateAll();
                        }
                        debugger.UpdateAll(gameTime);
                        break;
                    }
                case (MainGameState.InGame):
                    {
                        if (currentWorld != World.Current)
                            ChangeMap();

                        currentTrack.Update();
                        currentWorld.UpdateAll(gameTime);

                        // Switch to the game over screen if the player is dead or I say so
                        if (player.IsDead || kbState.IsKeyDown(Keys.Delete))
                            SwitchToGameOver();

                        //Pauses the game if the player presses the pause key
                        if (kbState.IsKeyDown(Controls.Pause) && prevkbState.IsKeyUp(Controls.Pause))
                            SwitchToPauseMenu();
                        mainDisplay.Update();

                        if (kbState.IsKeyDown(Keys.Space) && prevkbState.IsKeyUp(Keys.Space))
                            currentWorld.CheckWarps(player);
                        
                        break;
                    }
                case (MainGameState.Menu):
                    {

                        //Checks which button is pressed or if a new button needs to be active
                        MenuButtonLogic();
                        UpdateMouseInButton();
                        break;
                    }
                case (MainGameState.Pause):
                    {
                        //Checks which button is pressed or if a new button needs to be active
                        PauseButtonLogic();
                        UpdateMouseInButton();
                        break;
                    }
                case (MainGameState.GameOver):
                    {
                        GameOverLogic();
                        UpdateMouseInButton();
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
            //if in game follow the player
            if (currentState == MainGameState.InGame || currentState == MainGameState.Pause)
            {
                Matrix temp = mainDisplay.MainCam.GetViewMatrix();
                spriteBatch.Begin(transformMatrix: temp);
            }
            //else draw normally
            else
                spriteBatch.Begin();
            
            switch(currentState)
            {
                case (MainGameState.Debug):
                    {
                        debugger.Draw(spriteBatch);
                        break;
                    }
                case (MainGameState.InGame):
                    {
                        World.Current.Draw(spriteBatch);

                        spriteBatch.End();

                        //Draw all gui elements in game here
                        spriteBatch.Begin();


                        // Draw the player's health bar
                        Texture2D heart = allTextures["Heart"];
                        for (int i = 0; i<player.MaxHealth; i++)
                        {
                            if (i < player.HP)
                            {
                                spriteBatch.Draw(heart, new Rectangle(10+i * 70, 10, 64, 64), Color.White);
                            }
                            else
                            {
                                spriteBatch.Draw(heart, new Rectangle(10+i * 70, 10, 64, 64), Color.Gray);
                            }
                        }
                        spriteBatch.End();

                        //All Elements in game must be above this line
                        spriteBatch.Begin(transformMatrix: mainDisplay.MainCam.GetViewMatrix());

                        break;
                    }
                case (MainGameState.Menu):
                    {
                        spriteBatch.Draw(allTextures["Menu"], new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                        DrawButtons();
                        break;
                    }
                case (MainGameState.Pause):
                    {
                        World.Current.Draw(spriteBatch);


                        //Draw all gui elements in game here


                        

                        //All Elements in game must be above this line
                        spriteBatch.End();
                        spriteBatch.Begin();

                        // Draw the player's health bar
                        Texture2D heart = allTextures["Heart"];
                        for (int i = 0; i < player.MaxHealth; i++)
                        {
                            if (i < player.HP)
                            {
                                spriteBatch.Draw(heart, new Rectangle(10 + i * 70, 10, 64, 64), Color.White);
                            }
                            else
                            {
                                spriteBatch.Draw(heart, new Rectangle(10 + i * 70, 10, 64, 64), Color.Gray);
                            }
                        }

                        spriteBatch.Draw(allTextures["PauseMenu"], new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                        DrawButtons();
                        break;
                    }
                case (MainGameState.GameOver):
                    {
                        GraphicsDevice.Clear(Color.Red);
                        DrawGameOver(spriteBatch);
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
        /// A helper method to change between maps
        /// </summary>
        public void ChangeMap()
        {
            currentWorld = World.Current;
            currentWorld.ResetWorld();
            currentWorld.AllObjects.Add(player);
            currentWorld.QuadTree.AddObject(player);
        }

        /// <summary>
        /// A helper method meant to start the game
        /// </summary>
        public void StartGame()
        {
            IsMouseVisible = false;
            currentState = MainGameState.InGame;
            // Create the player in the first map & add it to the world
            player = new Player(128, 128, currentWorld.QuadTree);
            mainDisplay = new Display(GraphicsDevice, player);
            player.Texture = allTextures["PenPen"];
            player.WalkTexture = allTextures["PenPenWalking"];
            World.Current = maps["BlueMap"];
            ChangeMap();
        }

        /// <summary>
        /// A helper method that will initialize the main menu whenever it is called
        /// </summary>
        public void SwitchToMainMenu()
        {
            Soundtrack.Stop();
            IsMouseVisible = true;
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
            IsMouseVisible = true;
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
        /// A helper method that initializes the game over state when called
        /// </summary>
        public void SwitchToGameOver()
        {
            Soundtrack.Stop();
            IsMouseVisible = true;
            currentState = MainGameState.GameOver;
            int height = GraphicsDevice.Viewport.Height;
            int width = GraphicsDevice.Viewport.Width;
            Texture2D background = allTextures["GameOverBackground"];

            // scale the buttons to the size of the screen
            float scale = (float)height / (float)background.Height;

            buttons = new Button[2];
            Texture2D retry = allTextures["GameOverRetry"];
            Texture2D exit = allTextures["GameOverExit"];
            buttons[0] = new Button(retry, "Retry", new Rectangle((int)(width/2 - (scale * retry.Width) / 2), (int)(.65 * height), (int)(retry.Width * scale), (int)(retry.Height*scale)));
            buttons[1] = new Button(exit, "Exit", new Rectangle((int)(width / 2 - (scale * exit.Width) / 2), (int)(.8 * height), (int)(exit.Width * scale), (int)(exit.Height * scale)));
            indexActiveButton = 0;
        }

        /// <summary>
        /// A helper method that draws the game over state
        /// </summary>
        public void DrawGameOver(SpriteBatch spriteBatch)
        {
            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;
            Texture2D background = allTextures["GameOverBackground"];

            // Get a scale factor so you can properly draw the background
            float scale = (float)height / (float)background.Height;
            spriteBatch.Draw(background, new Rectangle((int)(width/2 - (scale*background.Width)/2), 0, (int)(scale*background.Width), (int)(scale*background.Height)), Color.White);

            DrawButtons();
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
        /// A helper method for game over button logic
        /// </summary>
        public void GameOverLogic()
        {
            // Adjust the currently selected button
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
            // If the user presses enter, handle that button's logic
            else if (kbState.IsKeyDown(Keys.Enter) && prevkbState.IsKeyUp(Keys.Enter))
            {
                switch (indexActiveButton)
                {
                    case 0:
                        IsMouseVisible = false;
                        StartGame();
                        break;
                    case 1:
                        SwitchToMainMenu();
                        break;
                }
            }
            //Menu Mouse Control
            prevMouse = currentMouse;
            currentMouse = Mouse.GetState();
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Contains(Mouse.GetState().Position))
                {
                    indexActiveButton = i;
                    if (currentMouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                    {
                        switch (indexActiveButton)
                        {
                            case 0:
                                IsMouseVisible = false;
                                StartGame();
                                break;
                            case 1:
                                SwitchToMainMenu();
                                break;
                        }
                    }

                }
            }

            
        }

        /// <summary>
        /// A helper method for all of the button logic necessary for updating the main menu
        /// </summary>
        public void MenuButtonLogic()
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
            //Switches the game state when a certain button is pressed
            else if (kbState.IsKeyDown(Keys.Enter) && prevkbState.IsKeyUp(Keys.Enter))
            {
                string buttonText = buttons[indexActiveButton].Name;
                if (buttonText == "StartButton")
                {
                    IsMouseVisible = false;
                    StartGame();
                }
                else if (buttonText == "ExitButton")
                    Exit();
            }
            //Menu Mouse Control
            prevMouse = currentMouse;
            currentMouse = Mouse.GetState();
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Contains(Mouse.GetState().Position))
                {
                    indexActiveButton = i;
                    if(currentMouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                    {
                        string buttonText = buttons[indexActiveButton].Name;
                        if (buttonText == "StartButton")
                        {
                            IsMouseVisible = false;
                            StartGame();
                        }
                        else if (buttonText == "ExitButton")
                            Exit();
                    }
                    
                }
            }
           
            
        }

        public void PauseButtonLogic()
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
            else if (kbState.IsKeyDown(Controls.Pause) && prevkbState.IsKeyUp(Controls.Pause) && currentState == MainGameState.Pause)
                currentState = MainGameState.InGame;
            //Switches the game state when a certain button is pressed
            else if (kbState.IsKeyDown(Keys.Enter) && prevkbState.IsKeyUp(Keys.Enter))
            {
                string buttonText = buttons[indexActiveButton].Name;
                if (buttonText == "ResumeButton")
                {
                    IsMouseVisible = false;
                    currentState = MainGameState.InGame;
                }
                else if (buttonText == "MainMenuButton")
                    SwitchToMainMenu();
            }
            //Menu Mouse Control
            prevMouse = currentMouse;
            currentMouse = Mouse.GetState();
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Contains(Mouse.GetState().Position))
                {
                    indexActiveButton = i;
                    if (currentMouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
                    {
                        string buttonText = buttons[indexActiveButton].Name;
                        if (buttonText == "ResumeButton")
                        {
                            IsMouseVisible = false;
                            currentState = MainGameState.InGame;
                        }
                        else if (buttonText == "MainMenuButton")
                            SwitchToMainMenu();
                    }

                }
            }


        }

        /// <summary>
        /// A helper method to determine if the mouse is in a button and set the active button to that if so
        /// </summary>
        public void UpdateMouseInButton()
        {
            for(int c=0; c<buttons.Length; c++)
            {
                if (buttons[c].Rectangle.Contains(mState.Position))
                {
                    indexActiveButton = c;
                    break;
                }
            }
        }
    }
}
