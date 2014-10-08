using Microsoft.Xna.Framework;
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
		//private Texture2D texture;
		Game1 game;
		ShipButton selectShip1;
		ShipButton selectShip2;
		ShipButton selectShip3;
		ShipButton selectShip4;
		BaseButton confirmButton;
		ShipButton lastbutton;
		Ship selectedship;
		

		public ShipSelectionScreen(Game1 game) {
			this.game = game;
			selectShip1 = new ShipButton(Game1.Assets.ufo);
			selectShip2 = new ShipButton(Game1.Assets.stunt);
			selectShip3 = new ShipButton(Game1.Assets.zoid);
			selectShip4 = new ShipButton(Game1.Assets.shuttle);
			confirmButton = new BaseButton(Game1.Assets.confirm) { height = 100, width = 300 };
			
			confirmButton.setPosition(new Vector2((game.getScreenWidth() - confirmButton.width) / 2, (game.getScreenHeight() - confirmButton.height)));
			selectShip1.setPosition(new Vector2((game.getScreenWidth() - (selectShip1.width * 4)) / 2, (selectShip1.height)));
			selectShip2.setPosition(new Vector2((game.getScreenWidth() - (selectShip2.width * 2)) / 2, (selectShip2.height)));
			selectShip3.setPosition(new Vector2((game.getScreenWidth() + (selectShip3.width / 2)) / 2, (selectShip3.height)));
			selectShip4.setPosition(new Vector2((game.getScreenWidth() + (selectShip4.width * 3)) / 2, (selectShip4.height)));
			selectedship = new UFO(new Vector2(200, 200), game);
			selectShip1.selected = true;
			lastbutton = selectShip1;

		}


		public Ship getship() {
			return selectedship;
		}


		public void Update() {


			MouseState mouseState = Mouse.GetState();
			confirmButton.update(mouseState);
			selectShip1.update(mouseState);
			selectShip2.update(mouseState);
			selectShip3.update(mouseState);
			selectShip4.update(mouseState);


			if (selectShip1.selected && selectShip2.selected) {
				if (lastbutton == selectShip2) {

					selectedship = new UFO(new Vector2(200, 200), game);
					selectShip2.selected = false;
					selectShip3.selected = false;
					selectShip4.selected = false;
					lastbutton = selectShip1;
				} else {
					selectedship = new Stunt(new Vector2(200, 200), game);
					selectShip1.selected = false;
					selectShip3.selected = false;
					selectShip4.selected = false;
					lastbutton = selectShip2;
				}


			}
			if (selectShip1.selected && selectShip3.selected) {
				if (lastbutton == selectShip3) {

					selectedship = new UFO(new Vector2(200, 200), game);
					selectShip2.selected = false;
					selectShip3.selected = false;
					selectShip4.selected = false;
					lastbutton = selectShip1;
				} else {
					selectedship = new Zoid(new Vector2(200, 200), game);
					selectShip1.selected = false;
					selectShip2.selected = false;
					selectShip4.selected = false;
					lastbutton = selectShip3;
				}


			}
			if (selectShip1.selected && selectShip4.selected) {
				if (lastbutton == selectShip4) {

					selectedship = new UFO(new Vector2(200, 200), game);
					selectShip3.selected = false;
					selectShip4.selected = false;
					lastbutton = selectShip1;
				} else {
					selectedship = new TestShip(new Vector2(200, 200), game);
					selectShip1.selected = false;
					selectShip3.selected = false;
					selectShip2.selected = false;
					lastbutton = selectShip4;
				}


			}
			if (selectShip2.selected && selectShip3.selected) {
				if (lastbutton == selectShip3) {
					selectedship = new Stunt(new Vector2(200, 200), game);
					selectShip1.selected = false;
					selectShip3.selected = false;
					selectShip4.selected = false;
					lastbutton = selectShip2;
				} else {
					selectedship = new Zoid(new Vector2(200, 200), game);
					selectShip1.selected = false;
					selectShip2.selected = false;
					selectShip4.selected = false;
					lastbutton = selectShip3;
				}
			}
			if (selectShip2.selected && selectShip4.selected) {
				if (lastbutton == selectShip4) {
					selectedship = new Stunt(new Vector2(200, 200), game);
					selectShip1.selected = false;
					selectShip3.selected = false;
					selectShip4.selected = false;
					lastbutton = selectShip2;
				} else {
					selectedship = new TestShip(new Vector2(200, 200), game);
					selectShip1.selected = false;
					selectShip2.selected = false;
					selectShip3.selected = false;
					lastbutton = selectShip4;
				}
			}

			if (selectShip3.selected && selectShip4.selected) {
				if (lastbutton == selectShip4) {
					selectedship = new Zoid(new Vector2(200, 200), game);
					selectShip1.selected = false;
					selectShip2.selected = false;
					selectShip4.selected = false;
					lastbutton = selectShip3;
				} else {
					selectedship = new TestShip(new Vector2(200, 200), game);

					selectShip1.selected = false;
					selectShip3.selected = false;
					selectShip2.selected = false;
					lastbutton = selectShip4;
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
			selectShip1.draw(spriteBatch);
			selectShip2.draw(spriteBatch);
			selectShip3.draw(spriteBatch);
			selectShip4.draw(spriteBatch);

			spriteBatch.End();
		}
	}
}
