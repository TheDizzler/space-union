using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Ships;
using SpaceUnion.Tools;
using SpaceUnion.Weapons.Systems;


namespace SpaceUnion.Weapons {
	/// <summary>
	/// A special type of weapon that fires a single beam of energy in
	/// a continuous stream.
	/// @Written by Tristan
	/// </summary>
	class LaserBeam : Sprite, WeaponSystem {


		int beamLength = 250;

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
		/// <summary>
		/// enemy edge hit location if !(0 <= s <= 1) then miss
		/// </summary>
		private float t;


		public LaserBeam(Vector2 startPoint, Ship ship)
			: base(assets.guiRectangle, startPoint) {

			owner = ship;

			weaponDamage = 1;
		}


		public void fire(Vector2 startPoint) {
			throw new NotImplementedException();
		}

		public new void update(GameTime gameTime, QuadTree quadTree) {

			beamQuanta.Clear();
			//Ray2 ray = new Ray2(new Vector2(0, 0), new Vector2(0, 0));
			beamDirection = new Vector2((float) Math.Sin(rotation), (float) -Math.Cos(rotation));

			beamQuantum = new Rectangle((int) position.X, (int) position.Y, 1, 3);

			t = 1; // beamLength multiplier ( 0 <= t <= 1 )
			//s = 1;
			// find targets within line of fire
			List<Tangible> possibleCollisions = quadTree.retrieve(owner); /* This method and may result in missed ships.
																		   * A different retriever using a raycast
																		   will likely be necessary. -Tristan-*/
			

			foreach (Tangible target in possibleCollisions) {
				if (target.isActive && target != owner) {

					
					// Check all for edges for a hit
					string edgeHit = getLengthClosestHitEdge(target.getHitBox());
					if (edgeHit != null) { // we have a bounding box hit

						// and find how long beam will be
						Vector2[] edgePoints = target.getHitBox().edges[edgeHit];
						//t = CollisionHandler.findT(edgePoints[0], edgePoints[1], position, beamDirection);
						// apply damage, etc
						target.destroy();
						break;
					}

				}
			}

			for (int i = 0; i < beamLength * t; ++i) {

				beamQuanta.Add(assets.Content.Load<Texture2D>("Projectiles/molten bullet (6x8)"));// test texture

			}

		}

		/// <summary> *NOT WORKING PROPERLY*
		/// Find t (in the parametric equation) along the laser beam.
		/// This could be probably be cleaned up with a loop.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		private String getLengthClosestHitEdge(HitBox target) {

			String edge = null;
			
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


		public override void draw(SpriteBatch spriteBatch) {
			foreach (Texture2D dot in beamQuanta) {

				position.X += beamDirection.X;
				position.Y += beamDirection.Y;
				beamQuantum.X = (int) position.X;
				beamQuantum.Y = (int) position.Y;
				spriteBatch.Draw(dot, beamQuantum, Color.White);
			}

		}

		public void updatePosition(Vector2 startPoint, float rot) {
			position = startPoint;
			rotation = rot;
		}


		public void doDamage(Tangible target, GameTime gameTime) {
			throw new NotImplementedException();
		}

	}
}
