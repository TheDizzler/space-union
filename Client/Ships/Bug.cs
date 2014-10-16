using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Weapons;


namespace SpaceUnion.Ships {
	class Bug : Ship {

		public Bug(Game1 game1)
			: base(assets.bug, assets.moltenBullet, game1) {

			maxSpeed = 100;
			accelSpeed = 50.0f;
			turnSpeed = 4.5f;

			mainFireDelay = TimeSpan.FromSeconds(.5f);
			altFireDelay = TimeSpan.FromSeconds(1f);

			weaponOrigin = new Vector2(position.X, position.Y - height / 2); // start position of weapon
		}


		protected override Projectile getProjectile() {
			return new MoltenBullet(Vector2.Add(position, weaponOrigin), this);
		}

		public override void altFire(GameTime gameTime) {

		}

	}
}
