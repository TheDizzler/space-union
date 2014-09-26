using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;

namespace SpaceUnion {

	/// <summary>
	/// Base abstract ship class.
	/// CURRENTLY NOT ABSTRACT FOR TESTING
	/// </summary>
	class Ship : Sprite {

		protected Vector2 velocity;

		public float maxHealth;
		public float currentHealth;
		private float angle = 0; //Angle in radians of ship orientation
		private Texture2D shipTexture;  //Ship Texture
		private float shipVelocityDirectionX = 0; //Amount of pixels the ship moves horizontally per frame (Calculated by sine of angle)
		private float shipVelocityDirectionY = 0; //Amount of pixels the ship moves vertically per frame (Calculated by cosine of angle)
		private float maxSpeed = 7;

		private float accelSpeed = 0.5f;
		private float currentSpeed = 0;


		public float getAngle() {
			return angle;
		}

		public float getShipVelocityDirectionX() {
			return shipVelocityDirectionX;
		}

		public float getShipVelocityDirectionY() {
			return shipVelocityDirectionY;
		}

		/// <summary>
		/// Get % health remaining
		/// </summary>
		public float HealthPercentage {
			get { return currentHealth / maxHealth; }
		}

		public bool alive = true;

		internal float attackDelay;
		protected float attackTimer;



		public Ship(Texture2D tex, Vector2 pos)
			: base(tex, pos) {
			shipTexture = tex;
			velocity = Vector2.Zero;
		}

		/// <summary>
		/// Collision check between ship and screen boundries.
		/// Ships loop horizontally and vertically.
		/// </summary>
		public void checkScreenWrap(GameWindow Window) {
			if (position.X < -5) {
				position.X = Window.ClientBounds.Width + 3;
			}
			if (position.X > Window.ClientBounds.Width + 5) {
				position.X = -3;
			}
			if (position.Y < -5) {
				position.Y = Window.ClientBounds.Height;
			}
			if (position.Y > Window.ClientBounds.Height + 5) {
				position.Y = 0;
			}
		}

		public override void draw(SpriteBatch sBatch) {
			position.X += shipVelocityDirectionX;
			position.Y -= shipVelocityDirectionY;

			Vector2 location = new Vector2(position.X, position.Y);
			Rectangle sourceRectangle = new Rectangle(0, 0, shipTexture.Width, shipTexture.Height);
			Vector2 origin = new Vector2(shipTexture.Width / 2, shipTexture.Height / 2);
			sBatch.Draw(shipTexture, location, null, Color.White, angle, origin, 0.1f, SpriteEffects.None, 0);
		}

		/// <summary>
		/// Rotates the ship left
		/// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
		/// </summary>
		public void rotateLeft() {
			if (angle > 6.283185 || angle < -6.283185) {
				angle = angle % 6.283185f;
			}
			angle -= 0.15f;
		}

		/// <summary>
		/// Rotates the ship right
		/// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
		/// </summary>
		internal void rotateRight() {
			if (angle > 6.283185 || angle < -6.283185) {
				angle = angle % 6.283185f;
			}
			angle += 0.15f;
		}

		//Debugging Ship Brake
		internal void stop() {
			shipVelocityDirectionX = 0;
			shipVelocityDirectionY = 0;
			currentSpeed = 0;
		}


		internal void thrust() {
			//Checking if speed doesnt exceed the ship's maximum speed
			if (currentSpeed < maxSpeed) {
				currentSpeed += accelSpeed;
			}
				//Ships cannot exceed maximum speed
			else {
				currentSpeed = maxSpeed;
			}


			//Update Ship Velocity Direction
			shipVelocityDirectionX = (float) Math.Sin(angle) * currentSpeed;
			shipVelocityDirectionY = (float) Math.Cos(angle) * currentSpeed;
		}
	}
}
