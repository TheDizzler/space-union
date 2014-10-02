using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.Tools;

namespace SpaceUnion {
	class MainMenuScreen {
		private Texture2D texture;
		private Game1 game;
		PlayButton btnPlay;
        GeneralButton shipSelect;
        


		public MainMenuScreen(Game1 game) {
			this.game = game;
			btnPlay = new PlayButton(Game1.Assets.playButton, game.GraphicsDevice);
			btnPlay.setPosition(new Vector2((game.getScreenWidth()  - btnPlay.width)/2, (game.getScreenHeight() - btnPlay.height)/ 2));
            shipSelect = new GeneralButton(Game1.Assets.confirm, game.GraphicsDevice);
            shipSelect.height = 100;
            shipSelect.width = 300;
            shipSelect.setPosition(new Vector2((game.getScreenWidth() - shipSelect.width)/2, (game.getScreenHeight() - shipSelect.height)));
            
		}

		public void Update() {
			MouseState mouseState = Mouse.GetState();
			btnPlay.Update(mouseState);
            shipSelect.Update(mouseState);
			if (btnPlay.isClicked == true) {
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
