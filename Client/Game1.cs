using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceUnionXNA.Controllers;
using SpaceUnionXNA.Tools;
using Nuclex.UserInterface;
using Nuclex.Input;

namespace SpaceUnionXNA
{
    /// <summary>
    /// This is the main game class
    /// </summary>
    public class Game1 : Game
    {

        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// Contains all game assets (gfx, audio, etc.)
        /// </summary>
        public static AssetManager Assets;
        /// <summary>
        /// An engine to create and manage all explosions, big and small
        /// </summary>
        public static ExplosionEngine explosionEngine;
        /// <summary>Initializes and manages the graphics device</summary>
        public GraphicsDeviceManager graphics_device_manager;
        /// <summary>Manages the graphical user interface</summary>
        public GuiManager gui_manager;
        /// <summary>Manages input devices for the game</summary>
        public InputManager input_manager;
        /// <summary>Initializes a new instance of the user interface demo</summary>
        /// 

        //Menu Classes with Nuclex Framework
        public LoginMenu login_menu;
        public MainMenu main_menu;
        public MultiplayerMenu multiplayer_menu;
        public OptionsMenu options_menu;
        public CreditsMenu credits_menu;
        public CreateLobbyMenu create_lobby_menu;
        public LobbyBrowserMenu lobby_browser_menu;
        public LobbyMenu lobby_menu;
        public Screen mainScreen;
        ShipSelectionScreen shipselectionScreen;

        //NETWORKING
        //public ClientCommHandler Communication { get; private set; }
        //public Player Player { get; set; }
        //public RoomInfo roomInfo;

        TeamBattle gameplayScreen;
        //MainMenuScreen mainMenuScreen;
        // Created by Matthew Baldock
        /*
		ShipSelectionScreen shipselectionScreen;
		LobbyOptions lobbyoptions;
		LobbyBrowser lobbybrowser;
		CreateLobby createlobby;
		GameLobby gamelobby;
		GameRoom gameroom;
		Options options;
         * */
        //end created by Matthew

        /// <summary>
        /// Game State Enum to track game states
        /// </summary>
        public enum GameState
        {
            Playing,
            TeamBattle,
            Select,
            LobbyOptions,
            GameRoom,
            Login,
            MainMenu,
            Multiplayer,
            Options,
            Credits,
            CreateLobby,
            LobbyBrowser,
            Lobby
        }

        public GameState currentGameState = GameState.Login;

        public int getScreenWidth()
        {
            return Window.ClientBounds.Width;
        }

        public int getScreenHeight()
        {
            return Window.ClientBounds.Height;
        }

        //public void setScreenSize(int width, int height, bool fullScreen = false)
        //{
        //	if (fullScreen)
        //	{
        //		//width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        //		//height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        //		Window.IsBorderless = true;
        //	}
        //	graphics.PreferredBackBufferHeight = height;
        //	graphics.PreferredBackBufferWidth = width;
        //	Window.Position = new Point(0, 0);
        //	graphics.ApplyChanges();
        //}

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Author: Troy Carefoot
            input_manager = new InputManager(Services, Window.Handle);
            gui_manager = new GuiManager(Services);
            Components.Add(this.input_manager);
            gui_manager.DrawOrder = 1000;

            //NETWORKING
            //Author: Troy Carefoot
            /*
            Communication = new ClientCommHandler();
            Player = new Player();
            Player.Username = "Troy";
            Player.Password = "loltroy";
            Player.IPAddress = Communication.getLocalIPv4Address();
             * */


            graphics.PreferredBackBufferWidth = 933;
            graphics.PreferredBackBufferHeight = 700;

            IsFixedTimeStep = false;

            Assets = new AssetManager(Content);
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            //Author: Troy Carefoot
            Viewport viewport = GraphicsDevice.Viewport;
            mainScreen = new Screen(viewport.Width, viewport.Height);
            gui_manager.Screen = mainScreen;
            gui_manager.Initialize();

            //Screen Margins
            mainScreen.Desktop.Bounds = new UniRectangle(
              new UniScalar(0.1f, 0.0f), new UniScalar(0.1f, 0.0f), // x and y = 10%
              new UniScalar(0.8f, 0.0f), new UniScalar(0.8f, 0.0f) // width and height = 80%
            );

            login_menu = new LoginMenu(this); //Users must login to play online

            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// shipselectionscreen added by Matthew Baldock
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // All sprites get loaded in to here
            Assets.loadContent(GraphicsDevice);
            explosionEngine = new ExplosionEngine(Assets);

