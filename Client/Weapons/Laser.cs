using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Ships;


namespace SpaceUnion.Weapons {
	class Laser : Projectile {

		public Laser(Vector2 startPoint, Ship ship)
			: base(assets.laser, startPoint, ship) {

			projectileTTL = 5;
			projectileMoveSpeed = 1.2f;

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
			
			base.doDamage(target, gameTime);
		}


		public override void destroy() {
			//isActive = false; //Still haveing a self player collision issue
		}

	}
}
