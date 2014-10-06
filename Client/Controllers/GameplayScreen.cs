﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using SpaceUnion.StellarObjects;
using SpaceUnion.Tools;


namespace SpaceUnion.Controllers {

	class GameplayScreen {

		private KeyboardState keyState;
		private MouseState mouseState;
		private SpriteBatch spriteBatch;

		//static private bool flashFlag = false;
		//private static System.Timers.Timer invinsibilityTimer;
		//private static System.Timers.Timer flashingTimer;
		//static protected bool invinsible;


		List<Ship> ships;

		private Ship playerShip;
		private Planet planet;
		private Game1 game;
		Camera mainCamera;
		GUI gui;
		private ExplosionEngine explosionEngine;

		private AssetManager Assets;

		static public int worldWidth = 8000;
		static public int worldHeight = 6000;

		private int SCREEN_WIDTH;
		private int SCREEN_HEIGHT;



		public GameplayScreen(Game1 game, SpriteBatch batch) {
			this.game = game;
			SCREEN_HEIGHT = game.getScreenHeight();
			SCREEN_WIDTH = game.getScreenWidth();

			spriteBatch = batch;

			/*
			invinsibilityTimer = new System.Timers.Timer(2000);
			invinsibilityTimer.Elapsed += OnTimedEvent;
			invinsibilityTimer.Enabled = false;
			flashingTimer = new System.Timers.Timer(50);
			flashingTimer.Elapsed += onTimedEventFlashing;
			flashingTimer.Enabled = false;
			flashFlag = true;
			invinsible = false;
			*/

			Assets = Game1.Assets;
			explosionEngine = new ExplosionEngine(Assets);
			playerShip = new Ship(Assets.ufo, Assets.laser, new Vector2(200, 200), explosionEngine); //Create new player ship
			planet = new Planet(Assets.waterPlanet, new Vector2(1000, 1000));

			gui = new GUI(game, playerShip, planet);

			Viewport mainViewport = new Viewport((int) playerShip.getX(), (int) playerShip.getY(),
				game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height - GUI.guiHeight);
			mainCamera = new Camera(mainViewport, worldWidth, worldHeight, 1.0f);


			ships = new List<Ship>();
			ships.Add(playerShip);

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

			planet.update(gameTime, ships);



			mainCamera.setZoom(mouseState.ScrollWheelValue);
			mainCamera.Position = playerShip.Position; // center the camera to player's position
			mainCamera.update(gameTime);

			/* Transform mouse input from view to world position
			 * NOT currently used but may be useful in the future*/
			Matrix inverse = Matrix.Invert(mainCamera.getTransformation());
			Vector2 mousePos = Vector2.Transform(
			   new Vector2(mouseState.X, mouseState.Y), inverse);



			//UpdateDamageCollision();
			playerShip.update(gameTime, game.Window);
			//gui.update();

			explosionEngine.update(gameTime);

		}

		private void UpdateDamageCollision() {
			// Use the Rectangle's built-in intersect function to 
			// determine if two objects are overlapping
			//foreach (Projectile p in projectiles) {
			//	if (p.getProjectileHitBox().getArray().Intersects(playerShip.getShipHitBox().getArray())) {
			//		playerShip.setHealth(-1);
			//	}
			//}
			//Assets = Game1.Assets;
			// Only create the rectangle once for the player
			//rectangle1 = new Rectangle((int)(playerShip.Position.X ),
			//(int)(playerShip.Position.Y),
			//(int)(Assets.spaceShipTest.Width * playerShip.getScale()),
			//(int)(Assets.spaceShipTest.Height * playerShip.getScale()));

			/*
				// Determine if the two objects collided with each
				// other
				if (rectangle1.Intersects(rectangle2) && invinsible == false)
				{
					invinsible = true;
					invinsibilityTimer.Enabled = true;
					playerShip.setHealth(playerShip.getHealth() - 1);
					flashingTimer.Enabled = true;
				}
				if (invinsible == false)
				{
					invinsibilityTimer.Enabled = false;
					flashingTimer.Enabled = false;
				}
				if (playerShip.getHealth() == 0)
				{
					playerShip.setHealth(100);
					game.EndMatch();
					invinsibilityTimer.Enabled = false;
				}
			*/
		}

		

		public void draw() {

			/* Main camera sprite batch */
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,
				null, null, null, null, mainCamera.getTransformation());


			drawWorld(); //Draws background

			explosionEngine.draw(spriteBatch);

			foreach (Ship ship in ships)
				ship.draw(spriteBatch); //Draws all space ships

			planet.draw(spriteBatch);
			
			


			spriteBatch.End();




			/* GUI spritebatch. Anything drawn here will remain
			 * static and not be affected by cameras. */
			spriteBatch.Begin();

			gui.draw(spriteBatch);


			spriteBatch.End();
		}
		/*
		private static void OnTimedEvent(Object source, ElapsedEventArgs e)
		{
			invinsible = false;
		}
		private static void onTimedEventFlashing(Object source, ElapsedEventArgs e)
		{
			if (flashFlag == true)
			{
				playerShip.setAlpha(0);
				flashFlag = false;
			}
			if (flashFlag == false)
			{
				playerShip.setAlpha(255);
				flashFlag = true;
			}
		}
		*/
	}
}
