using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Ships;
using SpaceUnion.Tools;


namespace SpaceUnion.Weapons.Projectiles {
	/// <summary>
	/// A type of abstract weapon 
	/// @Written by Kyle. Compiled and edited by Tristan.
	/// </summary>
	public abstract class Projectile : Tangible {

		/// <summary>
		/// Determines how fast the projectile moves in pixels per second
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
			timeActive = 0;
		}


		public void update(GameTime gameTime, QuadTree quadTree) {

			//if (willCollide)
			//	collide(collideTarget, gameTime);

			timeActive += (float) gameTime.ElapsedGameTime.TotalSeconds;
			if (projectileTTL > timeActive) {

				moveThisUpdate = velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
				//checkForCollisionProjectile(quadTree, gameTime, owner);

				position += moveThisUpdate;
				base.update(position);



				checkForCollision(quadTree, gameTime);
			} else {
				destroy();
			}
		}


		/// <summary>
		/// Deal damage and destroy the projectile.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="gameTime"></param>
		public override void collide(Tangible target, GameTime gameTime) {
			if (target != owner) {
				doDamage(target, gameTime);
				destroy();
			}
		}

		/// <summary>
		/// Inflict damage on the target.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="gameTime"></param>
		public virtual void doDamage(Tangible target, GameTime gameTime) {

			target.takeDamage(weaponDamage, gameTime, owner);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="startPoint">Location being fired from</param>
		/// <param name="direction"></param>
		/// <param name="shipVelocity"></param>
		public void launch(Vector2 startPoint, float direction, Vector2 shipVelocity) {

			rotation = direction;
			position = startPoint;
			velocity = new Vector2((float) Math.Sin(rotation) * projectileMoveSpeed,
				(float) -Math.Cos(rotation) * projectileMoveSpeed);
			velocity += shipVelocity;
			timeActive = 0;
			isActive = true;
		}



		public override void drawMiniMap(SpriteBatch batch) {
			throw new NotImplementedException();
		}
	}
}
