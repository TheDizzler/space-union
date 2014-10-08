using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceUnion.Ships {
	class Zoid : Ship {

		public Zoid(Vector2 pos, Game1 game1)
			: base(assets.zoid, assets.missile, pos, game1) {


		}

	}
}
