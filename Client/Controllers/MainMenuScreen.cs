﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceUnion.Tools;


namespace SpaceUnion.Controllers {
using SpaceUnion.Tools;

	class MainMenuScreen {
		
		private Game1 game;
		BaseButton btnPlay;
		BaseButton shipSelect;
        


		public MainMenuScreen(Game1 game) {
			this.game = game;
			btnPlay = new BaseButton(Game1.Assets.playButton);
			btnPlay.setPosition(new Vector2((game.getScreenWidth()  - btnPlay.width)/2, (game.getScreenHeight() - btnPlay.height)/ 2));
            shipSelect = new BaseButton(Game1.Assets.shipselection) {height = 100, width = 300};
			shipSelect.setPosition(new Vector2((game.getScreenWidth() - shipSelect.width)/2, (game.getScreenHeight() - shipSelect.height)));
            
		}

		public void Update() {
			MouseState mouseState = Mouse.GetState();
			btnPlay.update(mouseState);
            shipSelect.update(mouseState);
			if (btnPlay.isClicked == true) {
                btnPlay.isClicked = false;
                game.StartGame();
                btnPlay.isClicked = false;
			}
            if (shipSelect.isClicked == true)
            {
                game.GoToSelect();
                shipSelect.isClicked = false;
            }
		}

		public void draw(SpriteBatch spriteBatch) {

			spriteBatch.Begin();

			btnPlay.draw(spriteBatch);
            shipSelect.draw(spriteBatch);

			spriteBatch.End();
		}

	}
}
