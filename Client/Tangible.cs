using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Controllers;
using SpaceUnion.Tools;
using SpaceUnion.Ships;


namespace SpaceUnion {

	/// <summary>
	/// If in an object can move, be hit or interated with physically it must implement
	/// this interface. Provides a velocity, a hitbox, hitpoints and an alive state.
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

		public void takeDamage(int amount, GameTime gameTime, Ship owner) {

			// check last time taken damage
			if (gameTime.TotalGameTime - previousDamageTime > damageTime) {
				// Reset our current time
				previousDamageTime = gameTime.TotalGameTime;
				currentHealth -= amount;
			}

            if (currentHealth <= 0)
            {
                owner.kills += 1;
                destroy();
            }
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
		/// Check if hitboxes overlap.
		/// </summary>
		/// <param></param>
		/// <param name="quadTree"></param>
		/// <param name="gameTime"></param>
		protected virtual void checkForCollision(QuadTree quadTree, GameTime gameTime) {

			//foreach (Tangible target in targets)
			//	if (target != this && target.isActive)
			//		if (getHitBox().getArray().Intersects(target.getHitBox().getArray()))
			//			collide(target, gameTime);


			List<Tangible> possibleCollisions = quadTree.retrieve(this);

			foreach (Tangible target in possibleCollisions) {
				if (target.isActive && target != this)
					if (getHitBox().getArray().Intersects(target.getHitBox().getArray()))
						collide(target, gameTime);
			}


		}

		/// <summary>
		/// The actions to take when a collision occurs.
		/// </summary>
		/// <param name="target"></param>
		public abstract void collide(Tangible target, GameTime gameTime);

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

	}
}
