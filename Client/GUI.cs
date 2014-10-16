﻿using System;
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
		private List<Tangible> nearBy;
		/// <summary>
		/// Location of radar screen
		/// </summary>
		public Rectangle radarBox;


		public GUI(Game1 game, Ship ship, Planet plnt) {

			guiRectangle = Game1.Assets.guiRectangle;
			font = Game1.Assets.font;

			int screenWidth =  game.getScreenWidth();
			int screenHeight = game.getScreenHeight();
			int guiY = screenHeight - 80;
			int guiWidth =screenWidth;

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

			int radarSize = screenWidth / 6;
			radarBox = new Rectangle(screenWidth - radarSize - 15, screenHeight - radarSize - 15, radarSize, radarSize);

			nearBy = new List<Tangible>();
		}


		public void update(GameTime gameTime, QuadTree quadTree) {
			if (playerShip.position.X <= 7999) {
				currentVelocity = playerShip.velocity;

				accel = (currentVelocity - lastVelocity) / (float) gameTime.ElapsedGameTime.TotalSeconds;

				totalTime += gameTime.ElapsedGameTime.TotalSeconds;
			}

			nearBy = quadTree.retrieve(playerShip);

		}

		public void draw(SpriteBatch spriteBatch) {




			spriteBatch.Draw(guiRectangle, rect, Color.DarkSlateBlue); // the gui display
			spriteBatch.Draw(guiRectangle, radarBox, Color.Black);		// bg for radar display


			spriteBatch.DrawString(font, "ship velocity: " + currentVelocity,
				line1Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);


			spriteBatch.DrawString(font, "Ship position: " + playerShip.position,
				line3Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			spriteBatch.DrawString(font, "accel: " + accel,
				line5Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			//spriteBatch.DrawString(font, "totalTime: " + totalTime,
			//	line7Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			spriteBatch.DrawString(font, "nearBy: " + nearBy.Count,
				line7Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

			lastVelocity = playerShip.velocity;
			lastPosition = playerShip.position;
		}

	}
}
