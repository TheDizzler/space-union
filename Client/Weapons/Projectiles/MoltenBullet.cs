using System;
using Microsoft.Xna.Framework;
using SpaceUnion.Ships;


namespace SpaceUnion.Weapons.Projectiles {
	class MoltenBullet : Projectile {

		public MoltenBullet(Vector2 startPoint, Ship ship)
			: base(assets.moltenBullet, startPoint, ship) {


			projectileTTL = 1.5f;
			projectileMoveSpeed = .2f;

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

			base.doDamage(target, gameTime); // base calls destroy be default
		}


		public override void destroy() {
			//isActive = false;
			explosionEngine.createSmallExplosion(position);
		}
	}
}
