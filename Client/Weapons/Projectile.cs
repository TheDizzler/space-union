using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Ships;
using SpaceUnion.StellarObjects;
using SpaceUnion.Tools;


namespace SpaceUnion.Weapons {

	public abstract class Projectile : Tangible, WeaponSystem {

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

		public Ship owner { get; set; }
		public int weaponDamage { get; set; }

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

			velocity = ship.velocity;
			timeActive = 0;
		}



		public void update(GameTime gameTime, QuadTree quadTree) {

			timeActive += (float) gameTime.ElapsedGameTime.TotalSeconds;
			if (projectileTTL > timeActive) {

				position += velocity * (float) gameTime.ElapsedGameTime.TotalMilliseconds;
				base.update(position);

				checkForCollision(quadTree, gameTime);
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
            if (owner != target)
            {
                doDamage(target, gameTime);
                destroy();
            }
		}

        public override void destroy()
        {
            isActive = false;
            explosionEngine.createBigExplosion(position);
        }


		public virtual void doDamage(Tangible target, GameTime gameTime) {

			target.takeDamage(weaponDamage, gameTime, owner);

		}


	}
}
