using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.Tools;


namespace SpaceUnion {
	class PlayButton : SpaceUnion.Tools.GeneralButton {

		


		public PlayButton(Texture2D newTexture, GraphicsDevice graphics)
			: base(newTexture, graphics) {

			//size = new Vector2(300, 150);
			width = 300;
			height = 150;
			//buttonRectangle = new Rectangle((int) position.X, (int) position.Y, (int) size.X, (int) size.Y);
		}


	}
}
