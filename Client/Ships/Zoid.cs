﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Weapons.Projectiles;
using SpaceUnionXNA.Weapons.Systems;


namespace SpaceUnionXNA.Ships {
	class Zoid : Ship {

		private Vector2 weaponOrigin2;
		private WeaponSystem mainWeapon2;

		/// <summary>
		/// Set up the ships unique attributes here.
		/// </summary>
		/// <param name="game1"></param>
		public Zoid(Game1 game1)
			: base(assets.zoid, game1) {

			maxSpeed = 7;
			accelSpeed = 70.5f;
			turnSpeed = 4.5f;
			maxSpeed = 400;
			mainFireDelay = TimeSpan.FromSeconds(.2f);
			altFireDelay = TimeSpan.FromSeconds(1f);

			currentHealth = maxHealth = 35;

			mainWeapon = Launcher<Missile>.CreateLauncher(this, (x, y) => new Missile(x, y, game), 4);
			mainWeapon2 = Launcher<Missile>.CreateLauncher(this, (x, y) => new Missile(x, y, game), 4);

			weaponOrigin = new Vector2(position.X - width / 3, position.Y - height / 2);
			weaponOrigin2 = new Vector2(position.X + width / 3, position.Y - height / 2);

			engineOrigins.Add(new Vector2(position.X, position.Y + height / 2));
		}

		protected override void altFire(GameTime gameTime) {

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

	}

}
