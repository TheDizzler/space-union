using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.StellarObjects;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;


namespace SpaceUnionXNA.Controllers {

	public class GameplayScreen {

		private KeyboardState keyState;
		private MouseState mouseState;
		private SpriteBatch spriteBatch;

		private QuadTree quadTree;
		private List<Asteroid> asteroids;
		protected List<Ship> ships;
		protected List<Ship> inactiveShips;
		/// <summary>
		/// All objects in this list get added to the quadtree every update.
		/// </summary>
		public static List<Tangible> targets;
	//	private List<LargeMassObject> planets;
        private List<Vector2> respawnpoints;
        private List<Vector2> usedspawn;
		private Background background;
		private Ship playerShip;
		private Game1 game;
		private Camera mainCamera;
		private Camera radarCamera;
		protected GUI gui;

		Random gen;

		private AssetManager Assets;

		static public int worldWidth = 1000;
		static public int worldHeight = 750;

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

            asteroids = new List<Asteroid>();
            ships = new List<Ship>();
            inactiveShips = new List<Ship>();
            targets = new List<Tangible>();

            respawnpoints = new List<Vector2>();
            usedspawn = new List<Vector2>();

            respawnpoints.Add(new Vector2(worldWidth / 2, worldHeight - 100));
            respawnpoints.Add(new Vector2(worldWidth - 100, worldHeight / 2));
            respawnpoints.Add(new Vector2(worldWidth / 2, worldHeight));
            respawnpoints.Add(new Vector2(100, worldHeight / 2));
            respawnpoints.Add(new Vector2(100, 100));
            respawnpoints.Add(new Vector2(worldWidth - 100, 100));
            respawnpoints.Add(new Vector2(worldWidth - 100, worldHeight - 100));
            respawnpoints.Add(new Vector2(100, worldHeight - 100));

            for (int i = 0; i < 3; i++)
            {
                Ship enemyship = new Bug(game);
                enemyship.Position = respawnpoints.ElementAt(i+3);
                enemyship.blueTeam = true;
                enemyship.rotation = (float)Math.PI / 4;
                enemyship.controlAI();
                ships.Add(enemyship);
            }
            for (int i = 0; i < 2; i++)
            {
                Ship friendlyship = new Scout(game);
                friendlyship.Position = respawnpoints.ElementAt(i + 1);
                friendlyship.redTeam = true;
                ships.Add(friendlyship);
            }


			background = new Background(worldWidth, worldHeight, Assets.starfield2, Assets.starfield1, Assets.starfield1, Assets.starfield1);


			playerShip = selectedship;
			playerShip.Position = respawnpoints.ElementAt(0);
			//playerShip.rotation = (float) (Math.PI/2);
			//planets = new List<LargeMassObject>();
			//planets.Add(new Planet(Assets.waterPlanet, new Vector2(4000, 3000), 500f, 1000));
			//planets.Add(new Planet(Assets.moon, new Vector2(1000, 1000), 250f, 800));

			basicViewport = game.GraphicsDevice.Viewport;

			gui = new GUI(game, playerShip);

			Viewport mainViewport = new Viewport {
				X = 0, Y = 0,
				Width = game.GraphicsDevice.Viewport.Width, Height = game.GraphicsDevice.Viewport.Height - GUI.guiHeight
			};

			Viewport radarViewport = new Viewport {
				X = gui.radarBox.X, Y = gui.radarBox.Y,
				Width = gui.radarBox.Width, Height = gui.radarBox.Height
			};

			mainCamera = new Camera(mainViewport, worldWidth, worldHeight, 1.0f);
			radarCamera = new Camera(radarViewport, worldWidth, worldHeight, 0.05f);


			ships.Add(playerShip);


			foreach (Ship ship in ships)
				targets.Add(ship);
			for (int i = 0; i < 5; i++)
				AddAsteroid(new Vector2(gen.Next(100, worldWidth), gen.Next(100, worldHeight)));
		//	foreach (Planet planet in planets)
		//		targets.Add(planet);


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


			playerShip.control(keyState, gameTime);


		//	foreach (Planet planet in planets)
		//		planet.update(gameTime, quadTree, targets);



			/* Transform mouse input from view to world position
			 * NOT currently used but may be useful in the future*/
			Matrix inverse = Matrix.Invert(mainCamera.getTransformation());
			Vector2 mousePos = Vector2.Transform(
			   new Vector2(mouseState.X, mouseState.Y), inverse);


			//if (asteroids.Count < 50)
			//	AddAsteroid(new Vector2(gen.Next(100, 4000), gen.Next(100, 2000)));


			foreach (Ship ship in ships.ToList()) {
				if (!ship.isActive) {
					ship.inactiveStart = gameTime.TotalGameTime;
					inactiveShips.Add(ship);
					ships.Remove(ship);
				}
				ship.update(gameTime, quadTree);
			}

