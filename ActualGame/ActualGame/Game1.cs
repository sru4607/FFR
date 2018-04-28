﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace ActualGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public enum MainGameState {Debug, Menu, Pause, InGame, GameOver, Options, Boss, Victory }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Enemy testEnemy; // Unused - kept for example documentation
        Display mainDisplay;
        public static MainGameState CurrentState { get; set; }
        Dictionary<string, Texture2D> allTextures;
        Debug debugger;
        public static Dictionary<string, World> maps;
        World currentWorld;
        Button[] buttons;
        int indexActiveButton;
        KeyboardState kbState;
        KeyboardState prevkbState;
        MouseState mState;
        public static Player player;
        //Dictionary<string, Soundtrack> tracks;
        //Soundtrack currentTrack;
        public static double fps;
        public static double secondsPerFrame;
        MouseState prevMouse;
        MouseState currentMouse;
        Controls controls;
        SoundEffect laugh;
        Boss currentBoss;
        MainGameState returnFromOptions;
        bool changingOption;
        
        
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

           // tracks = new Dictionary<string, Soundtrack>();

            

            kbState = Keyboard.GetState();
            prevkbState = kbState;
            // Initialize the maps list
            maps = new Dictionary<string, World>();

            //Initializes all of the controls
            controls = new Controls();
            changingOption = false;
            
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
            allTextures.Add("Button", Content.Load<Texture2D>("Button"));
            allTextures.Add("Blank", Content.Load<Texture2D>("Blank"));
            allTextures.Add("ParkaDude", Content.Load<Texture2D>("ParkaDude"));
            allTextures.Add("Boss", Content.Load<Texture2D>("Boss"));
            allTextures.Add("Chain", Content.Load<Texture2D>("Chain"));
            allTextures.Add("Mace", Content.Load<Texture2D>("Mace"));

            // Load tiles systemmatically
            // BrickWall
            for (int i = 0; i < 16; i++)
                allTextures.Add("BrickWall" + i, Content.Load<Texture2D>("Tiles/BrickWall" + i));

            // BrickWallBlue
            for (int i = 0; i < 16; i++)
                allTextures.Add("BrickWallBlue" + i, Content.Load<Texture2D>("Tiles/BrickWallBlue" + i));

            // BrickWallRed
            for (int i = 0; i < 16; i++)
                allTextures.Add("BrickWallRed" + i, Content.Load<Texture2D>("Tiles/BrickWallRed" + i));

            // holder
            for (int i = 0; i < 16; i++)
                allTextures.Add("holder" + i, Content.Load<Texture2D>("Tiles/holder" + i));

            // temp
            for (int i = 0; i < 16; i++)
                allTextures.Add("temp" + i, Content.Load<Texture2D>("Tiles/temp" + i));

            // Walls
            for (int i = 0; i < 16; i++)
                allTextures.Add("Walls" + i, Content.Load<Texture2D>("Tiles/Walls" + i));

            // WallsGreen
            for (int i = 0; i < 16; i++)
                allTextures.Add("WallsGreen" + i, Content.Load<Texture2D>("Tiles/WallsGreen" + i));

            // WallsRed
            for (int i = 0; i < 16; i++)
                allTextures.Add("WallsRed" + i, Content.Load<Texture2D>("Tiles/WallsRed" + i));

            // Portal
            for (int i = 0; i < 16; i++)
                allTextures.Add("Portal" + i, Content.Load<Texture2D>("Tiles/Portal" + i));

            // Load maps
            maps.Add("Map1", new World(allTextures, "Map1", "Content/Map1.map"));
            maps.Add("RedMap", new World(allTextures, "RedMap", "Content/RedMap.map"));
            maps.Add("BlueMap", new World(allTextures, "BlueMap", "Content/BlueMap.map"));
            maps.Add("PathTest", new World(allTextures, "PathTest", "Content/PathTest.map"));
            maps.Add("Tutorial1", new World(allTextures, "Tutorial1", "Content/Tutorial1.map"));
            maps.Add("Tutorial2", new World(allTextures, "Tutorial2", "Content/Tutorial2.map"));
            maps.Add("Actual1", new World(allTextures, "Actual1", "Content/Actual1.map"));
            maps.Add("Actual2", new World(allTextures, "Actual2", "Content/Actual2.map"));
            maps.Add("Actual3", new World(allTextures, "Actual3", "Content/Actual3.map"));
            maps.Add("Actual4", new World(allTextures, "Actual4", "Content/Actual4.map"));
            maps.Add("Actual5", new World(allTextures, "Actual5", "Content/Actual5.map"));
            maps.Add("Actual6", new World(allTextures, "Actual6", "Content/Actual6.map"));
            maps.Add("BossMap", new World(allTextures, "BossMap", "Content/BossMap.map"));

            currentWorld = maps["Tutorial1"];
            World.Current = currentWorld;
            //levelOne = new World(allTextures, "Level One", "level1.txt");

            // Change the game to fullscreen and start the main menu
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            currentBoss = new Boss(allTextures["Blank"], allTextures["Boss"], allTextures["Chain"], allTextures["Mace"],(int)maps["BossMap"].WorldMaxX / 2 - 128, (int)maps["BossMap"].WorldMaxY / 2 - 128, 256, 256);
            SwitchToMainMenu();

            // NOTE: ElecTown is an arrangement of a track from Megaman Battle Network 4 by Capcom. Cannot be used in the main game

            Soundtrack elecTown = new Soundtrack(Content.Load<Song>("Electown"), 150, 16, 4);
            elecTown.SetLead(Content.Load<Song>("Electown_Lead"), 150, 4, 4);

          //  tracks.Add("Electown", elecTown);

            laugh = Content.Load<SoundEffect>("Laugh");


          //  currentTrack = elecTown;

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

            switch (CurrentState)
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
                        if (World.Current == maps["BossMap"])
                        {
                            CurrentState = MainGameState.Boss;
                        }
                        if (currentWorld != World.Current)
                            ChangeMap();

                    //    currentTrack.Update();
                        currentWorld.UpdateAll(gameTime);

                        // Switch to the game over screen if the player is dead or I say so
                        if (player.IsDead)
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
                case (MainGameState.Boss):
                    {
                        if (currentWorld != World.Current)
                            ChangeMap();

                        World.Current = maps["BossMap"];

                        //    currentTrack.Update();
                        currentWorld.UpdateAll(gameTime);
                        currentBoss.UpdateBoss(gameTime);
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
            GraphicsDevice.Clear(Color.LightSlateGray);
            
            // TODO: Add your drawing code here
            //if in game follow the player
            if (CurrentState == MainGameState.InGame || CurrentState == MainGameState.Boss || CurrentState == MainGameState.Pause)
            {
                Matrix temp = mainDisplay.MainCam.GetViewMatrix();
                spriteBatch.Begin(transformMatrix: temp);
            }
            //else draw normally
            else
                spriteBatch.Begin();
            
            switch(CurrentState)
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
                case (MainGameState.Boss):
                    {
                        GraphicsDevice.Clear(Color.Black);
                        World.Current = maps["BossMap"];
                        currentBoss.DrawBoss(spriteBatch);
                        World.Current.Draw(spriteBatch);
                        
                        spriteBatch.End();

                        //Draw all gui elements in game here
                        spriteBatch.Begin();
                        currentBoss.DrawHealthBar(spriteBatch);

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
            CurrentState = MainGameState.InGame;
            // Create the player in the first map & add it to the world
            player = new Player(128, 128, currentWorld.QuadTree);
            mainDisplay = new Display(GraphicsDevice, player);
            player.Texture = allTextures["ParkaDude"];
            player.WalkTexture = allTextures["PenPenWalking"];
            World.Current = maps["Tutorial1"];
         //   tracks["Electown"].Start();
            ChangeMap();
        }

        /// <summary>
        /// A helper method that will initialize the main menu whenever it is called
        /// </summary>
        public void SwitchToMainMenu()
        {
            Soundtrack.Stop();
            IsMouseVisible = true;
            CurrentState = MainGameState.Menu;
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
            CurrentState = MainGameState.Pause;
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
            laugh.Play();
            IsMouseVisible = true;
            CurrentState = MainGameState.GameOver;
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
        /// A helper method to switch to the options menu and provide the game state to return to afterwards (INCOMPLETE)
        /// </summary>
        public void SwitchToOptions()
        {
            returnFromOptions = CurrentState;
            CurrentState = MainGameState.Options;
            IsMouseVisible = true;
            CurrentState = MainGameState.Pause;
            int height = GraphicsDevice.Viewport.Height;
            int width = GraphicsDevice.Viewport.Width;
            //Values need to be adjusted; they are currently not going to look as intended
            buttons = new Button[6];
            Texture2D leftControl = allTextures["Button"];
            buttons[0] = new Button(leftControl, "LeftControl", new Rectangle(width / 2 - leftControl.Width / 2, height * 2 / 14 - leftControl.Height / 2, leftControl.Width, leftControl.Height));
            Texture2D rightControl = allTextures["Button"];
            buttons[1] = new Button(rightControl, "RightControl", new Rectangle(width / 2 - rightControl.Width / 2, height * 4 / 14 - rightControl.Height / 2, rightControl.Width, rightControl.Height));
            Texture2D jumpControl = allTextures["Button"];
            buttons[2] = new Button(jumpControl, "JumpControl", new Rectangle(width / 2 - jumpControl.Width / 2, height * 6 / 14 - jumpControl.Height / 2, jumpControl.Width, jumpControl.Height));
            Texture2D attackControl = allTextures["Button"];
            buttons[3] = new Button(attackControl, "AttackControl", new Rectangle(width / 2 - attackControl.Width / 2, height * 8 / 14 - attackControl.Height / 2, attackControl.Width, attackControl.Height));
            Texture2D pauseControl = allTextures["Button"];
            buttons[4] = new Button(pauseControl, "PauseControl", new Rectangle(width / 2 - pauseControl.Width / 2, height * 10 / 14 - pauseControl.Height / 2, pauseControl.Width, pauseControl.Height));
            Texture2D exitButton = allTextures["ExitButton"];
            buttons[5] = new Button(exitButton, "ExitButton", new Rectangle(width / 2 - exitButton.Width / 2, height * 12 / 14 - exitButton.Height / 2, exitButton.Width, exitButton.Height));
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
        /// A helper method for options menu logic (INCOMPLETE)
        /// </summary>
        public void OptionsLogic()
        {
            //Only allows changes if a button is not selected
            if(!changingOption)
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
                //Switches the game state if the exit button is pressed, or prepares to change an option
                else if (kbState.IsKeyDown(Keys.Enter) && prevkbState.IsKeyUp(Keys.Enter))
                {
                    string buttonText = buttons[indexActiveButton].Name;
                    if (buttonText == "ExitButton")
                        CurrentState = returnFromOptions;
                    else
                        changingOption = true;
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
                            if (buttonText == "ExitButton")
                                CurrentState = returnFromOptions;
                            else
                                changingOption = true;
                        }
                    }
                }
            }
            //Attempts to change the control selected to a single key pressed, if possible
            else
            {
                Keys controlChange;
                if(kbState != prevkbState)
                {
                    //Method needs to be completed to check if only a single key has been pressed, and if so attempt to edit that control to the key
                    changingOption = false;
                }
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
            else if (kbState.IsKeyDown(Controls.Pause) && prevkbState.IsKeyUp(Controls.Pause) && CurrentState == MainGameState.Pause)
                CurrentState = MainGameState.InGame;
            //Switches the game state when a certain button is pressed
            else if (kbState.IsKeyDown(Keys.Enter) && prevkbState.IsKeyUp(Keys.Enter))
            {
                string buttonText = buttons[indexActiveButton].Name;
                if (buttonText == "ResumeButton")
                {
                    IsMouseVisible = false;
                    CurrentState = MainGameState.InGame;
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
                            CurrentState = MainGameState.InGame;
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
