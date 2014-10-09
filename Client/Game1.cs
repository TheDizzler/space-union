﻿#region Using Statements
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
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game {
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		/// <summary>
		/// Contains all game assets (gfx, audio, etc.)
		/// </summary>
		public static AssetManager Assets;
		/// <summary>
		/// An engine to create and manage all explosions, big and small
		/// </summary>
		public static ExplosionEngine explosionEngine;

		GameplayScreen gameplayScreen;
		MainMenuScreen mainMenuScreen;
		ShipSelectionScreen shipselectionScreen;
		/// <summary>
		/// Game State Enum to track game states
		/// </summary>
		enum GameState {
			MainMenu,
			Playing,
			Options,
			TeamBattle,
			Select,
		}

		GameState currentGameState = GameState.MainMenu;

		public int getScreenWidth() {
			return Window.ClientBounds.Width;
		}

		public int getScreenHeight() {
			return Window.ClientBounds.Height;
		}

		public Game1()
			: base() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

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
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
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
			//graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
			//graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;

			graphics.ApplyChanges();

			
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
		/// 
		/// Checks player edge wrap around.
		/// 
		/// </summary>
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
					shipselectionScreen.Update();
					break;
				default:
					break;
			}


			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);



			switch (currentGameState) {
				case GameState.MainMenu:
					mainMenuScreen.draw(spriteBatch);
					break;
				case GameState.Playing:
					gameplayScreen.draw();
					break;
				case GameState.Select:
					shipselectionScreen.draw(spriteBatch);
					break;
			}

			base.Draw(gameTime);
		}


		public void GoToMain() {

			currentGameState = GameState.MainMenu;
			IsMouseVisible = true;
		}


		public void GoToSelect() {
			shipselectionScreen = new ShipSelectionScreen(this);
			currentGameState = GameState.Select;
			IsMouseVisible = true;
		}


		public void StartGame() {
			gameplayScreen = new GameplayScreen(this, spriteBatch, shipselectionScreen.getship());
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
