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
using SpaceUnionXNA.Animations;
using SpaceUnionXNA.Maps;

namespace SpaceUnionXNA {
	/// <summary>
	/// This is the main game class
	/// </summary>
	public class Game1 : Game {
		private int topBtmBorderPixels = 38;
		private int leftRightBorderPixels = 14;
		private Taskbar taskbar;
		public GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		public static ParticleEngine particleEngine;
		public ScrollingBackground scroll;

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
		public ControlMenu control_menu;
		ShipSelectionScreen shipselectionScreen;
		public string currentSound = "Medium";
		public string currentMusic = "Medium";

		public CollisionHandler collisionHandler;

		public List<Keys> keylist;

		//NETWORKING
		//public ClientCommHandler Communication { get; private set; }
		//public Player Player { get; set; }
		//public RoomInfo roomInfo;

		GameplayScreen gameplayScreen;
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

		public string windowState = "Windowed";
		public int width = 933;
		public int height = 700;

		/// <summary>
		/// Game State Enum to track game states
		/// </summary>
		public enum GameState {
			Playing,
			TeamBattle,
			Select,
			LobbyOptions,
			GameRoom,
			Login,
			MainMenu,
			Multiplayer,
			Options,
			ControlMenu,
			Credits,
			CreateLobby,
			LobbyBrowser,
			Lobby
		}

		public GameState currentGameState = GameState.Login;

		public Game1()
			: base() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			taskbar = new Taskbar();
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

			/* Initial key bindings when the game starts up
			 * Added by Steven */
			keylist = new List<Keys>();
			keylist.Add(Keys.W);
			keylist.Add(Keys.A);
			keylist.Add(Keys.D);
			keylist.Add(Keys.RightControl);
			keylist.Add(Keys.RightShift);
			keylist.Add(Keys.O);
			keylist.Add(Keys.P);
			Assets = new AssetManager(Content);
		}


		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
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

			collisionHandler = new CollisionHandler();
			graphics.ApplyChanges();

			MediaPlayer.IsRepeating = true;
			MediaPlayer.Play(Assets.titleSong);
			SoundEffect.MasterVolume = .15f;
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		/// shipselectionscreen added by Matthew Baldock
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			// All sprites get loaded in to here
			Assets.loadContent(GraphicsDevice);
			explosionEngine = new ExplosionEngine(Assets);

			List<Texture2D> textures = new List<Texture2D>();
			textures.Add(Assets.guiRectangle);
			//textures.Add(Assets.missile);
			particleEngine = new ParticleEngine(textures, new Vector2(400, 240));

			shipselectionScreen = new ShipSelectionScreen(this);

