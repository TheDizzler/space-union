using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion {

	public class MapIcon : Sprite {

		public MapIcon(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			scale = 6.0f;
		}


		public void draw(Vector2 pos, SpriteBatch sBatch) {

			position = pos;
			base.draw(sBatch);
		}

	}
}
