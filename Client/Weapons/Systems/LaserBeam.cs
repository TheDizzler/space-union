using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Ships;
using SpaceUnion.Tools;


namespace SpaceUnion.Weapons.Systems {
	/// <summary>
	/// A special type of weapon that fires a single beam of energy in
	/// a continuous stream.
	/// @Written by Tristan
	/// </summary>
	public class LaserBeam : Sprite, WeaponSystem {


		public int beamLength = 250;

		public Ship owner { get; set; }
		public int weaponDamage { get; set; }
		/// <summary>
		/// The bounds of the smallest constituent of the beam. Most likely a pixel
		/// or very close to one.
		/// </summary>
		private Rectangle beamQuantum;
		/// <summary>
		/// List of all the beamQuanta that will combine to create the beam.
		/// </summary>
		private List<Texture2D> beamQuanta = new List<Texture2D>();
		private Vector2 beamDirection;
		public float distToTarget;
		private  bool beamOn;


		public LaserBeam(Vector2 startPoint, Ship ship)
			: base(assets.guiRectangle, startPoint) {

			owner = ship;

			weaponDamage = 1;
			beamLength = 250;
		}


		public void fire(Vector2 startPoint) {
			throw new NotImplementedException();
		}

		public void update(GameTime gameTime, QuadTree quadTree) {

			if (!beamOn)
				return;
			beamQuanta.Clear();
			beamQuantum = new Rectangle((int) position.X, (int) position.Y, 1, 3);

			beamDirection = new Vector2((float) Math.Sin(rotation), (float) -Math.Cos(rotation));
			Ray2 ray = new Ray2(position, beamDirection * beamLength);


			distToTarget = 1;

			// find targets within line of fire
			List<Tangible> possibleCollisions = quadTree.retrieve(owner); /* This method and may result in missed ships.
																		   * A different retriever using a raycast
																		   will likely be necessary. -Tristan-*/
			Tangible currentTarget = null;

			foreach (Tangible target in possibleCollisions) {
				if (target != owner) {


					if (ray.intersects(target.getHitBox())) {
						float temp = ray.getDistance2();
						if (temp <= 1 && temp <= distToTarget) {
							distToTarget = temp;
							currentTarget = target;
						}
					}
				}
			}

			if (currentTarget != null) {
				doDamage(currentTarget, gameTime);
			}

			// not quite right. distToTarget should be t in parametric form.
			for (int i = 0; i < beamLength*distToTarget; ++i) {

				beamQuanta.Add(assets.Content.Load<Texture2D>("Projectiles/molten bullet (6x8)"));// test texture

			}
			beamOn = false;
		}


		public override void draw(SpriteBatch spriteBatch) {
			foreach (Texture2D dot in beamQuanta) {

				position.X += beamDirection.X;
				position.Y += beamDirection.Y;
				beamQuantum.X = (int) position.X;
				beamQuantum.Y = (int) position.Y;
				spriteBatch.Draw(dot, beamQuantum, Color.White);
			}
			beamQuanta.Clear();

		}


		/// <summary> *DEPRECATED?*
		/// Find t (in the parametric equation) along the laser beam.
		/// This could be probably be cleaned up with a loop.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		private String getLengthClosestHitEdge(HitBox target) {

			String edge = null;
			float t = 0;
			Vector2[] edgePoints = target.edges["left"];
			float temp = CollisionHandler.findT(edgePoints[0], edgePoints[1], position, beamDirection); // test left edge
			if (temp >= 0 && temp <= t) {
				t = temp;
				edge = "left";
			}

			edgePoints = target.edges["bottom"];
			temp = CollisionHandler.findT(edgePoints[0], edgePoints[1], position, beamDirection); // test bottom edge
			if (temp >= 0 && temp <= t) {
				t = temp;
				edge = "bottom";
			}

			edgePoints = target.edges["right"];
			temp = CollisionHandler.findT(edgePoints[0], edgePoints[1], position, beamDirection); // test right edge
			if (temp >= 0 && temp <= t) {
				t = temp;
				edge = "right";
			}

			edgePoints = target.edges["top"];
			temp = CollisionHandler.findT(edgePoints[0], edgePoints[1], position, beamDirection); // test top edge
			if (temp >= 0 && temp <= t) {
				t = temp;
				edge = "top";
			}
			return edge;
		}




		public void updatePosition(Vector2 startPoint, float rot) {
			position = startPoint;
			rotation = rot;
			beamOn = true;
		}


		public void doDamage(Tangible target, GameTime gameTime) {
			target.takeDamage(weaponDamage, gameTime);
		}

	}
}
