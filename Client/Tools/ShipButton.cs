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


		public ShipButton(Texture2D newTexture)
			: base(newTexture) {


		}



		override public void draw(SpriteBatch spriteBatch) {

			if (isDown)
				spriteBatch.Draw(texture, buttonRectangle, Color.Red);
			else if (isHovered)
				spriteBatch.Draw(texture, buttonRectangle, Color.CadetBlue);
			else if (selected) {
				spriteBatch.Draw(texture, buttonRectangle, Color.Yellow);
			} else
				spriteBatch.Draw(texture, buttonRectangle, Color.Blue);
		}
	}
}
