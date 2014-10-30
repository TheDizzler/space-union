using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Nuclex.UserInterface.Controls.Desktop;
using Nuclex.UserInterface.Visuals.Flat;
using Nuclex.Input;
using Nuclex.UserInterface;


namespace SpaceMenus
{

    /// <summary>Demonstrates the capabilities of the Nuclex UserInterface library</summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        /// <summary>Initializes and manages the graphics device</summary>
        public GraphicsDeviceManager graphics_device_manager;
        /// <summary>Manages the graphical user interface</summary>
        public GuiManager gui_manager;
        /// <summary>Manages input devices for the game</summary>
        public InputManager input_manager;
        /// <summary>Initializes a new instance of the user interface demo</summary>
        /// 
        public LoginMenu        login_menu;
        public MainMenu         main_menu;
        public MultiplayerMenu  multiplayer_menu;
        public OptionsMenu      options_menu;
        public CreditsMenu      credits_menu;
        public CreateLobbyMenu  create_lobby_menu;

        public Screen mainScreen;

        /// <summary>
        /// Game State Enum to track game states
        /// </summary>
        enum GameState
        {
            Login,
            MainMenu,
            Multiplayer,
            Options,
            Credits,
            CreateLobby,
            LobbyBrowser,
            Lobby
        }

        GameState currentGameState = GameState.Login;


        public Game1()
        {
            graphics_device_manager = new GraphicsDeviceManager(this);
            input_manager = new InputManager(Services, Window.Handle);
            gui_manager = new GuiManager(Services);
            // Automatically query the input devices once per update
            Components.Add(this.input_manager);
            gui_manager.DrawOrder = 1000;

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
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
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

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
                default:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>This is called when the game should draw itself.</summary>
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

    }

}
