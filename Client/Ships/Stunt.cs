using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceUnion.Ships {
	class Stunt : Ship {

		public Stunt(Vector2 pos, Game1 game1)
			: base(assets.stunt, assets.laser, pos, game1) {

		}

		public override void altFire(GameTime gameTime) {
			
		}

	}
}
