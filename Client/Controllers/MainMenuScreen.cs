using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceUnion.Tools;


namespace SpaceUnion.Controllers {

	class MainMenuScreen {
		
		private Game1 game;
		BaseButton btnPlay;

		public MainMenuScreen(Game1 game) {
			this.game = game;
			btnPlay = new BaseButton(Game1.Assets.playButton, game.GraphicsDevice);
			btnPlay.setPosition(new Vector2((game.getScreenWidth()  - btnPlay.width)/2, (game.getScreenHeight() - btnPlay.height)/ 2));
		}

		public void Update() {
			MouseState mouseState = Mouse.GetState();
			btnPlay.update(mouseState);

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
