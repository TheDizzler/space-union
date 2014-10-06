using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion.Weapons {

	public abstract class Projectile : Sprite {

		/// <summary>
		/// The firerer of the projectile
		/// </summary>
		protected Ship owner;

		/// <summary>
		/// If false, the projectile will be removed from the game.
		/// </summary>
		protected bool active;
		public bool getActive() {
			return active;
		}

		protected HitBox projectileHitBox;
		public HitBox getProjectileHitBox() {
			return projectileHitBox;
		}

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
			projectileHitBox = new HitBox(position.X, position.Y, texture.Width, texture.Height);

			//velocity = new Vector2((float) Math.Sin(rotation) * projectileMoveSpeed,
			//	(float) -Math.Cos(rotation) * projectileMoveSpeed);


			active = true;
			timeActive = 0;

			initialize(ship); // runs before parent class constructor -- may need to move this
		}

		/// <summary>
		/// Set up unique attributes of projectile here
		/// (example: projectileTTL, projectileMoveSpeed, projectileDamage, velocity)
		/// </summary>
		protected abstract void initialize(Ship ship);


		public void update(GameTime gameTime) {

			timeActive += (float) gameTime.ElapsedGameTime.TotalSeconds;
			if (projectileTTL > timeActive) {

				position += velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;


				projectileHitBox.updatePosition(position.X, position.Y); //updating hitbox

			} else {
				active = false;
			}
		}
	}
}
