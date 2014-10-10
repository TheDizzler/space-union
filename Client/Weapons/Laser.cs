﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceUnion.Weapons {
	class Laser : Projectile {

		public Laser(Texture2D texture, Vector2 position, Ship ship)
			: base(texture, position, ship) {

			projectileTTL = 5;
			projectileMoveSpeed = 1.2f;

			velocity = new Vector2((float) Math.Sin(rotation) * projectileMoveSpeed,
				(float) -Math.Cos(rotation) * projectileMoveSpeed);

			velocity += ship.velocity/1000;

			projectileDamage = 5;

		}

		public override void collide(Tangible target, GameTime gameTime) {

			doDamage(target, gameTime);
			destroy();
		}


		public override void doDamage(Tangible target, GameTime gameTime) {
			
			base.doDamage(target, gameTime);
		}


		public override void destroy() {
			//isActive = false; //Still haveing a self player collision issue
		}

	}
}
