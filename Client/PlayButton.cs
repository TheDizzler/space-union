using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.Tools;


namespace SpaceUnion {
	class PlayButton : Sprite {

		Rectangle buttonRectangle;
		public Vector2 size;

		public bool isClicked = false;

		public PlayButton(Texture2D newTexture, GraphicsDevice graphics)
			: base(newTexture, Vector2.Zero) {

			texture = newTexture;
			size = new Vector2(300, 150);
		}
		

		public void Update(MouseState mouse) {
			buttonRectangle = new Rectangle((int) position.X, (int) position.Y, (int) size.X, (int) size.Y);
			Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
			if (mouse.LeftButton == ButtonState.Pressed
				&& mouseRectangle.Intersects(buttonRectangle)) {

				isClicked = true;
			} else {
				isClicked = false;
			}



		}

		public void setPosition(Vector2 newPosition) {
			position = newPosition;
		}

		public void draw(SpriteBatch spriteBatch) {
			spriteBatch.Draw(texture, buttonRectangle, Color.Blue);
		}
	}
}
