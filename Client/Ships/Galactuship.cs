using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Weapons.Projectiles;
using SpaceUnionXNA.Weapons.Systems;

namespace SpaceUnionXNA.Ships {
	class Galactuship : Ship {

		private Vector2 weaponOrigin2;
		private WeaponSystem mainWeapon2;

		public Galactuship(Game1 game1)
			: base(assets.galactusship, game1) {


			// description = "Fast and manuverable, the scout ship is ideal for hit and runs. Its homing missiles ";

			currentHealth = maxHealth = 40;

			accelSpeed = 150f;
			turnSpeed = 1.5f;
			maxSpeed = 200;

			mainFireDelay = TimeSpan.FromSeconds(.2f);
			altFireDelay = TimeSpan.FromSeconds(1f);

			mainWeapon = Launcher<Missile>.CreateLauncher(this, (x, y) => new Missile(x, y), 4);
			mainWeapon2 = Launcher<Missile>.CreateLauncher(this, (x, y) => new Missile(x, y), 4);

			weaponOrigin = new Vector2(position.X - width / 3, position.Y - height / 2);
			weaponOrigin2 = new Vector2(position.X + width / 3, position.Y - height / 2);

			engineOrigins.Add(new Vector2(position.X - width / 8, position.Y + height / 2.5f));
			engineOrigins.Add(new Vector2(position.X + width / 8, position.Y + height / 2.5f));

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
			mainWeapon2.update(gameTime, quadTree);
		}


		protected override void additionalDraw(SpriteBatch sBatch) {
			mainWeapon2.draw(sBatch);
		}

		protected override void additionalFire(GameTime gameTime) {

			mainWeapon2.fire(Vector2.Add(position, weaponOrigin2));
		}

		protected override void altFire(GameTime gameTime) {
			throw new NotImplementedException();
		}
	}
}
