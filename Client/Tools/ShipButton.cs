using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.Tools;


namespace SpaceUnion.Tools {

	class ShipButton : BaseButton {
		
		public bool selected = false;
		private Ship ship;
        /// <summary>
        /// Constructor for Ship Button
        /// </summary>
        /// edited by Matthew Baldock
        /// <param name="shp"></param>
		public ShipButton(Ship shp)
			: base(shp.texture) {

			ship = shp;
			height = 128;
			width = 128;
		}


		public new void update(MouseState mouse) {

			ButtonState currentState = mouse.LeftButton;
			Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

			if (isDown && lastState == ButtonState.Pressed && currentState != ButtonState.Pressed) {
				isClicked = true;
				selected = true;
			}

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
        /// Draw the ship button
        /// </summary>
        /// Edited by Matthew Baldock
        /// <param name="spriteBatch"></param>
		override public void draw(SpriteBatch spriteBatch) {

			if (isDown)
				spriteBatch.Draw(texture, buttonRectangle, Color.Red);
			else if (isHovered)
				spriteBatch.Draw(texture, buttonRectangle, Color.CadetBlue);
			else if (selected) {
				spriteBatch.Draw(texture, buttonRectangle, Color.Yellow);
			} else
				spriteBatch.Draw(texture, buttonRectangle, Color.White);
		}

		public Ship getShip() {
			return ship;
		}

	}
}
