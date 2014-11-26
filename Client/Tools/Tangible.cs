using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Controllers;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Ships;


namespace SpaceUnionXNA.Tools {

	/// <summary>
	/// If in an object can move, be hit or interated with physically it must implement
	/// this interface. Provides a velocity, a hitbox, hitpoints and an alive state.
	/// @Written by Konstantin and Kyle. Compiled and edited by Tristan.
	/// </summary>
	public abstract class Tangible : Sprite {

		protected ExplosionEngine explosionEngine = Game1.explosionEngine;

		protected MapIcon miniMapIcon;

		/// <summary>
		/// If false, the object will be destroyed and removed from the game.
		/// </summary>
		public bool isActive { get; set; }

		/// <summary>
		/// How "big" an object is.
		/// Influences gravitational 'power' of large masses and collisions
		/// (collisons not yet implemented).
		/// </summary>
		public float mass = 1;

		private HitBox hitBox;
		/// <summary>
		/// Return Hitbox for collision detection
		/// </summary>
		/// <returns></returns>
		public HitBox getHitBox() {
			return hitBox;
		}

		protected int maxHealth = 1;
		protected int currentHealth;
		/// <summary>
		/// Get % health remaining
		/// </summary>
		float HealthPercentage {
			get { return currentHealth / maxHealth; }
		}

		public void takeDamage(int amount, GameTime gameTime, Ship owner) {
			//if (this != owner) {

			// check last time taken damage
			if (gameTime.TotalGameTime - previousDamageTime > damageTime) {
				// Reset our current time
				previousDamageTime = gameTime.TotalGameTime;
				currentHealth -= amount;
			}

			if (owner is Ship && this is Ship) {
				Ship target = (Ship) this;
				if (owner.blueTeam && target.redTeam) {
					if (currentHealth <= 0) {
						owner.kills += 1;
						destroy();
					}
				} else if (owner.blueTeam && target.blueTeam) {
					if (currentHealth <= 0) {
						owner.kills -= 1;
						destroy();
					}
				} else if (owner.redTeam && target.blueTeam) {
					if (currentHealth <= 0) {
						owner.kills += 1;
						destroy();
					}
				} else if (owner.redTeam && target.redTeam) {
					if (currentHealth <= 0) {
						owner.kills -= 1;
						destroy();
					}
				}
			} else {
				if (currentHealth <= 0) {
					owner.kills += 1;
					destroy();
				}
			}

			//}
		}

		/// <summary>
		///  gives the player temporary invincibility on collision
		/// </summary>
		TimeSpan damageTime;
		TimeSpan previousDamageTime;


		/// <summary>
		/// The current speed and direction of space object
		/// </summary>
		public Vector2 velocity = Vector2.Zero;

		/// <summary>
		/// Actually velocity used this update.
		/// This is changed if a collision is detected before movement.
		/// </summary>
		protected Vector2 moveThisUpdate;

		protected bool willCollide;
		public Tangible collideTarget;

		public float getVelocityX() {
			return velocity.X;
		}
		public float getVelocityY() {
			return velocity.Y;
		}


		protected Tangible(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			int padding = 0;
			hitBox = new HitBox(position.X, position.Y, width + padding, height + padding);
			isActive = true;
			currentHealth = maxHealth;

			damageTime = TimeSpan.FromSeconds(1);
		}

		/// <summary>
		/// Update the hitbox
		/// </summary>
		/// <param name="newPosition"></param>
		protected void update(Vector2 newPosition) {

			hitBox.updatePosition(newPosition, rotation);

		}

		/// <summary>
		/// Can draw hitboxes here for debugging.
		/// </summary>
		/// <param name="batch"></param>
		public override void draw(SpriteBatch batch) {
			base.draw(batch);
			//hitBox.draw(batch, assets); // Debug hitboxes

		}

		/// <summary>
		/// What to draw on the minimap for this object.
		/// </summary>
		/// <param name="batch"></param>
		public abstract void drawMiniMap(SpriteBatch batch);

		/// <summary>
		/// Check if hitboxes overlap.
		/// </summary>
		/// <param></param>
		/// <param name="quadTree"></param>
		/// <param name="gameTime"></param>
		protected virtual void checkForCollision(QuadTree quadTree, GameTime gameTime) {


			List<Tangible> possibleCollisions = quadTree.retrieve(this); // Need a better retrieve method for rays

			foreach (Tangible target in possibleCollisions.ToList<Tangible>()) {
				if (target.isActive && target != this)
					if (getHitBox().getArray().Intersects(target.getHitBox().getArray()))
						if (fineCheck(target))
							collide(target, gameTime);
			}

		}

