using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Weapons;


namespace SpaceUnion.Ships {
	class Zoid : Ship {

		Vector2 weaponOrigin2;

		/// <summary>
		/// Set up the ships unique attributes here.
		/// </summary>
		/// <param name="game1"></param>
		public Zoid(Game1 game1)
			: base(assets.zoid, assets.missile, game1) {

			maxSpeed = 7;
			accelSpeed = 4.5f;
			turnSpeed = 4.5f;

			mainFireDelay = TimeSpan.FromSeconds(.2f);
			altFireDelay = TimeSpan.FromSeconds(1f);

			weaponOrigin = new Vector2(position.X - width / 3, position.Y - height / 2);
			weaponOrigin2 = new Vector2(position.X + width / 3, position.Y - height / 2);
		}


		public override void fire(GameTime gameTime) {

			// Fire only every interval we set as the fireTime
			if (gameTime.TotalGameTime - previousMainFireTime > mainFireDelay) {
				// Reset our current time
				previousMainFireTime = gameTime.TotalGameTime;

				projectiles.Add(new Missle(Vector2.Add(position, weaponOrigin), this));
				projectiles.Add(new Missle(Vector2.Add(position, weaponOrigin2), this));
			}
		}


		public override void altFire(GameTime gameTime) {

		}


		/// <summary>
		/// Rotate where the weapon projectile originates from.
		/// </summary>
		/// <param name="rotateAmount"></param>
		protected override void rotateWeaponOrigin(float rotateAmount) {
			Matrix transform = getWeaponOriginTransform(rotateAmount);

			Vector2.TransformNormal(ref weaponOrigin, ref transform, out weaponOrigin);
			Vector2.TransformNormal(ref weaponOrigin2, ref transform, out weaponOrigin2);
		}


	}



}
