using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Tools;

namespace SpaceUnionXNA.Controllers {
	/// <summary>
	/// Ship selection screen. Currently accessed from the main menu and
	/// returns to the main menu.
	/// Created by Matthew Baldock
	/// Edited by Steven Chen
	/// </summary>
	class ShipSelectionScreen {

		Game1 game;
		ShipButton[] shipSelectionArray;
		ShipButton lastButton;
		BaseButton confirmButton;
		Ship selectedShip;
		Rectangle selectShipRect;
		Rectangle hoverShipRect;
       // Rectangle confirmShipRect;
		Rectangle selectShipTextureRect;
		Rectangle hoverShipTextureRect;
		Texture2D guiRectangle;
		Texture2D selectShipTexture;
		Texture2D hoverShipTexture;

		/* Default size of the ships */
		const int WIDTH     = 128;
		const int HEIGHT    = 128;
		const int SHIPCOUNT = 4;   // Change value according to how many different ships are available
		int shipsPerRow;


		int totalWidth;
		int totalHeight;

		int shipSelectWidth;
		int shipSelectHeight;

		int shipDescWidth;
		int shipDescHeight;


		/// <summary>
		/// Constructor for Ship Selection Screen
		/// </summary>
		/// Created by Matthew Baldock
		/// <param name="game"></param>
		public ShipSelectionScreen(Game1 game) {
			guiRectangle = Game1.Assets.guiRectangle;
			this.game = game;

			totalWidth = game.getScreenWidth();
			totalHeight = game.getScreenHeight();

			shipSelectWidth = totalWidth;
			shipSelectHeight = (int) (totalHeight * 0.70);

			shipDescWidth = (int) (totalWidth * 0.375);
			shipDescHeight = (int) (totalHeight * 0.3);

			shipSelectionArray = new ShipButton[SHIPCOUNT];
			/* Actual ships used; commented out to test other functions */
			shipSelectionArray[0] = new ShipButton(new UFO(game));
			shipSelectionArray[1] = new ShipButton(new Scout(game));
			shipSelectionArray[2] = new ShipButton(new Zoid(game));
			shipSelectionArray[3] = new ShipButton(new TestShip(game));

			/* For Testing X amount of ships; Remove */
			//for (int i = 0; i < SHIPCOUNT; i++)
			//{
			//	shipSelectionArray[i] = new ShipButton(Game1.Assets.ufo);
			//}

			confirmButton = new BaseButton(Game1.Assets.confirm) { height = 100, width = 200 };

			setGridDisplay(shipSelectWidth, shipSelectHeight);
			shipDescDisplay(totalHeight - shipDescHeight, shipDescWidth, shipDescHeight);
			confirmButton.setPosition(new Vector2((int) (totalWidth * 0.75),
												  (int) (shipSelectHeight + 100)));

			/* Sets the default selected ship */
			selectedShip = new Bug(game);
			displaySelectedShip(selectedShip);
			hoverShipTexture = selectedShip.texture;
			shipSelectionArray[0].selected = true;
			lastButton = shipSelectionArray[0];

		}

        /// <summary>
        /// Get ship for gameplay screen
        /// </summary>
        /// 
        /// Created by Matthew Baldock
        /// <returns></returns>

        public void setScrollDisplay()
        {

		}

		/// <summary>
		/// Sets the ships in a grid fashion
		/// </summary>
		public void setGridDisplay(int screenWidth, int screenHeight) {
			shipsPerRow = (screenWidth - (WIDTH * 2)) / WIDTH;
			float currentShipsPerRow = shipsPerRow;
			int shipsPerLastRow = (int) (SHIPCOUNT % shipsPerRow);
			float shipsPerColumn = (float) Math.Ceiling((float) (SHIPCOUNT / shipsPerRow));

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
							  + (screenWidth / 2)                    // Moves all ships towards the center
							  - (WIDTH * (currentShipsPerRow / 2))), // Moves all ships back by half the ships total width
					/* Y Coordinate */
								(i / shipsPerRow * HEIGHT)           // Sets each row from top to bottem starting from 0
							  + ((screenHeight / 2) - 75)           // Moves the rows towards the center
							  - (HEIGHT * (shipsPerColumn / 2))));   // Moves the rows back half the rows total height
			}
		}

		public void shipDescDisplay(int yPos, int width, int height) {
			selectShipRect = new Rectangle(0, yPos, width, height);
			hoverShipRect = new Rectangle(width, yPos, width, height);

		}


		/// <summary>
		/// Get ship for gameplay screen
		/// </summary>
		/// 
		/// Created by Matthew Baldock
		/// <returns></returns>
		public Ship getship() {
			return selectedShip;
		}


        /// <summary>
        /// Update the screen
        /// </summary>
        /// Created by Matthew Baldock
        /// Edited by Steven Chen

        public void displaySelectedShip(Ship ship)
        {
            selectShipTexture = ship.texture;
            selectShipTextureRect = new Rectangle(10, shipSelectHeight + (shipDescHeight / 2) - (HEIGHT / 2), 128, 128);
        }

		public void displayHoverShip(Ship ship) {
			hoverShipTexture = ship.texture;
			hoverShipTextureRect = new Rectangle(shipDescWidth + 10, shipSelectHeight + (shipDescHeight / 2) - (HEIGHT / 2), 128, 128);
		}

		/// <summary>
		/// Update the screen
		/// </summary>
		/// Created by Matthew Baldock
		/// Edited by Steven Chen
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
						displaySelectedShip(selectedShip);
					}
					if (shipSelectionArray[i].hover()) {
						displayHoverShip(shipSelectionArray[i].getShip());
					}
				}
			}

			if (confirmButton.isClicked == true) {
				game.EnterLoginMenu();
				confirmButton.isClicked = false;
			}

		}

		/// <summary>
		/// Draw buttons on to the view
		/// </summary>
		/// 
		/// Created by Matthew Baldock
		/// <param name="spriteBatch"></param>
		public void draw(SpriteBatch spriteBatch) {
			spriteBatch.Begin();

			spriteBatch.Draw(guiRectangle, selectShipRect, Color.Yellow);
			spriteBatch.Draw(guiRectangle, hoverShipRect, Color.CadetBlue);
           // spriteBatch.Draw(guiRectangle, confirmShipRect, Color.Green);
			spriteBatch.Draw(selectShipTexture, selectShipTextureRect, Color.White);
			spriteBatch.Draw(hoverShipTexture, hoverShipTextureRect, Color.White);
			confirmButton.draw(spriteBatch);

			for (int i = 0; i < SHIPCOUNT; i++) {
				shipSelectionArray[i].draw(spriteBatch);
			}

			spriteBatch.End();
		}
	}
}
