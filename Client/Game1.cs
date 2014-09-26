#region Using Statements
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

		public static AssetManager assets;



		private KeyboardState state;

		private Ship playerShip;

		public Game1()
			: base() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			assets = new AssetManager(Content);
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

			assets.loadContent(); // All sprite get loaded in to here


			playerShip = new Ship(assets.ufo, new Vector2(200, 200)); //Create new player ship


		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
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

			//Up Key toggles back thruster
			if (state.IsKeyDown(Keys.Up)) {
				playerShip.thrust();
			}

			//Left Key rotates ship left
			if (state.IsKeyDown(Keys.Left)) {
				playerShip.rotateLeft();
			}

			//Right Key rotates ship right
			else if (state.IsKeyDown(Keys.Right)) {
				playerShip.rotateRight();
			}

			//Space key activates debugging brake
			if (state.IsKeyDown(Keys.Space)) {
				playerShip.stop();
			}

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();


			base.Update(gameTime);
		}

		/// <summary>
		/// Draws the stars background and debug information.
		/// </summary>
		protected void drawWorld() {
			SpriteFont font = assets.font;
			spriteBatch.Draw(assets.background, new Rectangle(0, 0, 800, 480), Color.White);
			spriteBatch.DrawString(font, "Radian Angle =" + playerShip.getAngle(), new Vector2(100, 20), Color.Red);
			spriteBatch.DrawString(font, "Degree Angle =" + (playerShip.getAngle() * (180 / Math.PI)), new Vector2(100, 50), Color.Red);
			spriteBatch.DrawString(font, "X =" + playerShip.getShipVelocityDirectionX()
				+ " y = " + playerShip.getShipVelocityDirectionY(), new Vector2(100, 80), Color.Red);
			spriteBatch.DrawString(font, "X =" + playerShip.getSpaceshipX()
				+ " y = " + playerShip.getSpaceshipY(), new Vector2(100, 110), Color.Red);
		}


		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();



			drawWorld(); //Draws background

			playerShip.draw(spriteBatch); //Draws player space ship




			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
