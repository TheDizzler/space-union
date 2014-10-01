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

		public bool isClicked = false;
		private bool isDown = false;
		private bool isHovered = false;
		private ButtonState lastState;


		public PlayButton(Texture2D newTexture, GraphicsDevice graphics)
			: base(newTexture, Vector2.Zero) {

			width = 300;
			height = 150;
			//buttonRectangle = new Rectangle((int) position.X, (int) position.Y, (int) size.X, (int) size.Y);
		}


		public void Update(MouseState mouse) {

			ButtonState currentState = mouse.LeftButton;
			Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

			if (mouseRectangle.Intersects(buttonRectangle)) {
				isHovered = true;
			} else
				isHovered = false;

			if (isHovered && currentState == ButtonState.Pressed) {
				isDown = true;
			} else {
				isDown = false;
			}


			if (isHovered && lastState == ButtonState.Pressed && currentState != ButtonState.Pressed)
				isClicked = true;

			lastState = currentState;

		}

		public void setPosition(Vector2 newPosition) {
			position = newPosition;
			buttonRectangle = new Rectangle((int) position.X, (int) position.Y, (int) width, (int) height);
		}

		override public void draw(SpriteBatch spriteBatch) {

			if (isDown)
				spriteBatch.Draw(texture, buttonRectangle, Color.Red);
			else if (isHovered)
				spriteBatch.Draw(texture, buttonRectangle, Color.CadetBlue);
			else
				spriteBatch.Draw(texture, buttonRectangle, Color.Blue);
		}
	}
}
