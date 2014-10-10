using System;
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

		//private List<HitBox> boxList = new List<HitBox>();

		public List<HitBox> hitBoxes { get; set; }
		public int health { get; set; }

		

		public Asteroid(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			//hitBoxes = new List<HitBox> {createHitBox(pos.X, pos.Y, width, height)};

			Random r = new Random();
			double direction = r.NextDouble() * 2 * Math.PI; // angle of velocity
			int speed = r.Next(20); // speed
			velocity = new Vector2((float) (Math.Sin(direction)* speed), (float) (-Math.Cos(direction)*speed));
		}

		public void update(GameTime gameTime, List<Tangible> tangibles) {

			// move in a straight line
			position += velocity;

			base.update(position);
			checkWorldEdge();

			checkForCollision(tangibles);
		}


		
		public override void destroy() {
			isActive = false;
			explosionEngine.createExplosion(position);
		}


		public override void collide(Tangible target) {

			if (target is Projectile)
				target.collide(this);
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
