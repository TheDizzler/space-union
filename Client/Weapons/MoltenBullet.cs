using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceUnion.Weapons {
	class MoltenBullet : Projectile {

		public MoltenBullet(Vector2 startPoint, Ship ship)
			: base(assets.moltenBullet, startPoint, ship) {


			projectileTTL = .5f;
			projectileMoveSpeed = .2f;

			//Vector2 projVel = new Vector2((float) Math.Sin(rotation) * projectileMoveSpeed,
			//	(float) -Math.Cos(rotation) * projectileMoveSpeed);
			//Vector2.Add(ref velocity, ref projVel, out velocity);
			//velocity += new Vector2((float) Math.Sin(rotation) * projectileMoveSpeed,
			//	(float) -Math.Cos(rotation) * projectileMoveSpeed);


			velocity = new Vector2((float) Math.Sin(rotation) * projectileMoveSpeed,
				(float) -Math.Cos(rotation) * projectileMoveSpeed);

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
