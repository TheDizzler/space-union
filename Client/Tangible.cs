using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Controllers;
using SpaceUnion.Tools;


namespace SpaceUnion {

	/// <summary>
	/// If in an object can move, be hit or interated with physically it must implement
	/// this interface. Provides a velocity, a hitbox, hitpoints and an alive state.
	/// @Written by Konstantin and Kyle. Compiled and edited by Tristan.
	/// </summary>
	public abstract class Tangible : Sprite {


		//protected CollisionHandler collisionHandler = Game1.collisionHandler;
		protected ExplosionEngine explosionEngine = Game1.explosionEngine;


		/// <summary>
		/// If false, the object will be destroyed and removed from the game.
		/// </summary>
		public bool isActive { get; set; }

		/// <summary>
		/// How "big" an object is.
		/// Influences gravitational 'power' of large masses.
		/// </summary>
		public float mass = 1;

		private HitBox hitBox;
		//Return Hitbox for collision detection
		public HitBox getHitBox() {
			return hitBox;
		}

		protected int maxHealth = 100;
		protected int currentHealth;
		/// <summary>
		/// Get % health remaining
		/// </summary>
		float HealthPercentage {
			get { return currentHealth / maxHealth; }
		}

		public void takeDamage(int amount, GameTime gameTime) {

			// check last time taken damage
			if (gameTime.TotalGameTime - previousDamageTime > damageTime) {
				// Reset our current time
				previousDamageTime = gameTime.TotalGameTime;
				currentHealth -= amount;
			}

			if (currentHealth <= 0)
				destroy();
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

		/// <summary>
		/// A list of possible collisions this update.
		/// </summary>
		private List<float> possible = new List<float>();


		protected Tangible(Texture2D tex, Vector2 pos)
			: base(tex, pos) {


			hitBox = new HitBox(pos.X, pos.Y, width, height);
			isActive = true;
			currentHealth = maxHealth;

			damageTime = TimeSpan.FromSeconds(3);
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

			//batch.Draw(assets.guiRectangle, hitBox.getArray(), Color.Pink);
			//batch.Draw(assets.guiRectangle, hitBox.position, hitBox.getArray(), Color.Pink, hitBox.rotation, hitBox.position, scale, SpriteEffects.None, 0);
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

			foreach (Tangible target in possibleCollisions) {
				if (target.isActive && target != this)
					if (getHitBox().getArray().Intersects(target.getHitBox().getArray()))
						collide(target, gameTime);
			}

		}

		/// <summary>
		/// An attempt at collision prediction using an array of rays
		/// </summary>
		protected void checkForCollision2(QuadTree quadTree, GameTime gameTime) {

			willCollide = false;
			collideTarget = null;

			if (moveThisUpdate.Length() == 0)
				return;

			List<Tangible> possibleCollisions = quadTree.retrieve(this);

			Ray2 ray0 = new Ray2(hitBox.topLeft, this.moveThisUpdate);
			Ray2 ray1 = new Ray2(hitBox.bottomLeft, this.moveThisUpdate);
			Ray2 ray2 = new Ray2(hitBox.bottomRight, this.moveThisUpdate);
			Ray2 ray3 = new Ray2(hitBox.topRight, this.moveThisUpdate);

			float dist0, dist1, dist2, dist3;
			float velLength = moveThisUpdate.Length();
			float shortestDist = velLength;


			foreach (Tangible target in possibleCollisions) {
				if (target == this)
					continue;

				bool r0, r1, r2, r3;

				//note: it is necessary that each of these functions run
				r0 = ray0.intersectsToRange(target.getHitBox());
				r1 = ray1.intersectsToRange(target.getHitBox());
				r2 = ray2.intersectsToRange(target.getHitBox());
				r3 = ray3.intersectsToRange(target.getHitBox());

				if (!r0 && !r1 && !r2 && !r3) // If no rays intersect
					continue;



				dist0 = ray0.getDistance();
				dist1 = ray1.getDistance();
				dist2 = ray2.getDistance();
				dist3 = ray3.getDistance();

				possible.Clear();

				if (0 <= dist0 && dist0 <= velLength)
					possible.Add(dist0);
				if (0 <= dist1 && dist1 <= velLength)
					possible.Add(dist0);
				if (0 <= dist2 && dist2 <= velLength)
					possible.Add(dist0);
				if (0 <= dist3 && dist3 <= velLength)
					possible.Add(dist0);

				float temp = possible.Min();
				if (temp < shortestDist) {
					shortestDist = temp;
					willCollide = true;
					collideTarget = target;
				}
			}

			if (willCollide) {
				Vector2.Normalize(ref velocity, out moveThisUpdate);
				moveThisUpdate = moveThisUpdate * shortestDist;
			}
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
