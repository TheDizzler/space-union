using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;
using SpaceUnion.Weapons;
using SpaceUnion.Weapons.Projectiles;
using SpaceUnion.Weapons.Systems;


namespace SpaceUnion.Ships {
	class UFO : Ship {

		Shield shield;


		public UFO(Game1 game)
			: base(assets.ufo, assets.laser, game) {

			accelSpeed = 500.5f;
			turnSpeed = 4.5f;
			maxSpeed = 500;
			mainFireDelay = TimeSpan.FromSeconds(.5f);
			altFireDelay = TimeSpan.FromSeconds(1f);
			shield = new Shield(assets.shield, position);

			weaponOrigin = new Vector2(position.X, position.Y - height / 2); // start position of weapon
			mainWeapon = Launcher<Laser>.CreateLauncher(this, (x, y) => new Laser(x, y), 8);

		}


		public override void update(GameTime gameTime, QuadTree quadTree) {

			base.update(gameTime, quadTree);
			shield.update(gameTime, position);
		}

		public override void draw(SpriteBatch batch) {

			base.draw(batch);

			if (shield.on)
				shield.draw(batch);
		}

		//protected override Projectile getProjectile() {
		//	return new Laser(Vector2.Add(position, weaponOrigin), this);
		//}

		public override void altFire(GameTime gameTime) {

			if (gameTime.TotalGameTime - previousAltFireTime > altFireDelay) {

				previousAltFireTime = gameTime.TotalGameTime;
				shield.on = !shield.on;
			}
		}


		protected override void additionalUpdate(GameTime gameTime, QuadTree quadTree) {

		}

		protected override void additionalDraw(SpriteBatch sBatch) {

		}

		protected override void additionalFire(GameTime gameTime) {

		}

	}
}
