using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceUnion.Ships {
	class Zoid : Ship {

		public Zoid(Game1 game1)
			: base(assets.zoid, assets.missile, game1) {


		}

		public override void altFire(GameTime gameTime) {
			
		}

	}
}