            foreach (Ship ship in inactiveShips.ToList())
            {
                Random randomspawn = new Random();
                if (ship.isActive)
                {
                    ship.inactiveTime = TimeSpan.Zero;
                    ship.Position = respawnpoints.ElementAt(randomspawn.Next(respawnpoints.Count));
                    usedspawn.Add(respawnpoints.ElementAt(respawnpoints.IndexOf(ship.Position)));
                    respawnpoints.RemoveAt(respawnpoints.IndexOf(ship.Position));
                    ships.Add(ship);
                    inactiveShips.Remove(ship);
                }
                ship.update(gameTime, quadTree);
            }
            foreach (Vector2 spawn in usedspawn.ToList())
            {
                respawnpoints.Add(spawn);
                usedspawn.Remove(spawn);
            }

			for (int i = asteroids.Count - 1; i >= 0; i--) {
				asteroids[i].update(gameTime, quadTree);
				if (!asteroids[i].isActive) {
					asteroids.RemoveAt(i);
				}
			}

			if (keyState.IsKeyDown(Keys.P))
				mainCamera.zoom += Camera.zoomIncrement;
			if (keyState.IsKeyDown(Keys.O))
				mainCamera.zoom -= Camera.zoomIncrement;
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
				SamplerState.LinearClamp, null, null, null, mainCamera.getTransformation());


			background.draw(spriteBatch, mainCamera);

			drawScreen();


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
				SamplerState.LinearClamp, null, null, null, radarCamera.getTransformation());


			//draw grid
			drawGrid();


			foreach (Ship ship in ships)
				ship.drawMiniMap(spriteBatch);

		//	foreach (LargeMassObject planet in planets)
		//		planet.draw(spriteBatch);


			// Draw the Asteroids
			for (int i = 0; i < asteroids.Count; i++) {
				asteroids[i].draw(spriteBatch);
			}


			spriteBatch.End();
		}


		private void drawScreen() {

			//Draws all space ships
			foreach (Ship ship in ships)
				ship.draw(spriteBatch);

		//	foreach (LargeMassObject planet in planets)
		//		planet.draw(spriteBatch);


			// Draw the Asteroids
			for (int i = 0; i < asteroids.Count; i++) {
				asteroids[i].draw(spriteBatch);
			}

			Game1.explosionEngine.draw(spriteBatch);
		}

		/// <summary>
		/// Draw grid on radar screen
		/// </summary>
		private void drawGrid() {

			drawBorder(spriteBatch, new Rectangle(0, 0, worldWidth, worldHeight), 15, Color.White); // World enclosing box

			drawBorder(spriteBatch, new Rectangle(0, 0, worldWidth / 4, worldHeight / 4), 15, Color.White); //top
			drawBorder(spriteBatch, new Rectangle(worldWidth / 2, 0, worldWidth / 4, worldHeight / 4), 15, Color.White); //top right

			drawBorder(spriteBatch, new Rectangle(worldWidth / 4, worldHeight / 4, worldWidth / 4, worldHeight / 4), 15, Color.White);
			drawBorder(spriteBatch, new Rectangle(worldWidth * 3 / 4, worldHeight / 4, worldWidth / 4, worldHeight / 4), 15, Color.White);

			drawBorder(spriteBatch, new Rectangle(0, worldHeight / 2, worldWidth / 4, worldHeight / 4), 15, Color.White);
			drawBorder(spriteBatch, new Rectangle(worldWidth / 2, worldHeight / 2, worldWidth / 4, worldHeight / 4), 15, Color.White);

			drawBorder(spriteBatch, new Rectangle(worldWidth / 4, worldHeight * 3 / 4, worldWidth / 4, worldHeight / 4), 15, Color.White);
			drawBorder(spriteBatch, new Rectangle(worldWidth * 3 / 4, worldHeight * 3 / 4, worldWidth / 4, worldHeight / 4), 15, Color.White);
		}


		/// <summary>
		/// Hollow rectangle drawing code from:
		/// http://bluelinegamestudios.com/posts/drawing-a-hollow-rectangle-border-in-xna-4-0/
		/// </summary>
		/// <param name="batch"></param>
		/// <param name="rectangleToDraw"></param>
		/// <param name="thicknessOfBorder"></param>
		/// <param name="borderColor"></param>
		private void drawBorder(SpriteBatch batch, Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor) {
			// Draw top line
			batch.Draw(Assets.pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

			// Draw left line
			batch.Draw(Assets.pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

			// Draw right line
			batch.Draw(Assets.pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
											rectangleToDraw.Y,
											thicknessOfBorder,
											rectangleToDraw.Height), borderColor);
			// Draw bottom line
			batch.Draw(Assets.pixel, new Rectangle(rectangleToDraw.X,
											rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
											rectangleToDraw.Width,
											thicknessOfBorder), borderColor);
		}
    }
}
