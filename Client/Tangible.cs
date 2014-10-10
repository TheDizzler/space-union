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
	/// </summary>
	public abstract class Tangible : Sprite {


		protected CollisionHandler collisionHandler = Game1.collisionHandler;
		protected ExplosionEngine explosionEngine = Game1.explosionEngine;

		/// <summary>
		/// If false, the object will be destroyed and removed from the game.
		/// </summary>
		public bool isActive { get; set; }

		protected HitBox hitBox;
		//Return Hitbox for collision detection
		public HitBox getHitBox() {
			return hitBox;
		}

		protected int maxHealth = 100;
		public int currentHealth;

		public void takeDamage(int amount) {

			currentHealth -= amount;
			if (currentHealth <= 0)
				destroy();
		}

		/// <summary>
		/// Get % health remaining
		/// </summary>
		float HealthPercentage {
			get { return currentHealth / maxHealth; }
		}

		/// <summary>
		/// The current speed and direction of space object
		/// </summary>
		public Vector2 velocity = Vector2.Zero;
		public float getVelocityX() {
			return velocity.X;
		}
		public float getVelocityY() {
			return velocity.Y;
		}

		protected Tangible(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			hitBox = new HitBox(pos.X, pos.Y, width, height);
			isActive = true;
			currentHealth = maxHealth;
		}

		/// <summary>
		/// Update the hitbox
		/// </summary>
		/// <param name="newPosition"></param>
		protected void update(Vector2 newPosition) {

			hitBox.updatePosition(newPosition);

		}


		protected virtual void checkForCollision(List<Tangible> targets) {

			foreach (Tangible target in targets)
				if (target != this && target.isActive)
					if (getHitBox().getArray().Intersects(target.getHitBox().getArray()))
						//collisionHandler.addCollision(this, target);
						collide(target);

		}

		/// <summary>
		/// The actions to take when a collision occurs.
		/// </summary>
		/// <param name="target"></param>
		public abstract void collide(Tangible target);

		/// <summary>
		/// Call when object is destroyed.
		/// </summary>
		public abstract void destroy();

		/// <summary>
		/// Check if object is at world edge and stop (for now)
		/// </summary>
		protected void checkWorldEdge() {
			if (position.X <= 0) {
				position.X = 0;
			}
			if (position.X >= GameplayScreen.worldWidth) {
				position.X = GameplayScreen.worldWidth;
			}
			if (position.Y <= 0) {
				position.Y = 0;
			}
			if (position.Y >= GameplayScreen.worldHeight) {
				position.Y = GameplayScreen.worldHeight;
			}
		}
	}
}