			scroll = new ScrollingBackground(Game1.Assets.background) { height = getScreenHeight(), width = getScreenWidth() };
			scroll.setPosition(UIConstants.ORIGIN);
			IsMouseVisible = true;
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent() {
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
		protected override void Update(GameTime gameTime) {
			//Allows game to exit.
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
				|| Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();


			//particleEngine.EmitterLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
			particleEngine.Update(gameTime);

			//Update the current state
			switch (currentGameState) {
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
				case GameState.ControlMenu:
					control_menu.Update(gameTime);
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
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.LightSeaGreen);

			

			//Draw the current state
			switch (currentGameState) {
				case GameState.Login:
					login_menu.DrawMenu(gameTime, spriteBatch);
					break;
				case GameState.MainMenu:
					main_menu.DrawMenu(gameTime, spriteBatch);
					break;
				case GameState.Multiplayer:
					multiplayer_menu.DrawMenu(gameTime, spriteBatch);
					break;
				case GameState.Options:
					options_menu.DrawMenu(gameTime, spriteBatch);
					break;
				case GameState.Credits:
					credits_menu.DrawMenu(gameTime, spriteBatch);
					break;
				case GameState.CreateLobby:
					create_lobby_menu.DrawMenu(gameTime, spriteBatch);
					break;
				case GameState.LobbyBrowser:
					lobby_browser_menu.DrawMenu(gameTime, spriteBatch);
					break;
				case GameState.Lobby:
					lobby_menu.DrawMenu(gameTime, spriteBatch);
					break;
				case GameState.Playing:
					GraphicsDevice.Clear(Color.Black);
					gameplayScreen.draw(gameTime);
					break;
				case GameState.Select:
					shipselectionScreen.draw(spriteBatch);
					break;
				case GameState.ControlMenu:
					control_menu.DrawMenu(gameTime, spriteBatch);
					break;
				default:
					break;
			}
			base.Draw(gameTime);
		}

		public int getScreenWidth() {
			return Window.ClientBounds.Width;
		}

		public int getScreenHeight() {
			return Window.ClientBounds.Height;
		}

		

		public void EnterMainMenu() {
			
			currentGameState = GameState.MainMenu;
			main_menu = new MainMenu(this);
			if (MediaPlayer.State == MediaState.Stopped)
				MediaPlayer.Play(Assets.titleSong);
		}

		public void EnterLoginMenu() {
			currentGameState = GameState.Login;
			login_menu = new LoginMenu(this);
			if(MediaPlayer.State == MediaState.Stopped)
				MediaPlayer.Play(Assets.titleSong);
		}

		public void EnterMultiplayerMenu() {
			currentGameState = GameState.Multiplayer;
			multiplayer_menu = new MultiplayerMenu(this);
		}

		public void EnterOptionsMenu() {
			currentGameState = GameState.Options;
			options_menu = new OptionsMenu(this);
		}

		public void EnterCreditsMenu() {
			currentGameState = GameState.Credits;
			credits_menu = new CreditsMenu(this);
		}

		public void EnterCreateLobbyMenu() {
			currentGameState = GameState.CreateLobby;
			create_lobby_menu = new CreateLobbyMenu(this);
		}

		public void EnterLobbyBrowserMenu() {
			currentGameState = GameState.LobbyBrowser;
			lobby_browser_menu = new LobbyBrowserMenu(this);
		}

		public void EnterLobbyMenu() {
			currentGameState = GameState.Lobby;
			lobby_menu = new LobbyMenu(this, "Alice's Lobby");
		}

		public void EnterShipSelectionScreen() {
			currentGameState = GameState.Select;
			shipselectionScreen = new ShipSelectionScreen(this);
		}

		public void EnterControlMenu() {
			currentGameState = GameState.ControlMenu;
			control_menu = new ControlMenu(this);
		}

		public void StartGame() {
			mainScreen.Desktop.Children.Clear(); //Clear the gui
			//gameplayScreen = new GameplayScreen(this, spriteBatch, shipselectionScreen.getship());
			currentGameState = GameState.Playing;
			gameplayScreen = new TeamBattle(this, spriteBatch, shipselectionScreen.getship(), new Map(2000, 2000, this));
			IsMouseVisible = false;
		}


		public void EndMatch() {
			GraphicsDevice.Viewport = new Viewport(0, 0, getScreenWidth(), getScreenHeight());
			currentGameState = GameState.MainMenu;
			MediaPlayer.Stop();
			gameplayScreen = null;
			IsMouseVisible = true;
		}


		/// <summary>
		/// Sets the client size based on the values passed in
		/// @Author Steven
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="fullScreen"></param>
		public void setScreenSize(int width, int height, string WindowState) {
			this.width = width;
			this.height = height;
			var form = (System.Windows.Forms.Form) System.Windows.Forms.Form.FromHandle(Window.Handle);
			if (WindowState == "Fullscreen") {
				graphics.IsFullScreen = true;
				width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
				graphics.PreferredBackBufferHeight = height;
				graphics.PreferredBackBufferWidth = width;
				graphics.ApplyChanges();
				mainScreen = new Screen(width, height);
				gui_manager.Screen = mainScreen;
			} else if (WindowState == "Borderless") {
				graphics.IsFullScreen = false;
				form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
				form.WindowState = System.Windows.Forms.FormWindowState.Normal;

				height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
				width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

				if (!taskbar.AutoHide)
				{
					switch (taskbar.Position)
					{
						case TaskbarPosition.Bottom:
							height -= taskbar.Size.Height;
							break;
						case TaskbarPosition.Left:
							width -= taskbar.Size.Width;
							break;
						case TaskbarPosition.Right:
							width -= taskbar.Size.Width;
							break;
						case TaskbarPosition.Top:
							height -= taskbar.Size.Height;
							break;
					}
				}

				graphics.PreferredBackBufferHeight = height;
				graphics.PreferredBackBufferWidth = width;
				graphics.ApplyChanges();
				mainScreen = new Screen(width, height);
				gui_manager.Screen = mainScreen;
				form.ClientSize = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size;
				form.Location = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Location;
			} else {
				form.WindowState = System.Windows.Forms.FormWindowState.Normal;
				form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

				if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width == width)
					graphics.PreferredBackBufferWidth = width - leftRightBorderPixels;
				else
					graphics.PreferredBackBufferWidth = width;

				if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height == height) {
					graphics.PreferredBackBufferHeight = height - topBtmBorderPixels;
				} else {
					graphics.PreferredBackBufferHeight = height;
				}
				graphics.IsFullScreen = false;

				form.ClientSize = new System.Drawing.Size(width, height);
				form.Location = new System.Drawing.Point(0, 0);

				mainScreen = new Screen(width, height);
				gui_manager.Screen = mainScreen;
				graphics.ApplyChanges();
			}

			windowState = WindowState;
		}
	}
}