		/// <summary>
		/// A pixel-by-pixel collision detector. Useing texture.GetData(rawDataA) might be a little bit sketchy...
		/// Code from http://gamedev.stackexchange.com/questions/15191/is-there-a-good-way-to-get-pixel-perfect-collision-detection-in-xna
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		private bool fineCheck(Tangible target) {

			Color[] rawDataA = new Color[this.width * this.height];
			this.texture.GetData(rawDataA);
			Color[] rawDataB = new Color[target.width * target.height];
			target.texture.GetData(rawDataB);


			Rectangle thisRect = this.getHitBox().getArray();
			Rectangle targetRect = target.getHitBox().getArray();

			// Find the bounds of the rectangle intersection
			int top = Math.Max(thisRect.Top, targetRect.Top);
			int bottom = Math.Min(thisRect.Bottom, targetRect.Bottom);

			int left = Math.Max(thisRect.Left, targetRect.Left);
			int right = Math.Min(thisRect.Right, targetRect.Right);

			// for each pixel in the intersecting rectangle
			for (int y = top; y < bottom; ++y) {
				for (int x = left; x < right; ++x) {

					Color colorA = rawDataA[(x - thisRect.Left) + (y - thisRect.Top) * thisRect.Width];
					Color colorB = rawDataB[(x - targetRect.Left) + (y - targetRect.Top) * targetRect.Width];


					// if both colors are not transparent
					if (colorA.A != 0 && colorB.A != 0) {
						//GUI.hitDetected = true;
						return true;
					}
				}
			}
			//GUI.hitDetected = false;
			return false;
		}

		/// <summary>
		/// An attempt at collision prediction using an array of rays
		/// </summary>
		protected void checkForCollision2(QuadTree quadTree, GameTime gameTime) {

			willCollide = false;
			collideTarget = null;

			if (moveThisUpdate.Length() == 0)
				return;

			Vector2 move = new Vector2();
			Vector2.Normalize(ref moveThisUpdate, out move);

			List<Tangible> possibleCollisions = quadTree.retrieve(this);

			Ray2 ray0 = new Ray2(hitBox.topLeft, move);
			Ray2 ray1 = new Ray2(hitBox.bottomLeft, move);
			Ray2 ray2 = new Ray2(hitBox.bottomRight, move);
			Ray2 ray3 = new Ray2(hitBox.topRight, move);
			Ray2 ray4 = new Ray2(hitBox.position, move);

			float dist0, dist1, dist2, dist3, dist4;
			float velLength = move.Length();
			float shortestDist = velLength;


			foreach (Tangible target in possibleCollisions) {
				if (target == this)
					continue;

				bool r0, r1, r2, r3, r4;

				//note: it is necessary that each of these functions run
				r0 = ray0.intersectsToRange(target.getHitBox());
				r1 = ray1.intersectsToRange(target.getHitBox());
				r2 = ray2.intersectsToRange(target.getHitBox());
				r3 = ray3.intersectsToRange(target.getHitBox());
				r4 = ray4.intersectsToRange(target.getHitBox());

				if (!r0 && !r1 && !r2 && !r3 && !r4) // If no rays intersect
					continue;



				dist0 = ray0.getDistance();
				dist1 = ray1.getDistance();
				dist2 = ray2.getDistance();
				dist3 = ray3.getDistance();
				dist4 = ray4.getDistance();

				//possible.Clear();

				if (0 <= dist0 && dist0 <= shortestDist)
					setTarget(dist0, ref shortestDist, target);
				//possible.Add(dist0);
				if (0 <= dist1 && dist1 <= shortestDist)
					setTarget(dist1, ref shortestDist, target);
				//possible.Add(dist1);
				if (0 <= dist2 && dist2 <= shortestDist)
					setTarget(dist2, ref shortestDist, target);
				//possible.Add(dist2);
				if (0 <= dist3 && dist3 <= shortestDist)
					setTarget(dist3, ref shortestDist, target);
				//possible.Add(dist3);
				if (0 <= dist4 && dist4 <= shortestDist)
					setTarget(dist4, ref shortestDist, target);
				//possible.Add(dist4);

				//float temp = possible.Min();
				//if (temp < shortestDist) {
				//	shortestDist = temp;
				//	willCollide = true;
				//	collideTarget = target;
				//}
			}

			if (willCollide) {
				Vector2.Normalize(ref velocity, out moveThisUpdate);
				moveThisUpdate = moveThisUpdate * shortestDist;
			}
		}


		private void setTarget(float dist, ref float shortestDist, Tangible target) {
			shortestDist = dist;
			willCollide = true;
			collideTarget = target;
		}

		/// <summary>
		/// The actions to take when a collision occurs.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="gameTime"></param>
		public abstract void collide(Tangible target, GameTime gameTime);

		/// <summary>
		/// Call when object is destroyed.
		/// Sets the object not Active and removes from the global active list.
		/// </summary>
		public virtual void destroy() {
			isActive = false;
			GameplayScreen.targets.Remove(this);
		}

		/// <summary>
		/// Check if object is at world edge and stop (for now)
		/// </summary>
		protected void checkWorldEdge() {
			if (position.X <= 0) {
				position.X = 0;
				velocity.X = 0;
			}
			if (position.X >= GameplayScreen.worldWidth) {
				position.X = GameplayScreen.worldWidth;
				velocity.X = 0;
			}
			if (position.Y <= 0) {
				position.Y = 0;
				velocity.Y = 0;
			}
			if (position.Y >= GameplayScreen.worldHeight) {
				position.Y = GameplayScreen.worldHeight;
				velocity.Y = 0;
			}
		}


		protected bool outOfBounds() {

			if (position.X <= -50) {
				return true;
			}
			if (position.X >= GameplayScreen.worldWidth + 50) {
				return true;
			}
			if (position.Y <= -50) {
				return true;
			}
			if (position.Y >= GameplayScreen.worldHeight + 50) {
				return true;
			}
			return false;
		}


	}
}
