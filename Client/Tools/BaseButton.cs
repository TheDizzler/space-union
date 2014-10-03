using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.Tools;


namespace SpaceUnion.Tools {

	class BaseButton : Sprite {

		Rectangle buttonRectangle;

		public bool isClicked = false;
		private bool isDown = false;
		private bool isHovered = false;
		private ButtonState lastState;


		public BaseButton(Texture2D texture, GraphicsDevice graphics)
			: base(texture, Vector2.Zero) {

			width = 300;
			height = 150;
		}


		public void setPosition(Vector2 newPosition) {
			position = newPosition;
			buttonRectangle = new Rectangle((int) position.X, (int) position.Y, (int) width, (int) height);
		}


		public void update(MouseState mouse) {

			ButtonState currentState = mouse.LeftButton;
			Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

			if (isDown && lastState == ButtonState.Pressed && currentState != ButtonState.Pressed)
				isClicked = true;


			if (mouseRectangle.Intersects(buttonRectangle))
				isHovered = true;
			else
				isHovered = false;

			if (isHovered && lastState != ButtonState.Pressed && currentState == ButtonState.Pressed)
				isDown = true;

			if (isDown && !isHovered)
				isDown = false;

			

			lastState = mouse.LeftButton;
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
