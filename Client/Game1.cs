﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using SpaceUnion.Tools;
#endregion

namespace SpaceUnion {
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game {

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public static AssetManager Assets;

		Camera camera;

		private KeyboardState state;

		private Ship playerShip;

		static public int worldWidth = 4000;
		static public int worldHeight = 2000;

		GUI gui;
		


		public Game1()
			: base() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

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



			Assets.loadContent(GraphicsDevice); // All sprite get loaded in to here


			//Create new player ship
			playerShip = new Ship(Assets.ufo, new Vector2(200, 200));

			Viewport viewport = new Viewport((int) playerShip.getX(), (int) playerShip.getY(),
				GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Width / 2);
			camera = new Camera(viewport, worldWidth, worldHeight, 2.0f);

			gui = new GUI(Window);
			

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

			playerShip.checkScreenWrap(Window); //Check if ship will wrap around edges

			state = Keyboard.GetState(); //Get which keys are pressed or released
			MouseState mouse = Mouse.GetState();



			//Up Key toggles back thruster
			if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W)) {
				playerShip.thrust();
			}

			//Left Key rotates ship left
			if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A)) {
				playerShip.rotateLeft();
			}

			//Right Key rotates ship right
			else if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D)) {
				playerShip.rotateRight();
			}

			//Space key activates debugging brake
			if (state.IsKeyDown(Keys.Space)) {
				playerShip.stop();
			}

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			camera.zoom(mouse.ScrollWheelValue);
			camera.Position = playerShip.Position;


			// Transform mouse input from view to world position
			Matrix inverse = Matrix.Invert(camera.getTransformation());
			Vector2 mousePos = Vector2.Transform(
			   new Vector2(mouse.X, mouse.Y), inverse);

			gui.update(playerShip);

			base.Update(gameTime);
		}

		/// <summary>
		/// Draws the stars background and debug information.
		/// </summary>
		protected void drawWorld() {
			

			/* Parallax Scrolling BG */
			spriteBatch.Draw(Assets.starfield2,
				new Rectangle((int) (camera.Position.X * .9), (int) (camera.Position.Y * .9), worldWidth/4, worldHeight/4),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield1,
				new Rectangle((int) (camera.Position.X * 0.7), (int) (camera.Position.Y * 0.7), worldWidth, worldHeight),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield3,
				new Rectangle((int) (camera.Position.X * .5), (int) (camera.Position.Y *.5f), worldWidth, worldHeight),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);


			//spriteBatch.Draw(Assets.background, new Rectangle(0, 0, 800, 480), Color.White);


			


		}


		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {
			base.Draw(gameTime);
			GraphicsDevice.Clear(Color.Black);



			spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.LinearWrap, null, null, null, camera.getTransformation());


			drawWorld(); //Draws background

			playerShip.draw(spriteBatch); //Draws player space ship




			spriteBatch.End();


			/* GUI spritebatch
			 * anything drawn here will not be affected by cameras and remain static. */
			spriteBatch.Begin();

			gui.draw(spriteBatch);


			spriteBatch.End();

		}

		private void drawGUI() {

			

			
		}
	}
}
