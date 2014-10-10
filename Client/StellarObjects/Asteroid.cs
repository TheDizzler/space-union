﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;
using SpaceUnion.Weapons;


namespace SpaceUnion.StellarObjects {

	/// <summary>
	/// 
	/// </summary>
	public class Asteroid : Tangible {

		/// <summary>
		/// Damage given from collision
		/// </summary>
		private int damage = -20;
		public int Damage {
			get { return damage; }
		}

		public Asteroid(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			Random r = new Random();
			double direction = r.NextDouble() * 2 * Math.PI; // angle of velocity
			int speed = r.Next(20); // speed
			velocity = new Vector2((float) (Math.Sin(direction) * speed), (float) (-Math.Cos(direction) * speed));

			currentHealth = maxHealth = 10;
		}

		public void update(GameTime gameTime, List<Tangible> tangibles) {

			// move in a straight line
			position += velocity;

			base.update(position);

			checkWorldEdge();

			checkForCollision(tangibles, gameTime);
		}



		public override void destroy() {
			isActive = false;
			explosionEngine.createExplosion(position);
		}


		public override void collide(Tangible target, GameTime gameTime) {

			if (target is Projectile)
				target.collide(this, gameTime);
			else if (target is Ship)
				collisionHandler.shipOnAsteroid((Ship) target, this);
			else if (target is Asteroid)
				collisionHandler.asteroidOnAsteroid(this, (Asteroid) target);
			else if (target is Planet)
				collisionHandler.asteroidOnPlanet(this, (Planet) target);
			else
				throw new NotImplementedException();
		}



	}
}
