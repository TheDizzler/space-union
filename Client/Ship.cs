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

		//public float maxHealth;
		//public float currentHealth;
        private float angle = 0; //Angle in radians of ship orientation
        private float spaceshipX = 500; //Horizontal position of spaceship
        private float spaceshipY = 300; //Vertical position of spaceship
        private Texture2D shipTexture;  //Ship Texture
        private float shipVelocityDirectionX = 0; //Amount of pixels the ship moves horizontally per frame (Calculated by sine of angle)
        private float shipVelocityDirectionY = 0; //Amount of pixels the ship moves vertically per frame (Calculated by cosine of angle)
        private float maxSpeed = 7;

        private int shipWidth = 128; //Dimensions of sprite
        private int shipHeight = 128; //Dimensions of sprite

        private float accelSpeed = 0.5f;
        private float currentSpeed = 0;

        public int getWidth()
        {
            return shipWidth;
        }

        public int getHeight() 
        {
            return shipHeight;
        }


        public float getSpaceshipY(){
            return spaceshipY;
        }

        public float getSpaceshipX(){
            return spaceshipX;
        }

        public float getAngle() {
            return angle;
        }

        public float getShipVelocityDirectionX() {
            return shipVelocityDirectionX;
        }

        public float getShipVelocityDirectionY()
        {
            return shipVelocityDirectionY;
        }

        /*
		/// <summary>
		/// Get % health remaining
		/// </summary>
		public float HealthPercentage {
			get { return currentHealth / maxHealth; }
		}
        */

		public bool alive = true;

		//internal float attackDelay;
		//protected float attackTimer;        

		public Ship(Texture2D tex, Vector2 pos): base(tex, pos) {
            shipTexture = tex;
			velocity = Vector2.Zero;
            
		}

        /// <summary>
        /// Return spaceship hitbox.
        /// Translates the spaceship origin to top left.
        /// </summary>
        /// <returns></returns>
        public Rectangle getHitBox() 
        {
            Rectangle hitbox = new Rectangle((int)spaceshipX-(width/2),
                (int)spaceshipY-(height/2), width, height);
            return hitbox;
        }

        /// <summary>
        /// Collision check between ship and screen boundries.
        /// Ships loop horizontally and vertically.
        /// </summary>
        public void checkScreenWrap(GameWindow Window)
        {
            if (spaceshipX < -5)
            {
                spaceshipX = Window.ClientBounds.Width + 3;
            }
            if (spaceshipX > Window.ClientBounds.Width + 5)
            {
                spaceshipX = -3;
            }
            if (spaceshipY < -5)
            {
                spaceshipY = Window.ClientBounds.Height;
            }
            if (spaceshipY > Window.ClientBounds.Height + 5)
            {
                spaceshipY = 0;
            }
        }

        /// <summary>
        /// Draws the space ship on the game screen
        /// </summary>
        /// <param name="sBatch"></param>
        public override void draw(SpriteBatch sBatch)
        {
            spaceshipX += shipVelocityDirectionX;
            spaceshipY -= shipVelocityDirectionY;

            Vector2 origin = new Vector2(shipWidth / 2, shipHeight / 2);
            Vector2 location = new Vector2(spaceshipX, spaceshipY);
            sBatch.Draw(shipTexture, location, null, Color.White, angle, origin, 1f, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Rotates the ship left
        /// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
        /// </summary>
		public void rotateLeft() {
            if (angle > 6.283185 || angle < -6.283185)
            {
                angle = angle % 6.283185f;
            }
            angle -= 0.07f;
		}

        /// <summary>
        /// Rotates the ship right
        /// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
        /// </summary>
		internal void rotateRight() {
            if (angle > 6.283185 || angle < -6.283185)
            {
                angle = angle % 6.283185f;
            }
            angle += 0.07f;
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
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += accelSpeed;
            }
            //Ships cannot exceed maximum speed
            else
            {
                currentSpeed = maxSpeed;
            }

            //Update Ship Velocity Direction
            shipVelocityDirectionX = (float)Math.Sin(angle) * currentSpeed;
            shipVelocityDirectionY = (float)Math.Cos(angle) * currentSpeed;
		}
	}
}
