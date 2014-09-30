using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.StellarObjects;


namespace SpaceUnion.Controllers {

	class GameplayScreen {

		private KeyboardState keyState;
		private MouseState mouseState;
		private SpriteBatch spriteBatch;
		private Ship playerShip;
		private Planet planet;
		private Game1 game;

		Camera mainCamera;
		GUI gui;

		private Tools.AssetManager Assets;

		static public int worldWidth = 4000;
		static public int worldHeight = 2000;

		private int SCREEN_WIDTH;
		private int SCREEN_HEIGHT;


		public GameplayScreen(Game1 game, SpriteBatch batch) {
			this.game = game;
			SCREEN_HEIGHT = game.getScreenHeight();
			SCREEN_WIDTH = game.getScreenWidth();

			spriteBatch = batch;

			Assets = Game1.Assets;
			playerShip = new Ship(Assets.ufo, new Vector2(200, 200)); //Create new player ship
			planet = new Planet(Assets.waterPlanet, new Vector2(1000, 1000));

			gui = new GUI(game, playerShip);

			Viewport mainViewport = new Viewport((int) playerShip.getX(), (int) playerShip.getY(),
				game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height - GUI.guiHeight);
			mainCamera = new Camera(mainViewport, worldWidth, worldHeight, 1.0f);
		}

		/// <summary>
		/// Draws the stars background.
		/// </summary>
		protected void drawWorld() {

			/* Parallax Scrolling BG */
			spriteBatch.Draw(Assets.starfield2,
				new Rectangle((int) (mainCamera.Position.X * .9), (int) (mainCamera.Position.Y * .9), worldWidth / 4, worldHeight / 4),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield1,
				new Rectangle((int) (mainCamera.Position.X * 0.7), (int) (mainCamera.Position.Y * 0.7), 1600, 1200),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield1,
				new Rectangle((int) (mainCamera.Position.X * 0.6), (int) (mainCamera.Position.Y * 0.6), 2400, 1800),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield1,
				new Rectangle((int) (mainCamera.Position.X * 0.4), (int) (mainCamera.Position.Y * 0.4), 800, 600),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield3,
				new Rectangle((int) (mainCamera.Position.X * .5), (int) (mainCamera.Position.Y * .5f), worldWidth / 10, worldHeight / 10),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// 
		/// Checks player edge wrap around/edge stop.
		/// 
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Update(GameTime gameTime) {
			//playerShip.checkScreenWrap(Window); //Check if ship will wrap around edges
			playerShip.checkScreenStop(game.Window);
			keyState = Keyboard.GetState(); //Get which keys are pressed or released
			mouseState = Mouse.GetState();



			//Up Key toggles back thruster
			if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W)) {
				playerShip.thrust(gameTime);
			}

			//Left Key rotates ship left
			if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A)) {
				playerShip.rotateLeft(gameTime);
			}

			//Right Key rotates ship right
			else if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D)) {
				playerShip.rotateRight(gameTime);
			}

			//Space key activates debugging brake
			if (keyState.IsKeyDown(Keys.Space)) {
				playerShip.stop();
			}


			playerShip.update(gameTime);

			planet.update(gameTime, playerShip);

			mainCamera.setZoom(mouseState.ScrollWheelValue);
			mainCamera.Position = playerShip.CenterPosition; // center the camera to player's position
			mainCamera.update(gameTime);

			/* Transform mouse input from view to world position
			 * NOT currently used but may be useful in the future*/
			Matrix inverse = Matrix.Invert(mainCamera.getTransformation());
			Vector2 mousePos = Vector2.Transform(
			   new Vector2(mouseState.X, mouseState.Y), inverse);

			gui.update(playerShip);
		}


		public void draw() {

			/* Main camera sprite batch */
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,
				SamplerState.LinearWrap, null, null, null, mainCamera.getTransformation());


			drawWorld(); //Draws background

			playerShip.draw(spriteBatch); //Draws player space ship
			planet.draw(spriteBatch);

			spriteBatch.End();




			/* GUI spritebatch. Anything drawn here will remain
			 * static and not be affected by cameras. */
			spriteBatch.Begin();

			gui.draw(spriteBatch);


			spriteBatch.End();
		}

	}
}
