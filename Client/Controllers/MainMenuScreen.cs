using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceUnion {
	class MainMenuScreen {
		private Texture2D texture;
		private Game1 game;
		PlayButton btnPlay;

		public MainMenuScreen(Game1 game) {
			this.game = game;
			texture = game.Content.Load<Texture2D>("Buttons/playbutton");
			btnPlay = new PlayButton(Game1.Assets.playButton, game.GraphicsDevice);
			btnPlay.setPosition(new Vector2((game.getScreenWidth()  - btnPlay.width)/2, (game.getScreenHeight() - btnPlay.height)/ 2));
		}

		public void Update() {
			MouseState mouseState = Mouse.GetState();
			btnPlay.Update(mouseState);

			if (btnPlay.isClicked == true) {
                btnPlay.isClicked = false;
                game.StartGame();
			}
		}

		public void draw(SpriteBatch spriteBatch) {

			spriteBatch.Begin();

			btnPlay.draw(spriteBatch);

			spriteBatch.End();
		}

	}
}
