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
using SpaceUnionXNA.Animations;
using Microsoft.Xna.Framework.Media;
using SpaceUnionXNA.Maps;


namespace SpaceUnionXNA.Controllers {

	public class GameplayScreen {

		private KeyboardState keyState;
		private MouseState mouseState;
		private SpriteBatch spriteBatch;

		protected QuadTree quadTree;
		//private List<Asteroid> asteroids;
		protected List<Ship> ships;
		//protected List<Ship> inactiveShips;
		/// <summary>
		/// All objects in this list get added to the quadtree every update.
		/// It is ESSENTIAL that newly spawned objects get added to this list when
		/// created. All objects remove themselves when destroy() is called.
		/// </summary>
		public static List<Tangible> targets;
		//private List<LargeMassObject> planets;
		//private List<Vector2> respawnpoints;
		//private List<Vector2> usedspawn;
		//private Background background;
		protected Ship playerShip;
		private Game1 game;
		protected Camera mainCamera;
		protected Camera radarCamera;
		protected GUI gui;

		
		//Random gen;

		private AssetManager Assets;

		//static public int worldWidth = 1000;
		//static public int worldHeight = 750;

		private int SCREEN_WIDTH;
		private int SCREEN_HEIGHT;

		protected Viewport basicViewport;
		protected Map level;
		public static float worldWidth;
		public static float worldHeight;


		public GameplayScreen(Game1 game, SpriteBatch batch, Ship selectedship, Map map) {
			this.game = game;
			this.level = map;
			
			worldWidth = map.worldWidth;
			worldHeight = map.worldHeight;

			SCREEN_HEIGHT = game.getScreenHeight();
			SCREEN_WIDTH = game.getScreenWidth();

			
			quadTree = new QuadTree(0, new Rectangle(0, 0, level.worldWidth, level.worldHeight));

			spriteBatch = batch;

			//gen = new Random();
			Assets = Game1.Assets;

			
			ships = new List<Ship>();
			//inactiveShips = new List<Ship>();
			targets = new List<Tangible>();
			level.init(targets);

			//for (int i = 0; i < 3; i++) {
			//	Ship enemyship = new Scout(game);
			//	enemyship.Position = level.respawnpoints.ElementAt(i + 3);
			//	enemyship.blueTeam = true;
			//	enemyship.rotation = (float) Math.PI / 4;
			//	enemyship.controlAI();
			//	ships.Add(enemyship);
			//}
			for (int i = 0; i < 2; i++) {
				Ship friendlyship = new Lobstar(game);
				friendlyship.Position = level.respawnpoints.ElementAt(i + 1);
				friendlyship.redTeam = true;
				ships.Add(friendlyship);
			}


			playerShip = selectedship;
			playerShip.Position = level.respawnpoints.ElementAt(0);
			//playerShip.rotation = (float) (Math.PI/2);

			basicViewport = game.GraphicsDevice.Viewport;

			gui = new GUI(game, playerShip);

			Viewport mainViewport = new Viewport {
				X = 0, Y = 0,
				Width = game.GraphicsDevice.Viewport.Width,
				Height = game.GraphicsDevice.Viewport.Height - GUI.guiHeight
			};

			Viewport radarViewport = new Viewport {
				X = gui.radarBox.X, Y = gui.radarBox.Y,
				Width = gui.radarBox.Width, Height = gui.radarBox.Height
			};

			mainCamera = new Camera(mainViewport, level.worldWidth, level.worldHeight, 0.5f);
			radarCamera = new Camera(radarViewport, level.worldWidth, level.worldHeight, 0.05f);


			ships.Add(playerShip);


			foreach (Ship ship in ships)
				targets.Add(ship);

			MediaPlayer.Stop();
			Game1.Assets.klaxxon.Play();
			MediaPlayer.Play(Game1.Assets.battleSong);
		}


		//private void AddAsteroid(Vector2 position) {
		//	Asteroid asteroid = new Asteroid(Assets.asteroid, position);
		//	asteroids.Add(asteroid);
		//	targets.Add(asteroid);
		//}



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
			//System.Console.WriteLine("tick");
			quadTree.clear();
			foreach (Tangible target in targets)
				quadTree.insert(target);


			keyState = Keyboard.GetState(); //Get which keys are pressed or released
			mouseState = Mouse.GetState();

			if (playerShip.isActive)
				playerShip.control(keyState, gameTime);



			/* Transform mouse input from view to world position
			 * NOT currently used but may be useful in the future*/
			//Matrix inverse = Matrix.Invert(mainCamera.getTransformation());
			//Vector2 mousePos = Vector2.Transform(
			//   new Vector2(mouseState.X, mouseState.Y), inverse);


			foreach (Ship ship in ships.ToList()) {

				ship.update(gameTime, quadTree);

				if (!ship.isActive) {
					if (ship.inactiveTime.Seconds >= 2) {
						level.respawn(ship);
					}
				}
			}

			level.update(gameTime, quadTree);


			

			/** Camera Debugging **/
			if (keyState.IsKeyDown(Keys.P))
				mainCamera.zoom += Camera.zoomIncrement;
			if (keyState.IsKeyDown(Keys.O))
				mainCamera.zoom -= Camera.zoomIncrement;
			/** Camera Debugging **/


			mainCamera.setZoom(mouseState.ScrollWheelValue);
			mainCamera.Position = playerShip.Position; // center the camera to player's position
			mainCamera.update(gameTime);

			radarCamera.Position = playerShip.Position; // center the camera to player's position
			radarCamera.update(gameTime);

			Game1.explosionEngine.update(gameTime);

			gui.update(gameTime, quadTree);
			game.collisionHandler.update(gameTime);

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

			level.background.draw(spriteBatch, mainCamera);

			drawScreen();

			Game1.particleEngine.Draw(spriteBatch);

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

			level.drawMiniMap(spriteBatch);

			foreach (Ship ship in ships)
				ship.drawMiniMap(spriteBatch);


			spriteBatch.End();
		}


		private void drawScreen() {

			//Draws all space ships
			foreach (Ship ship in ships)
				ship.draw(spriteBatch);

			level.draw(spriteBatch);

			//	foreach (LargeMassObject planet in planets)
			//		planet.draw(spriteBatch);


			//// Draw the Asteroids
			//for (int i = 0; i < asteroids.Count; i++) {
			//	asteroids[i].draw(spriteBatch);
			//}
			//drawBorder(spriteBatch, new Rectangle(0 - Assets.bug.Width / 2 - 10, 0 - Assets.bug.Height / 2 - 10,
			//		   worldWidth + Assets.bug.Width + 20, worldHeight + Assets.bug.Height + 20), 10, Color.White);
			Game1.explosionEngine.draw(spriteBatch);
		}

		
	}
}
