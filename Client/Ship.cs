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
	class Ship : Sprite {

		/// <summary>
		/// A restistance to movement so all objects will enventual slow to a stop
		/// </summary>
		public static float dampening = .1f;

		/// <summary>
		/// The current speed and direction of ship
		/// </summary>
		public Vector2 velocity;
		//protected Vector2 impulse;
		//private float shipVelocityDirectionX = 0; //Amount of pixels the ship moves horizontally per frame (Calculated by sine of angle)
		//private float shipVelocityDirectionY = 0; //Amount of pixels the ship moves vertically per frame (Calculated by cosine of angle)

		//private float shipVelocityDirectionX = 0; //Amount of pixels the ship moves horizontally per frame (Calculated by sine of angle)
		//private float shipVelocityDirectionY = 0; //Amount of pixels the ship moves vertically per frame (Calculated by cosine of angle)
        private int health = 100;
        private bool active;
        private float shipScale;
        internal HitBox shipHitBox;
		protected float maxSpeed = 7;

		protected float accelSpeed = 4.5f;
		protected float currentSpeed = 0;
		/// <summary>
		/// Turn speed in degrees per second
		/// </summary>
		protected float turnSpeed = 4.5f;

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

        public int getHealth()
        {
            return health;
        }

        public void setHealth(int health)
        {
            this.health += health;
            if (this.health <= 0)
            {
                this.health = 0;
            }
        }
        public bool Active
        {
            get { return active; }
        }

		/*
		public float maxHealth;
		public float currentHealth;
        
		/// <summary>
		/// Get % health remaining
		/// </summary>
		public float HealthPercentage {
			get { return currentHealth / maxHealth; }
		}
        */


		//internal float attackDelay;
		//protected float attackTimer;


		public Ship(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			velocity = Vector2.Zero;
			scale = .3f;
            shipScale = scale;
            shipHitBox = new HitBox(position.X, position.Y,
             (int)(this.texture.Width * shipScale), (int)(this.texture.Height * shipScale));
            active = true;
		}

		/// <summary>
		/// Collision check between ship and screen boundries.
		/// Ships loop horizontally and vertically.
		/// </summary>
        /// 
        public float getScale()
        {
            return shipScale;
        }


		public void checkScreenWrap(GameWindow Window) {
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

		public void checkScreenStop(GameWindow Window) {
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

		/* !!Never have update code in draw function!! */
		public override void draw(SpriteBatch sBatch) {

			base.draw(sBatch);
		}


		public void update(GameTime gameTime) {
			// Elapsed time is taken into consideration in thrust and planet.pull
			position.X += velocity.X;
			position.Y -= velocity.Y;
            shipHitBox.updatePosition(position.X, position.Y);
            if (health <= 0)
            {
                active = false;
            }
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
		internal void rotateRight(GameTime gameTime) {
			if (rotation > 6.283185 || rotation < -6.283185) {
				rotation = rotation % 6.283185f;
			}
			rotation += turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
		}

		//Debugging Ship Brake
		internal void stop() {
			velocity.X = 0;
			velocity.Y = 0;
			currentSpeed = 0;
		}

		/// <summary>
		/// Power to main thruster
		/// Does not exceed a max speed cap
		/// </summary>
		/// <param name="gameTime"></param>
		internal void thrust(GameTime gameTime) {

			//Checking if speed doesnt exceed the ship's maximum speed
			//if (currentSpeed < maxSpeed) {
			//	currentSpeed += accelSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
			//} else { //Ships cannot exceed maximum speed
			//	currentSpeed = maxSpeed;
			//}


			//Update Ship Velocity Direction
			//velocity.X = (float) Math.Sin(rotation) * currentSpeed;
			//velocity.Y = (float) Math.Cos(rotation) * currentSpeed;

			//position.X += velocity.X;
			//position.Y -= velocity.Y;
			Vector2 acceleration = new Vector2((float) Math.Sin(rotation), (float) Math.Cos(rotation));
			acceleration *= accelSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;


			Vector2.Add(ref velocity, ref acceleration, out velocity);

		}
	}
}
