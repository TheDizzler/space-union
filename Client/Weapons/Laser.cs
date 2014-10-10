using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceUnion.Weapons {
	class Laser : Projectile {

		public Laser(Texture2D texture, Vector2 position, Ship ship)
			: base(texture, position, ship) {


		}


		protected override void initialize(Ship ship) {

			projectileTTL = 5;
			projectileMoveSpeed = 200f;

			velocity = new Vector2((float) Math.Sin(rotation) * projectileMoveSpeed,
				(float) -Math.Cos(rotation) * projectileMoveSpeed);

			velocity += ship.velocity;

			projectileDamage = 5;
		}

		public override void collide(Tangible target) {

			doDamage(target);
		}

		public override void destroy() {
			throw new NotImplementedException();
		}

		public override void doDamage(Tangible target) {
			
			target.takeDamage(projectileDamage);
		}

		

	}
}
