using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data_Structures;
using Client_Comm_Module;

namespace SpaceUnion {
	class MainMenuScreen {
		//private Texture2D texture;
		private Game1 game;
		PlayButton btnPlay;
        public static string username = "andrew";
        public static string password = "lolandrew";
        

		public MainMenuScreen(Game1 game) {
			this.game = game;
			btnPlay = new PlayButton(Game1.Assets.playButton, game.GraphicsDevice);
			btnPlay.setPosition(new Vector2((game.getScreenWidth()  - btnPlay.width)/2, (game.getScreenHeight() - btnPlay.height)/ 2));
		}

		public void Update() {
			MouseState mouseState = Mouse.GetState();
			btnPlay.Update(mouseState);

			if (btnPlay.isClicked == true) {
                btnPlay.isClicked = false;
                //NETWORKING TODO: Login request

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
