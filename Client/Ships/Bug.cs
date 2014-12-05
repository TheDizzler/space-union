using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Weapons.Projectiles;
using SpaceUnionXNA.Weapons.Systems;


namespace SpaceUnionXNA.Ships {
	/// <summary>
	/// A tiny ship.
	/// @Written by Tristan.
	/// </summary>
	class Bug : Ship {

		public Bug(Game1 game1)
			: base(assets.bug, game1) {

			maxSpeed = 300;
			accelSpeed = 100.0f;
			turnSpeed = 9.5f;

			currentHealth = maxHealth = 5;

			mass = 500;

			mainFireDelay = TimeSpan.FromSeconds(.5f);
			altFireDelay = TimeSpan.FromSeconds(1f);

			mainWeapon = Launcher<MoltenBullet>.CreateLauncher(this, (x, y) => new MoltenBullet(x, y, game), 2);
			weaponOrigin = new Vector2(position.X, position.Y - height / 2); // start position of weapon

			engineOrigins.Add(new Vector2(position.X - width / 4, position.Y + height / 2));
			engineOrigins.Add(new Vector2(position.X + width / 4, position.Y + height / 2));
		}


		protected override void altFire(GameTime gameTime) {

		}



		protected override void additionalUpdate(GameTime gameTime, QuadTree quadTree) {

		}

		protected override void additionalDraw(SpriteBatch sBatch) {

		}

		protected override void additionalFire(GameTime gameTime) {

		}

		

	}
}
