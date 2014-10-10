using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using SpaceUnion.StellarObjects;
using SpaceUnion.Tools;
using SpaceUnion.Weapons;


namespace SpaceUnion.Controllers {

	class GameplayScreen {

		private KeyboardState keyState;
		private MouseState mouseState;
		private SpriteBatch spriteBatch;


		List<Asteroid> asteroids;
		List<Ship> ships;
		List<Tangible> targets;
		List<Planet> planets;

		private Ship playerShip;
		private Game1 game;
		Camera mainCamera;
		GUI gui;

		Random gen;

		private AssetManager Assets;

		static public int worldWidth = 8000;
		static public int worldHeight = 6000;

		private int SCREEN_WIDTH;
		private int SCREEN_HEIGHT;


		


		public GameplayScreen(Game1 game, SpriteBatch batch, Ship selectedship) {

			this.game = game;
			SCREEN_HEIGHT = game.getScreenHeight();
			SCREEN_WIDTH = game.getScreenWidth();

			spriteBatch = batch;

			gen = new Random();
			Assets = Game1.Assets;

			playerShip = selectedship;
			playerShip.Position = new Vector2(200, 200);
			planets = new List<Planet>();
			planets.Add(new Planet(Assets.waterPlanet, new Vector2(4000, 3000), 1000f, 1000));
			planets.Add(new Planet(Assets.moon, new Vector2(1000, 1000), 500f, 800));


			gui = new GUI(game, playerShip, planets[0]);

			Viewport mainViewport = new Viewport((int) playerShip.getX(), (int) playerShip.getY(),
				game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height - GUI.guiHeight);
			mainCamera = new Camera(mainViewport, worldWidth, worldHeight, 1.0f);


			asteroids = new List<Asteroid>();
			ships = new List<Ship>();
			targets = new List<Tangible>();

			ships.Add(playerShip);
			foreach (Ship ship in ships)
				targets.Add(ship);

			


			for (int i = 0; i < 10; i++)
				AddAsteroid(new Vector2(gen.Next(100, worldWidth), gen.Next(100, worldHeight)));
		}

		/// <summary>
		/// Draws the stars background.
		/// </summary>
		protected void drawWorld() {

			/* Parallax Scrolling BG */
			spriteBatch.Draw(Assets.starfield2,
				new Rectangle((int) (mainCamera.Position.X * .9), (int) (mainCamera.Position.Y * .9), worldWidth / 2, worldHeight / 2),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield1,
				new Rectangle((int) (mainCamera.Position.X * 0.7), (int) (mainCamera.Position.Y * 0.7), worldWidth / 4, worldHeight / 4),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield1,
				new Rectangle((int) (mainCamera.Position.X * 0.6), (int) (mainCamera.Position.Y * 0.6), worldWidth / 5, worldHeight / 5),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield1,
				new Rectangle((int) (mainCamera.Position.X * 0.4), (int) (mainCamera.Position.Y * 0.4), worldWidth / 6, worldHeight / 6),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			//spriteBatch.Draw(Assets.starfield3,
			//	new Rectangle((int) (mainCamera.Position.X * .5), (int) (mainCamera.Position.Y * .5f), worldWidth / 10, worldHeight / 10),
			//	null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
		}

		private void AddAsteroid(Vector2 position) {
			Asteroid asteroid = new Asteroid(Assets.asteroid, position);
			asteroids.Add(asteroid);
			targets.Add(asteroid);
		}


		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Update(GameTime gameTime) {

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
			if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D)) {
				playerShip.rotateRight(gameTime);
			}

			//Space key activates debugging brake
			if (keyState.IsKeyDown(Keys.Space)) {
				playerShip.stop();
			}

			if (keyState.IsKeyDown(Keys.LeftControl)) {
				playerShip.fire(gameTime);

			}

			if (keyState.IsKeyDown(Keys.LeftShift))
				playerShip.altFire(gameTime);

			foreach (Planet planet in planets)
				planet.update(gameTime, targets);



			mainCamera.setZoom(mouseState.ScrollWheelValue);
			mainCamera.Position = playerShip.Position; // center the camera to player's position
			mainCamera.update(gameTime);

			/* Transform mouse input from view to world position
			 * NOT currently used but may be useful in the future
			Matrix inverse = Matrix.Invert(mainCamera.getTransformation());
			Vector2 mousePos = Vector2.Transform(
			   new Vector2(mouseState.X, mouseState.Y), inverse);
			*/

			if (asteroids.Count < 50)
				AddAsteroid(new Vector2(gen.Next(100, 4000), gen.Next(100, 2000)));


			foreach (Ship ship in ships)
				ship.update(gameTime, targets);
			//gui.update();

			for (int i = asteroids.Count - 1; i >= 0; i--) {
				asteroids[i].update(gameTime, targets);
				if (!asteroids[i].isActive)
					asteroids.RemoveAt(i);
			}

			Game1.explosionEngine.update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void draw() {

			/* Main camera sprite batch */
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
				SamplerState.LinearWrap, null, null, null, mainCamera.getTransformation());


			drawWorld(); //Draws background


			//Draws all space ships
			foreach (Ship ship in ships)
				ship.draw(spriteBatch);

			foreach (Planet planet in planets)
				planet.draw(spriteBatch);


			// Draw the Asteroids
			for (int i = 0; i < asteroids.Count; i++) {
				asteroids[i].draw(spriteBatch);
			}

			Game1.explosionEngine.draw(spriteBatch);

			spriteBatch.End();


			/* GUI spritebatch. Anything drawn here will remain
			 * static and not be affected by cameras. */
			spriteBatch.Begin();

			gui.draw(spriteBatch);


			spriteBatch.End();
		}
	}
}
