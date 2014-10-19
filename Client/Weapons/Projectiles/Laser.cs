using System;
using Microsoft.Xna.Framework;
using SpaceUnion.Ships;


namespace SpaceUnion.Weapons.Projectiles {
	class Laser : Projectile {

		public Laser(Vector2 startPoint, Ship ship)
			: base(assets.laser, startPoint, ship) {

			projectileTTL = 5;
			projectileMoveSpeed = 1000.2f;


			weaponDamage = 5;

		}

		/// <summary>
		/// Add unique collision behaviour here
		/// </summary>
		/// <param name="target"></param>
		/// <param name="gameTime"></param>
		public override void collide(Tangible target, GameTime gameTime) {

			base.collide(target, gameTime);
		}

		/// <summary>
		/// Add unique damage behaviour here
		/// </summary>
		/// <param name="target"></param>
		/// <param name="gameTime"></param>
		public override void doDamage(Tangible target, GameTime gameTime) {

			base.doDamage(target, gameTime);
		}


		public override void destroy() {
			base.destroy();
		}

	}
}
