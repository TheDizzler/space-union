using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using SpaceUnion.Controllers;
using SpaceUnion.Tools;

namespace SpaceUnion {

	/// <summary>
	/// Base abstract ship class.
	/// CURRENTLY NOT ABSTRACT FOR TESTING
	/// </summary>
	public class Ship : Sprite {

		/// <summary>
		/// A restistance to movement so all objects will enventual slow to a stop
		/// </summary>
		public static float dampening = .1f;

		/// <summary>
		/// The current speed and direction of ship
		/// </summary>
		public Vector2 velocity;
		protected float maxSpeed = 7;
		protected float accelSpeed = 4.5f;
		protected float currentSpeed = 0;
		/// <summary>
		/// Turn speed in degrees per second
		/// </summary>
		protected float turnSpeed = 4.5f;


		// The rate of fire of the player laser
		TimeSpan fireTime;
		TimeSpan previousFireTime;

		public List<Projectile> projectiles;

		protected HitBox shipHitBox;

		//Return Ship Hitbox for collision detection
		public HitBox getShipHitBox() {
			return shipHitBox;
		}

		public float getShipVelocityDirectionX() {
			return velocity.X;
		}

		public float getShipVelocityDirectionY() {
			return velocity.Y;
		}

		public int getHealth() {
			return currentHealth;
		}
		/// <summary>
		/// Collision check between ship and screen boundries.
		/// Ships loop horizontally and vertically.
		/// </summary>
		public float getScale() {
			return scale;
		}

		public void setHealth(int health) {
			this.currentHealth = health;
		}

		
		public int maxHealth = 100;
		public int currentHealth;
        
		/// <summary>
		/// Get % health remaining
		/// </summary>
		public float HealthPercentage {
			get { return currentHealth / maxHealth; }
		}
        

		private ExplosionEngine explosionEngine;
		private Texture2D weaponTexture;

		/// <summary>
		/// Ship constructor
		/// </summary>
		/// <param name="tex">Ship texture</param>
		/// <param name="wpnTex">Weapon texture</param>
		/// <param name="pos">Spawn location</param>
		/// <param name="explEngine">Explosion Engine reference</param>
		public Ship(Texture2D tex, Texture2D wpnTex, Vector2 pos, ExplosionEngine explEngine)
			: base(tex, pos) {

			weaponTexture = wpnTex;
			velocity = Vector2.Zero;
			shipHitBox = new HitBox(position.X, position.Y, this.texture.Width, this.texture.Height);
			//scale = .3f;

			projectiles = new List<Projectile>();
			// Set the laser to fire every quarter second
			fireTime = TimeSpan.FromSeconds(.15f);

			currentHealth = maxHealth;
			explosionEngine = explEngine;
		}


		public void update(GameTime gameTime, GameWindow window) {
			// Elapsed time is taken into consideration in thrust and planet.pull
			position.X += velocity.X;
			position.Y -= velocity.Y;

			checkScreenStop(window);

			// Update the Projectiles
			for (int i = projectiles.Count - 1; i >= 0; i--) {
				projectiles[i].update(gameTime);

				if (projectiles[i].getActive() == false) {
					projectiles.RemoveAt(i);
				}
			}
		}


		/* !!Never have update code in draw function!! */
		public override void draw(SpriteBatch sBatch) {

			base.draw(sBatch);

			// Draw the Projectiles
			//for (int i = 0; i < projectiles.Count; i++) {
			//	sBatch.Draw(Assets.shuttle, projectiles[i].getProjectileHitBox().getArray(), Color.Red);
			//	//projectiles[i].draw(spriteBatch); //This is a debug statement to view where the hitboxes are
			//}
			foreach (Projectile projectile in projectiles)
				projectile.draw(sBatch);
		}


		/// <summary>
		/// Rotates the ship left
		/// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
		/// </summary>
		/// <param name="gameTime"></param>
		public void rotateLeft(GameTime gameTime) {
			if (rotation > 6.283185 || rotation < -6.283185) {
				rotation = rotation % 6.283185f;
			}
			// rotates ship by an amount weighted by the amount time that has passed since last update
			rotation -= turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
		}

		/// <summary>
		/// Rotates the ship right
		/// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
		/// </summary>
		/// <param name="gameTime"></param>
		public void rotateRight(GameTime gameTime) {
			if (rotation > 6.283185 || rotation < -6.283185) {
				rotation = rotation % 6.283185f;
			}
			rotation += turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
		}

		//Debugging Ship Brake
		public void stop() {
			velocity = Vector2.Zero;
			currentSpeed = 0;

			explode();
		}

		/// <summary>
		/// Power to main thruster
		/// Does not exceed a max speed cap
		/// </summary>
		/// <param name="gameTime"></param>
		public void thrust(GameTime gameTime) {

			Vector2 acceleration = new Vector2((float) Math.Sin(rotation), (float) Math.Cos(rotation));
			acceleration *= accelSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

			Vector2.Add(ref velocity, ref acceleration, out velocity);

		}

		public void fire(GameTime gameTime) {

			// Fire only every interval we set as the fireTime
			if (gameTime.TotalGameTime - previousFireTime > fireTime) {
				// Reset our current time
				previousFireTime = gameTime.TotalGameTime;

				// Add the projectile, but add it to the front and center of the player
				//AddProjectile(position + new Vector2(0, 0));
				Projectile projectile = new Projectile(weaponTexture, position, this);
				//projectile.Initialize(game.GraphicsDevice.Viewport);
				projectiles.Add(projectile);
			}
		}

		private void AddProjectile(Vector2 position) {

			Projectile projectile = new Projectile(weaponTexture, Position, this);
			//projectile.Initialize(game.GraphicsDevice.Viewport);
			projectiles.Add(projectile);
		}


		/// <summary>
		/// Call when destroyed
		/// </summary>
		protected void explode() {

			explosionEngine.createExplosions(this);
		}

		/// <summary>
		/// Check if ship will wrap around edges
		/// </summary>
		/// <param name="Window"></param>
		private void checkScreenWrap(GameWindow Window) {
			if (position.X < -5) {
				position.X = GameplayScreen.worldWidth + 3;
			}
			if (position.X > GameplayScreen.worldWidth + 5) {
				position.X = -3;
			}
			if (position.Y < -5) {
				position.Y = GameplayScreen.worldHeight;
			}
			if (position.Y > GameplayScreen.worldHeight + 5) {
				position.Y = 0;
			}
		}

		private void checkScreenStop(GameWindow Window) {
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
