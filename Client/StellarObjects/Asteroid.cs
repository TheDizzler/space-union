using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Ships;
using SpaceUnion.Tools;
using SpaceUnion.Weapons;
using SpaceUnion.Weapons.Projectiles;


namespace SpaceUnion.StellarObjects {

	/// <summary>
	/// Written by Kyle. Edits by Tristan.
	/// </summary>
	public class Asteroid : Tangible {

		/// <summary>
		/// Damage given from collision
		/// </summary>
		private int damage = 5;
		public int collisionDamage {
			get { return damage; }
		}

		public Asteroid(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			Random r = new Random();
			double direction = r.NextDouble() * 2 * Math.PI; // angle of velocity
			int speed = r.Next(500); // speed in pixels per second
			// move in a straight line
			velocity = new Vector2((float) (Math.Sin(direction) * speed), (float) (-Math.Cos(direction) * speed));

			mass = 1000;
			currentHealth = maxHealth = 1;
		}

		public void update(GameTime gameTime, QuadTree quadTree) {


			if (willCollide)
				collide(collideTarget, gameTime);

			moveThisUpdate = velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
			checkForCollision2(quadTree, gameTime);

			position += moveThisUpdate;
			base.update(position);

			if (outOfBounds())
				destroy();

			
			//checkWorldEdge();

			//checkForCollision(quadTree, gameTime);
		}



		public override void destroy() {
			explosionEngine.createBigExplosion(position);
			base.destroy();
		}

		public override void drawMiniMap(SpriteBatch batch) {
			throw new NotImplementedException();
		}

		public override void collide(Tangible target, GameTime gameTime) {

			if (target is Projectile)
				target.collide(this, gameTime);
			else if (target is Ship)
				CollisionHandler.shipOnAsteroid((Ship) target, this, gameTime);
			else if (target is Asteroid)
				CollisionHandler.asteroidOnAsteroid(this, (Asteroid) target, gameTime);
			else if (target is Planet)
				CollisionHandler.asteroidOnPlanet(this, (Planet) target, gameTime);
			else
				throw new NotImplementedException();
		}


		

	}
}
