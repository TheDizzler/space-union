using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Weapons.Projectiles;
using SpaceUnionXNA.Weapons.Systems;


namespace SpaceUnionXNA.Ships {
	class Scout : Ship {

		private Vector2 weaponOrigin2;
		private WeaponSystem mainWeapon2;

		public Scout(Game1 game1)
			: base(assets.scout, game1) {

			description = "Fast and manuverable, the scout ship is ideal for hit-and-runs. Its homing missiles have enemy recognition but lack sophisticated target selection.";

			currentHealth = maxHealth = 30;

			accelSpeed = 250f;
			turnSpeed = 2.5f;
			maxSpeed = 500;

			mainWeapon = new HomingLauncher(this);
			mainWeapon2 = new HomingLauncher(this);

			weaponOrigin = new Vector2(position.X - 25, position.Y + 10);
			weaponOrigin2 = new Vector2(position.X + 25, position.Y + 10);

			engineOrigins.Add(new Vector2(position.X - width / 4, position.Y + height / 2));
			engineOrigins.Add(new Vector2(position.X + width / 4, position.Y + height / 2));
		}



		/// <summary>
		/// Rotate where the weapon projectile originates from.
		/// </summary>
		/// <param name="rotateAmount"></param>
		protected override void rotateWeaponOrigin(float rotateAmount) {

			Matrix transform = getWeaponOriginTransform(rotateAmount);

			Vector2.TransformNormal(ref weaponOrigin, ref transform, out weaponOrigin);
			Vector2.TransformNormal(ref weaponOrigin2, ref transform, out weaponOrigin2);

			for (int i = 0; i < engineOrigins.Count; ++i) {
				Vector2 temp = engineOrigins[i];
				Vector2.TransformNormal(ref temp, ref transform, out temp);
				engineOrigins[i] = temp;
			}
		}



		protected override void additionalUpdate(GameTime gameTime, QuadTree quadTree) {
			mainWeapon2.updatePosition(Vector2.Add(position, weaponOrigin2), rotation);
			mainWeapon.updatePosition(Vector2.Add(position, weaponOrigin), rotation);


			mainWeapon2.update(gameTime, quadTree);
		}


		protected override void additionalDraw(SpriteBatch sBatch) {
			mainWeapon2.draw(sBatch);
		}

		protected override void additionalFire(GameTime gameTime) {

			mainWeapon2.fire(Vector2.Add(position, weaponOrigin2));
		}

		protected override void altFire(GameTime gameTime) {

		}

	}
}
