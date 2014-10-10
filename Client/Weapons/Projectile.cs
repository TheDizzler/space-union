using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.StellarObjects;
using SpaceUnion.Tools;


namespace SpaceUnion.Weapons {

	public abstract class Projectile : Tangible {

		/// <summary>
		/// The firerer of the projectile
		/// </summary>
		protected Ship owner;

		// Determines how fast the projectile moves
		protected float projectileMoveSpeed;

		protected Vector2 velocity;
		/// <summary>
		/// Lenght of time (in seconds) the projectile will stay active.
		/// </summary>
		protected int projectileTTL;
		/// <summary>
		/// length of time in seconds projectile has been active.
		/// </summary>
		protected float timeActive;

		/// <summary>
		/// The amount of damage the projectile can inflict
		/// </summary>
		protected int projectileDamage;


		/// <summary>
		/// 
		/// </summary>
		/// <param name="texture">Projectile texture</param>
		/// <param name="position">Location being fired from</param>
		/// <param name="ship">Origin of the projectile</param>
		protected Projectile(Texture2D texture, Vector2 position, Ship ship)
			: base(texture, position) {

			owner = ship;
			rotation = (float) ship.getRotation();

			timeActive = 0;
		}

		/// <summary>
		/// Set up unique attributes of projectile here
		/// (example: projectileTTL, projectileMoveSpeed, projectileDamage, velocity)
		/// </summary>
		//protected abstract void initialize(Ship ship);


		public void update(GameTime gameTime, List<Tangible> targets) {

			timeActive += (float) gameTime.ElapsedGameTime.TotalSeconds;
			if (projectileTTL > timeActive) {

				position += velocity * (float) gameTime.ElapsedGameTime.TotalMilliseconds;
				base.update(position);

				checkForCollision(targets, gameTime);
			} else {
				isActive = false;
			}
		}


		//private void UpdateDamageCollisions() {
		//	// Use the Rectangle's built-in intersect function to 
		//	// determine if two objects are overlapping
		//	foreach (Projectile p in projectiles) {
		//		if (p.getProjectileHitBox().getArray().Intersects(playerShip.getShipHitBox().getArray())) {
		//			playerShip.setHealth(-1);
		//		}
		//		foreach (Asteroid a in asteroids) {
		//			if (p.getProjectileHitBox().getArray().Intersects(a.hitbox.getArray())) {
		//				a.Active = false;
		//			}
		//		}
		//	}
		//}

		public virtual void doDamage(Tangible target, GameTime gameTime) {

			target.takeDamage(projectileDamage, gameTime);
		}

	}
}
