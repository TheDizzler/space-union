using System;
using Microsoft.Xna.Framework;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Tools;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace SpaceUnionXNA.Weapons.Projectiles {
	public class Laser : Projectile {

		public Laser(Vector2 startPoint, Ship ship)
			: base(assets.laser, startPoint, ship) {

			projectileTTL = 20;
			projectileMoveSpeed =500.0f;

			weaponDamage = 25;
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

			base.doDamage(target, gameTime);
		}


		public override void destroy() {
			explosionEngine.createSmallExplosion(position);
			base.destroy();
		}

	}
}
