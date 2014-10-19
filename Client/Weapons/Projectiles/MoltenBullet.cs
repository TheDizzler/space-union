﻿using System;
using Microsoft.Xna.Framework;
using SpaceUnion.Controllers;
using SpaceUnion.Ships;


namespace SpaceUnion.Weapons.Projectiles {
	class MoltenBullet : Projectile {

		public MoltenBullet(Vector2 startPoint, Ship ship)
			: base(assets.moltenBullet, startPoint, ship) {


			projectileTTL = 1.5f;
			projectileMoveSpeed = 500f;

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

			base.doDamage(target, gameTime); // base calls destroy by default
		}


		public override void destroy() {

			explosionEngine.createSmallExplosion(position);
			base.destroy();
		}
	}
}
