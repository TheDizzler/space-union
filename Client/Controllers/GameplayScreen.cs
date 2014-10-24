using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using SpaceUnion.Ships;
using SpaceUnion.StellarObjects;
using SpaceUnion.Tools;
using SpaceUnion.Weapons;


namespace SpaceUnion.Controllers {

	class GameplayScreen {

		private KeyboardState keyState;
		private MouseState mouseState;
		private SpriteBatch spriteBatch;

		QuadTree quadTree;
		List<Asteroid> asteroids;
		protected List<Ship> ships;
		protected List<Tangible> targets;
		protected List<Planet> planets;

		protected Ship playerShip;
		private Game1 game;
		private Camera mainCamera;
		private Camera radarCamera;
		protected GUI gui;

		Random gen;

		private AssetManager Assets;

		static public int worldWidth = 8000;
		static public int worldHeight = 6000;

		private int SCREEN_WIDTH;
		private int SCREEN_HEIGHT;
		private Viewport basicViewport;


		public GameplayScreen(Game1 game, SpriteBatch batch, Ship selectedship) {
            
			this.game = game;
			SCREEN_HEIGHT = game.getScreenHeight();
			SCREEN_WIDTH = game.getScreenWidth();

			quadTree = new QuadTree(0, new Rectangle(0, 0, worldWidth, worldHeight));

			spriteBatch = batch;

			gen = new Random();
			Assets = Game1.Assets;
            Ship enemyship = new Bug(game);
            enemyship.Position = new Vector2(300, 300);
            
			playerShip = selectedship;
			playerShip.Position = new Vector2(250, 250);
			//playerShip.rotation = (float) (Math.PI/2);
			planets = new List<Planet>();
			planets.Add(new Planet(Assets.waterPlanet, new Vector2(4000, 3000), 1000f, 1000));
			planets.Add(new Planet(Assets.moon, new Vector2(1000, 1000), 500f, 800));

			basicViewport = game.GraphicsDevice.Viewport;

			gui = new GUI(game, playerShip, planets[0]);

			Viewport mainViewport = new Viewport {
				X = 0, Y = 0,
				Width = game.GraphicsDevice.Viewport.Width, Height = game.GraphicsDevice.Viewport.Height - GUI.guiHeight
			};

			Viewport radarViewport = new Viewport {
				X = gui.radarBox.X, Y = gui.radarBox.Y,
				Width = gui.radarBox.Width, Height = gui.radarBox.Height
			};

			mainCamera = new Camera(mainViewport, worldWidth, worldHeight, 1.0f);
			radarCamera = new Camera(radarViewport, worldWidth, worldHeight, 0.1f);

			asteroids = new List<Asteroid>();
			ships = new List<Ship>();
			targets = new List<Tangible>();

			ships.Add(playerShip);
            ships.Add(new Bug(game));
			foreach (Ship ship in ships)
				targets.Add(ship);
			for (int i = 0; i < 50; i++)
				AddAsteroid(new Vector2(gen.Next(100, worldWidth), gen.Next(100, worldHeight)));



		}


		private void AddAsteroid(Vector2 position) {
			Asteroid asteroid = new Asteroid(Assets.asteroid, position);
			asteroids.Add(asteroid);
			targets.Add(asteroid);
		}



		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// Basic flow of "turn":
		/// create QuadTree, get player input, apply gravity,
		/// movement (ships, asteroids?), camera update, collision detection(?),
		/// explosion handling, gui update.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public virtual void Update(GameTime gameTime) {

			quadTree.clear();
			foreach (Tangible target in targets)
				quadTree.insert(target);


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
				planet.update(gameTime, quadTree, targets);



			/* Transform mouse input from view to world position
			 * NOT currently used but may be useful in the future
			Matrix inverse = Matrix.Invert(mainCamera.getTransformation());
			Vector2 mousePos = Vector2.Transform(
			   new Vector2(mouseState.X, mouseState.Y), inverse);
			*/

			//if (asteroids.Count < 50)
			//	AddAsteroid(new Vector2(gen.Next(100, 4000), gen.Next(100, 2000)));


			foreach (Ship ship in ships)
				ship.update(gameTime, quadTree);


			for (int i = asteroids.Count - 1; i >= 0; i--) {
				asteroids[i].update(gameTime, quadTree);
                if (!asteroids[i].isActive)
                    asteroids.RemoveAt(i);
			}


			mainCamera.setZoom(mouseState.ScrollWheelValue);
			mainCamera.Position = playerShip.Position; // center the camera to player's position
			mainCamera.update(gameTime);

			radarCamera.Position = playerShip.Position; // center the camera to player's position
			radarCamera.update(gameTime);

			Game1.explosionEngine.update(gameTime);

			gui.update(gameTime, quadTree);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void draw(GameTime gameTime) {

			game.graphics.GraphicsDevice.Viewport = mainCamera.viewport;
			/* Main camera sprite batch */
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
				SamplerState.LinearWrap, null, null, null, mainCamera.getTransformation());

			drawWorld(radarCamera); //Draws background

			drawScreen(mainCamera);


			spriteBatch.End();



			/* GUI spritebatch. Anything drawn here will remain
			 * static and not be affected by cameras. */
			game.graphics.GraphicsDevice.Viewport = basicViewport;
			spriteBatch.Begin();

			gui.draw(spriteBatch);


			spriteBatch.End();


			/** Radar camera spritebatch */
			game.graphics.GraphicsDevice.Viewport = radarCamera.viewport;
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
				SamplerState.LinearWrap, null, null, null, radarCamera.getTransformation());

			drawScreen(radarCamera);


			spriteBatch.End();






		}

		private void drawScreen(Camera camera) {



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
		}



		/// <summary>
		/// Draws the stars background.
		/// </summary>
		/// <param name="camera"></param>
		protected void drawWorld(Camera camera) {

			/* Parallax Scrolling BG */
			spriteBatch.Draw(Assets.starfield2,
				new Rectangle((int) (camera.Position.X * .9), (int) (camera.Position.Y * .9), worldWidth / 2, worldHeight / 2),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield1,
				new Rectangle((int) (camera.Position.X * 0.7), (int) (camera.Position.Y * 0.7), worldWidth / 4, worldHeight / 4),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield1,
				new Rectangle((int) (camera.Position.X * 0.6), (int) (camera.Position.Y * 0.6), worldWidth / 5, worldHeight / 5),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(Assets.starfield1,
				new Rectangle((int) (camera.Position.X * 0.4), (int) (camera.Position.Y * 0.4), worldWidth / 6, worldHeight / 6),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			//spriteBatch.Draw(Assets.starfield3,
			//	new Rectangle((int) (mainCamera.Position.X * .5), (int) (mainCamera.Position.Y * .5f), worldWidth / 10, worldHeight / 10),
			//	null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
		}

	}
}
