using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.StellarObjects;
using SpaceUnionXNA.Tools;


namespace SpaceUnionXNA.Tools {

	public class GUI {


		protected  Game1 game;

		public const int guiHeight = 80;

		protected SpriteFont font;

		protected Texture2D guiRectangle;

		protected  Ship playerShip;

		Rectangle rect;
		Vector2 line1Pos, line2Pos, line3Pos, line4Pos, line5Pos, line6Pos, line7Pos;


		private Vector2 lastVelocity;
		private Vector2 lastPosition;
		private Vector2 accel;
		private double totalTime = 0;
		private Vector2 currentVelocity;
		private List<Tangible> nearBy;
		/// <summary>
		/// Location of radar screen
		/// </summary>
		public Rectangle radarBox;

		private Tangible target;
		private MouseState mouseScreen;
		private Vector2 mouseWorld;
		


		public GUI(Game1 game, Ship ship) {

			guiRectangle = Game1.Assets.guiRectangle;
			font = Game1.Assets.font;

			this.game = game;

			int screenWidth =  game.getScreenWidth();
			int screenHeight = game.getScreenHeight();
			int guiY = screenHeight - 80;
			int guiWidth =screenWidth;

			playerShip = ship;

			rect = new Rectangle(0, guiY, guiWidth, guiHeight);

			line1Pos = new Vector2(100, guiY + 0);
			line2Pos = new Vector2(100, guiY + 10);
			line3Pos = new Vector2(100, guiY + 20);
			line4Pos = new Vector2(100, guiY + 30);
			line5Pos = new Vector2(100, guiY + 40);
			line6Pos = new Vector2(100, guiY + 50);
			line7Pos = new Vector2(100, guiY + 60);

			int radarSize = screenWidth / 6;
			radarBox = new Rectangle(screenWidth - radarSize - 15, screenHeight - radarSize - 15, radarSize, radarSize);

			nearBy = new List<Tangible>();
		}


		public virtual void update(GameTime gameTime, MouseState mouseState, Vector2 mousePos, QuadTree quadTree) {

			/* Debugging */
			mouseScreen = mouseState;
			mouseWorld = mousePos;

			if (playerShip.position.X <= 7999) {
				currentVelocity = playerShip.velocity;

				accel = (currentVelocity - lastVelocity) / (float) gameTime.ElapsedGameTime.TotalSeconds;

				totalTime += gameTime.ElapsedGameTime.TotalSeconds;
			}

			nearBy = quadTree.retrieve(playerShip);

			if (playerShip.collideTarget != null)
				target = playerShip.collideTarget;

			/* Debugging */
		}


		public virtual void update(GameTime gameTime, QuadTree quadTree) {

			/* Debugging */
			currentVelocity = playerShip.velocity;
			accel = (currentVelocity - lastVelocity) / (float) gameTime.ElapsedGameTime.TotalSeconds;
			totalTime += gameTime.ElapsedGameTime.TotalSeconds;

			if (playerShip.collideTarget != null)
				target = playerShip.collideTarget;
			/* Debugging */
		}


		public virtual void draw(SpriteBatch spriteBatch) {


			spriteBatch.Draw(guiRectangle, rect, Color.DarkSlateBlue); // the gui display
			spriteBatch.Draw(guiRectangle, radarBox, Color.Black);		// bg for radar display


			/* Debugging */
			//spriteBatch.DrawString(font, "ship velocity: " + currentVelocity,
			//	line1Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			//spriteBatch.DrawString(font, "Screen coords: " + mouseScreen.Position,
			//	line3Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			spriteBatch.DrawString(font, "mouseWorld: " + mouseWorld,
				line5Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			//spriteBatch.DrawString(font, "Ship position: " + playerShip.position,
			//	line3Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			spriteBatch.DrawString(font, "accel: " + accel,
				line1Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			//spriteBatch.DrawString(font, "totalTime: " + totalTime,
			//	line7Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			//spriteBatch.DrawString(font, "collidetarget: " + target,
			//	line7Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			if (playerShip is Scout) {
				spriteBatch.DrawString(font, "BeamLength: " + playerShip.getBeam().beamLength
					+ " distToTarget: " + playerShip.getBeam().distToTarget,
					line7Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
			}

			lastVelocity = playerShip.velocity;
			lastPosition = playerShip.position;

			/* Debugging */
		}

	}
}
