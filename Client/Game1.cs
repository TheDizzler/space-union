#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using SpaceUnion.Controllers;
using SpaceUnion.Tools;
#endregion


namespace SpaceUnion {
	/// <summary>
	/// This is the main game class
	/// </summary>
	public class Game1 : Game {

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

		TeamBattle gameplayScreen;
		MainMenuScreen mainMenuScreen;
		// Created by Matthew Baldock
		ShipSelectionScreen shipselectionScreen;
		LobbyOptions lobbyoptions;
		LobbyBrowser lobbybrowser;
		CreateLobby createlobby;
		GameLobby gamelobby;
		GameRoom gameroom;
		Options options;
		//end created by Matthew

		/// <summary>
		/// Game State Enum to track game states
		/// </summary>
		enum GameState {
			MainMenu,
			Playing,
			Options,
			TeamBattle,
			Select,
			LobbyOptions,
			LobbyBrowser,
			CreateLobby,
			GameLobby,
			GameRoom
		}

		GameState currentGameState = GameState.MainMenu;

		public int getScreenWidth() {
			return Window.ClientBounds.Width;
		}

		public int getScreenHeight() {
			return Window.ClientBounds.Height;
		}

        public void setScreenSize(int width, int height, bool fullScreen = false)
        {
            if (fullScreen)
            {
                //width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                //height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                Window.IsBorderless = true;
            }
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            Window.Position = new Point(0, 0);
            graphics.ApplyChanges();
        }

		public Game1()
			: base() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

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
		protected override void Initialize() {
			base.Initialize();
			//graphics.IsFullScreen = true;
			graphics.ApplyChanges();
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

			//Load Main Menu
			mainMenuScreen = new MainMenuScreen(this);
			shipselectionScreen = new ShipSelectionScreen(this);

			IsMouseVisible = true;



		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent() {
			base.UnloadContent();
			spriteBatch.Dispose();
		}



		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// 
		/// Select - Layer5 added by Matthew Baldock
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime) {
			//Allows Game to Exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
				|| Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			switch (currentGameState) {
				case GameState.MainMenu:
					mainMenuScreen.Update();
					break;
				case GameState.Playing:
					gameplayScreen.Update(gameTime);
					break;
				case GameState.Select:
					shipselectionScreen.update();
					break;
				case GameState.LobbyOptions:
					lobbyoptions.update();
					break;
				case GameState.LobbyBrowser:
					break;
					lobbybrowser.update();
					break;
				case GameState.CreateLobby:
					createlobby.update();
					break;
				case GameState.GameLobby:
					gamelobby.update();
					break;
				case GameState.GameRoom:
					gameroom.update();
					break;
				case GameState.Options:
					options.update();
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
			GraphicsDevice.Clear(Color.Black);



			switch (currentGameState) {
				case GameState.MainMenu:
					mainMenuScreen.draw(spriteBatch);
					break;
				case GameState.Playing:
					gameplayScreen.draw(gameTime);
					break;
				case GameState.Select:
					shipselectionScreen.draw(spriteBatch);
					break;
				case GameState.LobbyOptions:
					lobbyoptions.draw(spriteBatch);
					break;
				case GameState.LobbyBrowser:
					lobbybrowser.draw(spriteBatch);
					break;
				case GameState.CreateLobby:
					createlobby.draw(spriteBatch);
					break;
				case GameState.GameLobby:
					gamelobby.draw(spriteBatch);
					break;
				case GameState.GameRoom:
					gameroom.draw(spriteBatch);
					break;
				case GameState.Options:
					options.draw(spriteBatch);
					break;
			}

			base.Draw(gameTime);
		}

		//Gotolayer1 - gotoselect added by Matthew Baldock
		public void GoToMain() {

			currentGameState = GameState.MainMenu;
			IsMouseVisible = true;
		}
		public void GoToLobbyOptions() {
			lobbyoptions = new LobbyOptions(this);
			currentGameState = GameState.LobbyOptions;
			IsMouseVisible = true;
		}
		public void GoToLobbyBrowser() {
			lobbybrowser = new LobbyBrowser(this);
			currentGameState = GameState.LobbyBrowser;
			IsMouseVisible = true;
		}
		public void GoToCreateLobby() {
			createlobby = new CreateLobby(this);
			currentGameState = GameState.CreateLobby;
			IsMouseVisible = true;
		}
		public void GoToGameLobby() {
			gamelobby = new GameLobby(this);
			currentGameState = GameState.GameLobby;
			IsMouseVisible = true;
		}
		public void GoToGameRoom() {
			gameroom = new GameRoom(this);
			currentGameState = GameState.GameRoom;
			IsMouseVisible = true;
		}

		public void GoToSelect() {
			shipselectionScreen = new ShipSelectionScreen(this);
			currentGameState = GameState.Select;
			IsMouseVisible = true;
		}

		public void GoToOptions() {
			options = new Options(this);
			currentGameState = GameState.Options;
			IsMouseVisible = true;
		}
		public void StartGame() {
			//gameplayScreen = new GameplayScreen(this, spriteBatch, shipselectionScreen.getship());
			gameplayScreen = new TeamBattle(this, spriteBatch, shipselectionScreen.getship());
			Viewport v = GraphicsDevice.Viewport;
			currentGameState = GameState.Playing;
			IsMouseVisible = false;
		}


		public void EndMatch() {
			currentGameState = GameState.MainMenu;
			IsMouseVisible = true;
		}
	}
}
