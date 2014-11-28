using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Weapons.Projectiles;
using SpaceUnionXNA.Weapons.Systems;


namespace SpaceUnionXNA.Ships {
	class Galactus : Ship {

		public Galactus(Game1 game1)
			: base(assets.galactusship, assets.moltenBullet, game1) {

			maxSpeed = 100;
			accelSpeed = 20.0f;
			turnSpeed = 1.0f;

			currentHealth = maxHealth = 10;

			mass = 1000;

			mainFireDelay = TimeSpan.FromSeconds(.5f);
			altFireDelay = TimeSpan.FromSeconds(1f);

            mainWeapon = Launcher<MoltenBullet>.CreateLauncher(this, (x, y) => new MoltenBullet(x, y), 8);
			weaponOrigin = new Vector2(position.X, position.Y - height / 2); // start position of weapon
		}


		/// <summary>
		/// Main weapon fire method
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void fire(GameTime gameTime) {



		}

		protected override void additionalUpdate(GameTime gameTime, QuadTree quadTree) {
			
		}

		protected override void additionalDraw(SpriteBatch sBatch) {
			
		}

		protected override void additionalFire(GameTime gameTime) {
			
		}

		protected override void altFire(GameTime gameTime) {
			
		}
	}
}
