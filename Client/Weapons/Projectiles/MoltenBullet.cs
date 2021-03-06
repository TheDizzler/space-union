﻿using System;
using Microsoft.Xna.Framework;
using SpaceUnionXNA.Controllers;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Tools;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace SpaceUnionXNA.Weapons.Projectiles {
	class MoltenBullet : Projectile {

		

		public MoltenBullet(Vector2 startPoint, Ship ship, Game1 game)
			: base(assets.moltenBullet, startPoint, ship, game) {


			projectileTTL = 0.5f;
			projectileMoveSpeed = 500f;

			weaponDamage = 3;
			
		}


		public override List<SoundEffect> getFireSFX() {
			
			List<SoundEffect> fireSFXs = new List<SoundEffect>();
			fireSFXs.Add(assets.laserbolt0);
			fireSFXs.Add(assets.laserbolt1);
			fireSFXs.Add(assets.laserbolt2);
			fireSFXs.Add(assets.laserbolt3);

			return fireSFXs;
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
			explosionEngine.createSmallExplosion(position);
			base.doDamage(target, gameTime); // base calls destroy by default
		}


		public override void destroy() {

			base.destroy();
		}
	}
}
