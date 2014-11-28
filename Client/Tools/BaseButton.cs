using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnionXNA.Tools;

///Edited by Matthew Baldock
///Edited by Triston Gillon
namespace SpaceUnionXNA.Tools {

	public class BaseButton : Sprite {

		protected Rectangle buttonRectangle;

		public bool isClicked = false;
		protected bool isDown = false;
		protected bool isHovered = false;
		protected ButtonState lastState;

        public bool hover()
        {
            return isHovered;
        }

        /// <summary>
        /// Constructor for BaseButton, uses Texture in AssetsManager and a Vector2 value
        /// </summary>
        /// <param name="texture"></param>
		public BaseButton(Texture2D texture)
			: base(texture, Vector2.Zero) {

			width = 300;
			height = 150;
		}

        /// <summary>
        /// Sets position in viewport of button
        /// </summary>
        /// <param name="newPosition"></param>
		public void setPosition(Vector2 newPosition) {
			position = newPosition;
			buttonRectangle = new Rectangle((int) position.X, (int) position.Y, (int) width, (int) height);
		}

        /// <summary>
        /// Updates the button based on mouse interactions
        /// </summary>
        /// <param name="mouse"></param>
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

        /// <summary>
        /// Draws button base on its current state
        /// </summary>
        /// <param name="spriteBatch"></param>
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
