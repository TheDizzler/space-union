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

		/// <summary>
		/// Determines how fast the projectile moves
		/// </summary>
		protected float projectileMoveSpeed;
		/// <summary>
		/// Lenght of time (in seconds) the projectile will stay active.
		/// </summary>
		protected float projectileTTL;
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

		/// <summary>
		/// Deal damage and destroy the projectile.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="gameTime"></param>
		public override void collide(Tangible target, GameTime gameTime) {
			doDamage(target, gameTime);
			destroy();
		}

		public virtual void doDamage(Tangible target, GameTime gameTime) {

			target.takeDamage(projectileDamage, gameTime);
		}

	}
}
