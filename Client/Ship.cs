using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using SpaceUnion.Tools;

namespace SpaceUnion {

	/// <summary>
	/// Base abstract ship class.
	/// CURRENTLY NOT ABSTRACT FOR TESTING
	/// </summary>
	class Ship : Sprite {

		protected Vector2 velocity;

		private float shipVelocityDirectionX = 0; //Amount of pixels the ship moves horizontally per frame (Calculated by sine of angle)
		private float shipVelocityDirectionY = 0; //Amount of pixels the ship moves vertically per frame (Calculated by cosine of angle)
		private float maxSpeed = 7;

		private float accelSpeed = 0.5f;
		private float currentSpeed = 0;


		public float getShipVelocityDirectionX() {
			return shipVelocityDirectionX;
		}

		public float getShipVelocityDirectionY() {
			return shipVelocityDirectionY;
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
		}

		/// <summary>
		/// Collision check between ship and screen boundries.
		/// Ships loop horizontally and vertically.
		/// </summary>
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


			//Vector2 location = new Vector2(position.X, position.Y);
			//Rectangle sourceRectangle = new Rectangle(0, 0, shipTexture.Width, shipTexture.Height);
			//Vector2 origin = new Vector2(shipTexture.Width / 2, shipTexture.Height / 2);
			//sBatch.Draw(shipTexture, location, null, Color.White, angle, origin, 0.1f, SpriteEffects.None, 0);


			base.draw(sBatch);
		}

		/// <summary>
		/// Rotates the ship left
		/// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
		/// </summary>
		public void rotateLeft() {
			if (rotation > 6.283185 || rotation < -6.283185) {
				rotation = rotation % 6.283185f;
			}
			rotation -= 0.15f;
		}

		/// <summary>
		/// Rotates the ship right
		/// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
		/// </summary>
		internal void rotateRight() {
			if (rotation > 6.283185 || rotation < -6.283185) {
				rotation = rotation % 6.283185f;
			}
			rotation += 0.15f;
		}

		//Debugging Ship Brake
		internal void stop() {
			shipVelocityDirectionX = 0;
			shipVelocityDirectionY = 0;
			currentSpeed = 0;
		}

		/// <summary>
		/// Power to main thruster
		/// Does not exceed a max speed cap
		/// </summary>
		internal void thrust() {

			//Checking if speed doesnt exceed the ship's maximum speed
			if (currentSpeed < maxSpeed) {
				currentSpeed += accelSpeed;
			} else { //Ships cannot exceed maximum speed
				currentSpeed = maxSpeed;
			}


			//Update Ship Velocity Direction
			shipVelocityDirectionX = (float) Math.Sin(rotation) * currentSpeed;
			shipVelocityDirectionY = (float) Math.Cos(rotation) * currentSpeed;

			position.X += shipVelocityDirectionX;
			position.Y -= shipVelocityDirectionY;
		}
	}
}
