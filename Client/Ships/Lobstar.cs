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
	class Lobstar : Ship {

		public Lobstar(Game1 game1)
			: base(assets.lobstar, game1) {

			maxSpeed = 300;
			accelSpeed = 100.0f;
			turnSpeed = 2.5f;

			currentHealth = maxHealth = 5;

			mass = 500;

			mainFireDelay = TimeSpan.FromSeconds(.5f);
			altFireDelay = TimeSpan.FromSeconds(1f);

			mainWeapon = new LaserBeam(Vector2.Add(position, weaponOrigin), this);
			weaponOrigin = new Vector2(position.X, position.Y - height / 2); // start position of weapon

			engineOrigins.Add(new Vector2(position.X, position.Y + height / 2));
		}


		/// <summary>
		/// Main weapon fire method
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void fire(GameTime gameTime) {


			((LaserBeam) mainWeapon).updatePosition(Vector2.Add(position, weaponOrigin), rotation);

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
