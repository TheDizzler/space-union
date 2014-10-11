using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceUnion.Ships;


namespace SpaceUnion.Weapons {
	public class Missle : Projectile {

		public Missle(Vector2 startPoint, Ship ship)
			: base(assets.missile, startPoint, ship) {


			projectileTTL = 1.5f;
			projectileMoveSpeed = 2.2f;

			velocity = new Vector2((float) Math.Sin(rotation) * projectileMoveSpeed,
				(float) -Math.Cos(rotation) * projectileMoveSpeed);

			//velocity += ship.velocity / 1000;

			projectileDamage = 3;

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
			//isActive = false;
			explosionEngine.createExplosion(position);
		}

	}
}
