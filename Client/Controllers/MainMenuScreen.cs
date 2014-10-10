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
        public static string username = "troy";
        public static string password = "loltroy";
        public static ClientCommHandler clientCommHandler = new ClientCommHandler();
        public static Player player;

		public MainMenuScreen(Game1 game) {
			this.game = game;
			btnPlay = new PlayButton(Game1.Assets.playButton, game.GraphicsDevice);
			btnPlay.setPosition(new Vector2((game.getScreenWidth()  - btnPlay.width)/2, (game.getScreenHeight() - btnPlay.height)/ 2));
            
            //NETWORKING
            Player player = new Player();
            player.Username = MainMenuScreen.username;
            player.Password = MainMenuScreen.password;
            player.IPAddress = ClientCommHandler.getLocalIPv4Address();
            clientCommHandler.sendLoginRequest(player);
            player = clientCommHandler.getLoginConfirmation();
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
