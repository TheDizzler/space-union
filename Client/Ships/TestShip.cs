using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Weapons;


namespace SpaceUnion.Weapons {
	class TestShip : Ship {

		public TestShip(Game1 game1)
			: base(assets.shuttle, assets.missile, game1) {

		}

		protected override Projectile getProjectile() {
			return new Missle(Vector2.Add(position, weaponOrigin), this);
		}

		public override void altFire(GameTime gameTime) {
			
		}

	}
}
