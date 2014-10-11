using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using SpaceUnion.StellarObjects;
using SpaceUnion.Tools;


namespace SpaceUnion {

	class GUI {


		public const int guiHeight = 80;

		SpriteFont font;

		Texture2D guiRectangle;

		private  Ship playerShip;

		Planet planet;
		Rectangle rect;
		Vector2 line1Pos, line2Pos, line3Pos, line4Pos, line5Pos, line6Pos, line7Pos;


		private Vector2 lastVelocity;
		private Vector2 lastPosition;
		private Vector2 accel;
		private double totalTime = 0;
		private Vector2 currentVelocity;


		public GUI(Game1 game, Ship ship, Planet plnt) {

			guiRectangle = Game1.Assets.guiRectangle;
			font = Game1.Assets.font;

			int guiY = game.getScreenHeight() - 80;
			int guiWidth = game.getScreenWidth();

			playerShip = ship;
			planet = plnt;

			rect = new Rectangle(0, guiY, guiWidth, guiHeight);

			line1Pos = new Vector2(100, guiY + 0);
			line2Pos = new Vector2(100, guiY + 10);
			line3Pos = new Vector2(100, guiY + 20);
			line4Pos = new Vector2(100, guiY + 30);
			line5Pos = new Vector2(100, guiY + 40);
			line6Pos = new Vector2(100, guiY + 50);
			line7Pos = new Vector2(100, guiY + 60);
		}


		public void update(GameTime gameTime) {
			if (playerShip.position.X <= 7999){
			currentVelocity = playerShip.velocity;

			accel = (currentVelocity - lastVelocity) / (float) gameTime.ElapsedGameTime.TotalSeconds;
			
				totalTime += gameTime.ElapsedGameTime.TotalSeconds;
			}

		}

		public void draw(SpriteBatch spriteBatch) {




			spriteBatch.Draw(guiRectangle, rect, Color.DarkSlateBlue); // the gui display

			spriteBatch.DrawString(font, "ship velocity: " + currentVelocity,
				line1Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);


			spriteBatch.DrawString(font, "Ship position: " + playerShip.position,
				line3Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			spriteBatch.DrawString(font, "accel: " + accel,
				line5Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			spriteBatch.DrawString(font, "totalTime: " + totalTime,
				line7Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			lastVelocity = playerShip.velocity;
			lastPosition = playerShip.position;
		}

	}
}
