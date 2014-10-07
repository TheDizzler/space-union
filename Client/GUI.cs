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
		Vector2 line1Pos, line2Pos, line3Pos, line4Pos, line5Pos, line6Pos;

		public GUI(Game1 game, Ship ship, Planet plnt) {

			guiRectangle = Game1.Assets.guiRectangle;
			font = Game1.Assets.font;

			int guiY = game.getScreenHeight() - 80;
			int guiWidth = game.getScreenWidth();

			playerShip = ship;
			planet = plnt;

			rect = new Rectangle(0, guiY, guiWidth, guiHeight);

			line1Pos = new Vector2(100, guiY + 10);
			line2Pos = new Vector2(100, guiY + 20);
			line3Pos = new Vector2(100, guiY + 30);
			line4Pos = new Vector2(100, guiY + 40);
            line5Pos = new Vector2(100, guiY + 50);
            line6Pos = new Vector2(100, guiY + 60);
		}

        //draw method
		public void draw(SpriteBatch spriteBatch) {

			spriteBatch.Draw(guiRectangle, rect, Color.DarkSlateBlue); // the gui display

			spriteBatch.DrawString(font, "ship velocity X: " + playerShip.velocity.X
				+ " y = " + playerShip.velocity.Y,
				line1Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
			spriteBatch.DrawString(font, "Ship position X: " + playerShip.getX() + " y = " + playerShip.getY(),
				line3Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(font, "Total bullets: " + playerShip.projectiles.Count,
                line5Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
		}


		public void update() {
			
		}

	}
}