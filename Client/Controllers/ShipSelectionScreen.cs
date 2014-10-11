﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.Ships;
using SpaceUnion.Tools;

namespace SpaceUnion.Controllers {
	class ShipSelectionScreen {

		Game1 game;
		ShipButton[] shipSelectionArray;
		ShipButton lastButton;
		BaseButton confirmButton;
		Ship selectedShip;


		/* Default size of the ships */
		const int WIDTH       = 128;
		const int HEIGHT      = 128;
		const int SHIPCOUNT = 4;   // Change value according to how many different ships are available
		int shipsPerRow;

		public ShipSelectionScreen(Game1 game) {
			this.game = game;

			confirmButton = new BaseButton(Game1.Assets.confirm) { height = 100, width = 300 };


			shipsPerRow = (game.getScreenWidth() - (WIDTH * 2)) / WIDTH;
			float currentShipsPerRow = shipsPerRow;
			int shipsPerLastRow = (int) (SHIPCOUNT % shipsPerRow);
			float shipsPerColumn = (float) Math.Ceiling((float) (SHIPCOUNT / shipsPerRow));
			this.game = game;
			shipSelectionArray = new ShipButton[SHIPCOUNT];
			/* Actual ships used; commented out to test other functions */
			shipSelectionArray[0] = new ShipButton(new UFO(game));
			shipSelectionArray[1] = new ShipButton(new Stunt(game));
			shipSelectionArray[2] = new ShipButton(new Zoid(game));
			shipSelectionArray[3] = new ShipButton(new TestShip(game));

			/* For Testing X amount of ships; Remove */
			//for (int i = 0; i < SHIPCOUNT; i++)
			//{
			//	shipSelectionArray[i] = new ShipButton(Game1.Assets.ufo);
			//}

			confirmButton = new BaseButton(Game1.Assets.confirm) {height = 100, width = 300};

			/* Sets the ship's icon size and then its position on the screen based on how many ships there are */
			for (int i = 0; i < SHIPCOUNT; i++) {
				if (i == SHIPCOUNT - shipsPerLastRow) {
					currentShipsPerRow = shipsPerLastRow;
				}
				shipSelectionArray[i].height = HEIGHT;
				shipSelectionArray[i].width = WIDTH;
				shipSelectionArray[i].setPosition(
					new Vector2(/* X Coordinate */
							   ((i % shipsPerRow * WIDTH)            // Sets each ship side by side from left to right starting from 0
							  + (game.getScreenWidth() / 2)          // Moves all ships towards the center
							  - (WIDTH * (currentShipsPerRow / 2))), // Moves all ships back by half the ships total width
					/* Y Coordinate */
								(i / shipsPerRow * HEIGHT)           // Sets each row from top to bottem starting from 0
							  + ((game.getScreenHeight() / 2) - 50)  // Moves the rows towards the center
							  - (HEIGHT * (shipsPerColumn / 2))));   // Moves the rows back half the rows total height
			}

			confirmButton.setPosition(new Vector2((game.getScreenWidth() - confirmButton.width) / 2,
												  (game.getScreenHeight() - confirmButton.height)));

			/* Sets the default selected ship */
			selectedShip = new UFO(game);
			shipSelectionArray[0].selected = true;
			lastButton = shipSelectionArray[0];

		}


		public Ship getship() {
			return selectedShip;
		}


		public void update() {
			MouseState mouseState = Mouse.GetState();
			confirmButton.update(mouseState);

			for (int i = 0; i < shipSelectionArray.Length; i++) {
				shipSelectionArray[i].update(mouseState);
			}

			/* Allows for feedback of selecting a ship and sets the selected ship */
			for (int i = 0; i < SHIPCOUNT; i++) {
				for (int j = 0; j < SHIPCOUNT; j++) {
					/* Due to how BaseButton works, at most 2 ships will be selected
					 * which results in this logic to compensate for that fact 
					 */
					if (shipSelectionArray[i].selected && shipSelectionArray[j].selected && i != j) {
						if (lastButton == shipSelectionArray[j]) {
							selectedShip = shipSelectionArray[i].getShip();
							shipSelectionArray[j].selected = false;
							lastButton = shipSelectionArray[i];
						} else {
							selectedShip = shipSelectionArray[j].getShip();
							shipSelectionArray[i].selected = false;
							lastButton = shipSelectionArray[j];
						}
					}
				}
			}

			if (confirmButton.isClicked == true) {
				game.GoToMain();
				confirmButton.isClicked = false;
			}

		}


		public void draw(SpriteBatch spriteBatch) {
			spriteBatch.Begin();

			confirmButton.draw(spriteBatch);

			for (int i = 0; i < SHIPCOUNT; i++) {
				shipSelectionArray[i].draw(spriteBatch);
			}

			spriteBatch.End();
		}
	}
}