            shipselectionScreen = new ShipSelectionScreen(this);
            IsMouseVisible = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
            spriteBatch.Dispose();
            Content.Unload();
        }



        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// 
        /// Select - Layer5 added by Matthew Baldock
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Allows game to exit.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Update the current state
            switch (currentGameState)
            {
                case GameState.Login:
                    login_menu.Update(gameTime);
                    break;
                case GameState.MainMenu:
                    main_menu.Update(gameTime);
                    break;
                case GameState.Multiplayer:
                    multiplayer_menu.Update(gameTime);
                    break;
                case GameState.Options:
                    options_menu.Update(gameTime);
                    break;
                case GameState.Credits:
                    credits_menu.Update(gameTime);
                    break;
                case GameState.CreateLobby:
                    create_lobby_menu.Update(gameTime);
                    break;
                case GameState.LobbyBrowser:
                    lobby_browser_menu.Update(gameTime);
                    break;
                case GameState.Lobby:
                    lobby_menu.Update(gameTime);
                    break;
                case GameState.Playing:
                    gameplayScreen.Update(gameTime);
                    break;
                case GameState.Select:
                    shipselectionScreen.update();
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// 
        /// select - layer5 added by Matthew Baldock
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSeaGreen);

            //Draw the current state
            switch (currentGameState)
            {
                case GameState.Login:
                    login_menu.DrawMenu(gameTime);
                    break;
                case GameState.MainMenu:
                    main_menu.DrawMenu(gameTime);
                    break;
                case GameState.Multiplayer:
                    multiplayer_menu.DrawMenu(gameTime);
                    break;
                case GameState.Options:
                    options_menu.DrawMenu(gameTime);
                    break;
                case GameState.Credits:
                    credits_menu.DrawMenu(gameTime);
                    break;
                case GameState.CreateLobby:
                    create_lobby_menu.DrawMenu(gameTime);
                    break;
                case GameState.LobbyBrowser:
                    lobby_browser_menu.DrawMenu(gameTime, spriteBatch);
                    break;
                case GameState.Lobby:
                    lobby_menu.DrawMenu(gameTime);
                    break;
                case GameState.Playing:
                    GraphicsDevice.Clear(Color.Black);
                    gameplayScreen.draw(gameTime);
                    break;
                case GameState.Select:
                    shipselectionScreen.draw(spriteBatch);
                    break;
                default:
                    break;
            }
            base.Draw(gameTime);
        }

        public void EnterMainMenu()
        {
            currentGameState = GameState.MainMenu;
            main_menu = new MainMenu(this);
        }

        public void EnterLoginMenu()
        {
            currentGameState = GameState.Login;
            login_menu = new LoginMenu(this);
        }

        public void EnterMultiplayerMenu()
        {
            currentGameState = GameState.Multiplayer;
            multiplayer_menu = new MultiplayerMenu(this);
        }

        public void EnterOptionsMenu()
        {
            currentGameState = GameState.Options;
            options_menu = new OptionsMenu(this);
        }

        public void EnterCreditsMenu()
        {
            currentGameState = GameState.Credits;
            credits_menu = new CreditsMenu(this);
        }

        public void EnterCreateLobbyMenu()
        {
            currentGameState = GameState.CreateLobby;
            create_lobby_menu = new CreateLobbyMenu(this);
        }

        public void EnterLobbyBrowserMenu()
        {
            currentGameState = GameState.LobbyBrowser;
            lobby_browser_menu = new LobbyBrowserMenu(this);
        }

        public void EnterLobbyMenu()
        {
            currentGameState = GameState.Lobby;
            lobby_menu = new LobbyMenu(this, "Alice's Lobby");
        }

        public void EnterShipSelectionScreen()
        {
            currentGameState = GameState.Select;
            shipselectionScreen = new ShipSelectionScreen(this);
        }


        public void StartGame()
        {
            //gameplayScreen = new GameplayScreen(this, spriteBatch, shipselectionScreen.getship());
            currentGameState = GameState.Playing;
            gameplayScreen = new TeamBattle(this, spriteBatch, shipselectionScreen.getship());
            Viewport v = GraphicsDevice.Viewport;
            IsMouseVisible = false;
        }


        public void EndMatch()
        {
            currentGameState = GameState.MainMenu;
            IsMouseVisible = true;
        }
    }
}
