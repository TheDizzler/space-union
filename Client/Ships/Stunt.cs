using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Weapons;


namespace SpaceUnion.Ships {
	class Stunt : Ship {

		public Stunt(Game1 game1)
			: base(assets.stunt, assets.laser, game1) {

		}

		protected override Projectile getProjectile() {
			return new Laser(Vector2.Add(position, weaponOrigin), this);
		}

		public override void altFire(GameTime gameTime) {
			
		}

	}
}
