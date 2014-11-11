using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Tools;


namespace SpaceUnionXNA.Weapons.Projectiles {
	public class Missile : Projectile {

		public Missile(Vector2 startPoint, Ship ship)
			: base(assets.missile, startPoint, ship) {


			projectileTTL = .5f;
			projectileMoveSpeed = 2000.2f;

			weaponDamage = 1;

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
			
			explosionEngine.createSmallExplosion(position);
			base.destroy();
		}

	}
}
