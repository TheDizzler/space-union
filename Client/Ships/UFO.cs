using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Weapons.Projectiles;
using SpaceUnionXNA.Weapons.Systems;


namespace SpaceUnionXNA.Ships {
	class UFO : Ship {

		Shield shield;


		public UFO(Game1 game)
			: base(assets.ufo, game) {

			description = "Fast acceleration and quick turning make the UFO the most agile ship. However, its weak armor and bullets means it has to stay one step ahead of its enemies.";


			currentHealth = maxHealth = 20;
			accelSpeed = 1000f;
			turnSpeed = 4.5f;
			maxSpeed = 300;

			mainFireDelay = TimeSpan.FromSeconds(.15f);
			altFireDelay = TimeSpan.FromSeconds(1f);
			shield = new Shield(assets.shield, position);

			mainWeapon = Launcher<MoltenBullet>.CreateLauncher(this, (x, y) => new MoltenBullet(x, y, game), 3);
			weaponOrigin = new Vector2(position.X, position.Y - height / 2); // start position of weapon

			engineOrigins.Add(new Vector2(position.X - width / 4, position.Y + height / 2));
			engineOrigins.Add(new Vector2(position.X + width / 4, position.Y + height / 2));
		}


		public override void update(GameTime gameTime, QuadTree quadTree) {

			base.update(gameTime, quadTree);
			if (altFiring)
				shield.update(gameTime, position);
			else
				shield.on = false;
		}

		public override void draw(SpriteBatch batch) {

			base.draw(batch);
			if (shield.on)
				shield.draw(batch);
		}


		protected override void altFire(GameTime gameTime) {

			if (gameTime.TotalGameTime - previousAltFireTime > altFireDelay) {

				previousAltFireTime = gameTime.TotalGameTime;
				shield.on = true;
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
