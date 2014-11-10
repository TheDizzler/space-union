using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.StellarObjects;
using SpaceUnion.Tools;
using SpaceUnion.Weapons;


namespace SpaceUnion.Ships {

	/// <summary>
	/// Base abstract ship class.
	/// </summary>
	public abstract class Ship : Tangible {

		/// <summary>
		/// Reference to Game1
		/// </summary>
		protected Game1 game;

		/// <summary>
		/// A restistance to movement so all objects will enventual slow to a stop
		/// </summary>
		public static float dampening = .1f;

		protected float maxSpeed = 100;
		/// <summary>
		/// How many units(pixels) per second a ship will travel more per second of thrust
		/// </summary>
		protected float accelSpeed = 20.0f;
		/// <summary>
		/// The non-directional speed of ship in pixels/second
		/// </summary>
		protected float currentSpeed = 0;
		/// <summary>
		/// Turn speed in radians per second
		/// </summary>
		protected float turnSpeed = 4.5f;


		/// <summary>
		/// Amount of time in seconds between main weaponfire
		/// </summary>
		protected TimeSpan mainFireDelay;
		/// <summary>
		/// Amount of time in seconds between alt weaponfire
		/// </summary>
		protected TimeSpan altFireDelay;
		/// <summary>
		/// When main weapon was fired in GameTime.
		/// </summary>
		protected TimeSpan previousMainFireTime;
		/// <summary>
		/// When alt weapon was fired in GameTime
		/// </summary>
		protected TimeSpan previousAltFireTime;

		public List<Projectile> projectiles;


		//private WeaponSystem mainWeapon;
		/// <summary>
		/// Location on sprite where weapon appears from
		/// </summary>
		protected Vector2 weaponOrigin = Vector2.Zero;

		/// <summary>
		/// Ship constructor
		/// </summary>
		/// <param name="tex">Ship texture</param>
		/// <param name="wpnTex">Weapon texture</param>
		/// <param name="game1"></param>
		protected Ship(Texture2D tex, Texture2D wpnTex, Game1 game1)
			: base(tex, Vector2.Zero) {

			this.game = game1;
			velocity = Vector2.Zero;
			//scale = .3f;
			mass = 10000;
			projectiles = new List<Projectile>();

		}

		//public abstract void setup();


		public virtual void update(GameTime gameTime, QuadTree quadTree) {
			
			position += velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
			base.update(position);


			checkWorldEdge();



			// Update the Projectiles
			for (int i = projectiles.Count - 1; i >= 0; i--) {
				projectiles[i].update(gameTime, quadTree);

				if (!projectiles[i].isActive) {
					projectiles.RemoveAt(i);
				}
			}

			checkForCollision(quadTree, gameTime);
		}

		
		public override void draw(SpriteBatch sBatch) {

			base.draw(sBatch);

			foreach (Projectile projectile in projectiles)
				projectile.draw(sBatch);
		}

		/// <summary>
		/// Determine what kind of collision is occuring.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="gameTime"></param>
		/// <exception cref="NotImplementedException">A new kind of object that needs handling</exception>
		public override void collide(Tangible target, GameTime gameTime) {

			if (target is Projectile)
				target.collide(this, gameTime); // the projectile can handle it from here
			else if (target is Ship)
				CollisionHandler.shipOnShip(this, (Ship) target, gameTime);
			else if (target is Asteroid)
				CollisionHandler.shipOnAsteroid(this, (Asteroid) target, gameTime);
			else if (target is Planet)
				CollisionHandler.shipOnPlanet(this, (Planet) target, gameTime);
			else
				throw new NotImplementedException();
		}

		/// <summary>
		/// Power to main thruster
		/// No max speed cap
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void thrust(GameTime gameTime) {
            ///Max speed logic by
            ///Matthew Baldock
            ///Steven Chen
            Vector2 tempVelocity = velocity;
			Vector2 acceleration = new Vector2((float) Math.Sin(rotation),(float) -Math.Cos(rotation));
			acceleration *= accelSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            float differencex;
            float differencey;
            Vector2 max = new Vector2((float)Math.Sqrt(maxSpeed*maxSpeed/2), (float)Math.Sqrt(maxSpeed*maxSpeed/2));
            
            Vector2.Add(ref tempVelocity, ref acceleration, out tempVelocity);
            float newangle = (float)Math.Atan(tempVelocity.Y/tempVelocity.X);
            if (tempVelocity.Length() > maxSpeed && newangle != rotation)
            {
                differencex = (float)Math.Sin(rotation) * maxSpeed;
                differencey = (float)-Math.Cos(rotation) * maxSpeed;
                acceleration.X = differencex - velocity.X;
                acceleration.Y = differencey - velocity.Y;
                Vector2.Add(ref velocity, ref acceleration, out velocity);
                
            }

            if (tempVelocity.Length() < maxSpeed)
            {
                Vector2.Add(ref velocity, ref acceleration, out velocity);
            }
            

            
            
            
           
		}


		/// <summary>
		/// Main weapon fire method
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void fire(GameTime gameTime) {

			// Fire only every interval we set as the fireTime
			if (gameTime.TotalGameTime - previousMainFireTime > mainFireDelay) {
				// Reset our current time
				previousMainFireTime = gameTime.TotalGameTime;

				// Add the projectile, but add it to the front and center of the player
				projectiles.Add(getProjectile());
			}
		}

		protected abstract Projectile getProjectile();

		/// <summary>
		/// Alternate Weapon
		/// </summary>
		/// <param name="gameTime"></param>
		public abstract void altFire(GameTime gameTime);

		public override void destroy() {
			explode();
		}

		/// <summary>
		/// Call when destroyed
		/// </summary>
		protected void explode() {

			explosionEngine.explodeShip(this);
		}


		/// <summary>
		/// Rotates the ship left
		/// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void rotateLeft(GameTime gameTime) {

			float oldRotation = rotation;
			if (rotation > 6.283185 || rotation < -6.283185) {
				rotation = rotation % 6.283185f;
			}
			// rotates ship by an amount weighted by the amount time that has passed since last update
			rotation -= turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

			rotateWeaponOrigin(rotation - oldRotation);

		}


		/// <summary>
		/// Rotates the ship right
		/// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void rotateRight(GameTime gameTime) {

			float oldRotation = rotation;
			if (rotation > 6.283185 || rotation < -6.283185) {
				rotation = rotation % 6.283185f;
			}
			rotation += turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

			rotateWeaponOrigin(rotation - oldRotation);
		}

		/// <summary>
		/// Rotate where the weapon projectile originates from.
		/// </summary>
		/// <param name="rotateAmount"></param>
		protected virtual void rotateWeaponOrigin(float rotateAmount) {
			Matrix transform = getWeaponOriginTransform(rotateAmount);
			Vector2.TransformNormal(ref weaponOrigin, ref transform, out weaponOrigin);
		}

		/// <summary>
		/// Calculates the rotation needed for the weaponOrigin to stay grapically consistent.
		/// </summary>
		/// <param name="rotateAmount"></param>
		/// <returns></returns>
		protected Matrix getWeaponOriginTransform(float rotateAmount) {

			return Matrix.CreateTranslation(-origin.X, -origin.Y, 0)
				* Matrix.CreateRotationZ(rotateAmount)
				* Matrix.CreateTranslation(origin.X, origin.Y, 0);
		}

		//Debugging Ship Brake
		public void stop() {
			velocity = Vector2.Zero;
			currentSpeed = 0;

			explode();
		}

	}
}
