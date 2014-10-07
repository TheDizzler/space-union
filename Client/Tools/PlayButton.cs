using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceUnion.Tools {

	class PlayButton : Sprite {

		Rectangle buttonRectangle;

		public bool isClicked = false;
		private bool isDown = false;
		private bool isHovered = false;
		private ButtonState lastState;


		public PlayButton(Texture2D newTexture, GraphicsDevice graphics)
			: base(newTexture, Vector2.Zero) {

			width = 300;
			height = 150;
		}


	}
}
