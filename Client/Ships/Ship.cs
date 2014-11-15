﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceUnionXNA.StellarObjects;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Weapons.Projectiles;
using SpaceUnionXNA.Weapons.Systems;


namespace SpaceUnionXNA.Ships {

	/// <summary>
	/// Base abstract ship class.
	/// </summary>
	public abstract class Ship : Tangible {
        //test
        SpriteFont font;
        Vector2 username_label;
        String username;

		/// <summary>
		/// Reference to Game1
		/// </summary>
		protected Game1 game;

		/// <summary>
		/// A restistance to movement so all objects will enventual slow to a stop
		/// (not realistic in space but may play better)
		/// </summary>
		public static float dampening = .999f;

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
		/// Keep track of old acceleration
		/// </summary>
		protected Vector2 oldAccel = Vector2.Zero;
		protected bool inertiaOn = false;
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

		/// <summary>
		/// The main weapon of the ship. Created by calling:
		/// mainWeapon = Launcher &lt; T &gt; .CreateLauncher(this, (x, y) => new T(x, y), numBullets);
		/// where T is a Projectile type.
		/// Ya, it's ugly, I know...sorry....
		/// </summary>
		public WeaponSystem mainWeapon;
		/// <summary>
		/// Location on sprite where weapon appears from
		/// </summary>
		protected Vector2 weaponOrigin = Vector2.Zero;

		private KeyboardState lastState;
		protected bool firing;
		protected bool altFiring;

		public int kills = 0;
		public int deaths = 0;
		
		public bool blueTeam = false;
		public bool redTeam = false;

		public TimeSpan inactiveStart;
		public TimeSpan inactiveTime = TimeSpan.Zero;

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

			miniMapIcon = new MapIcon(assets.shipMapIcon, position);

            //Test
            font = Game1.Assets.font;
		}

        public void setUserName(String name)
        {
            username = name;
        }

        public string getUserName()
        {
            return username;
        }

		/* TEST */
		public LaserBeam getBeam() {

			if (this is Scout)
				return ((LaserBeam) mainWeapon);
			return null;
		}

		/// <summary>
		/// @Written by Troy and Kyle with additions by Tristan.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="quadTree"></param>
		public virtual void update(GameTime gameTime, QuadTree quadTree) {

            //Moving username
            username_label.X = (position.X - 50);
            username_label.Y = (position.Y - 50);

			if (firing)
				fire(gameTime);

			if (altFiring)
				altFire(gameTime);

			//if (willCollide)
			//	collide(collideTarget, gameTime);

			moveThisUpdate = velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
			//checkForCollision2(quadTree, gameTime);

			position += moveThisUpdate;
			base.update(position);

			checkForCollision(quadTree, gameTime);

			checkWorldEdge();

			mainWeapon.update(gameTime, quadTree);
			additionalUpdate(gameTime, quadTree);

			velocity *= dampening; // apply a little resistance. Any thrust should over power this.
			if (isActive == false)
			{
				inactiveTime = gameTime.TotalGameTime - inactiveStart;
				if (inactiveTime.Seconds >= 2)
				{
					isActive = true;
				}
			}
		}

		/// <summary>
		/// If a ship has more than one main weapon, place the update code for it here.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="quadTree"></param>
		protected abstract void additionalUpdate(GameTime gameTime, QuadTree quadTree);

		/// <summary>
		/// @Written by Troy and Kyle.
		/// </summary>
		/// <param name="sBatch"></param>
		public override void draw(SpriteBatch sBatch) {

			base.draw(sBatch);
            sBatch.DrawString(font, username,
                username_label, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
			mainWeapon.draw(sBatch);
			additionalDraw(sBatch);
		}

		/// <summary>
		/// If a ship has more than one main weapon, place the draw code for it here.
		/// </summary>
		/// <param name="sBatch"></param>
		protected abstract void additionalDraw(SpriteBatch sBatch);

		/// <summary>
		/// Draws the ship to the radar.
		/// </summary>
		/// <param name="batch"></param>
		public override void drawMiniMap(SpriteBatch batch) {

			miniMapIcon.draw(position, batch);
			//Vector2 org = new Vector2(miniMapIcon.Width/2, miniMapIcon.Height/2);
			//batch.Draw(miniMapIcon, position, null, Color.White, 0, org, 6f, SpriteEffects.None, 0);
		}

		/// <summary>
		/// Determine what kind of collision is occuring.
		/// @Written by Tristan.
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


		public virtual void control(KeyboardState keyState, GameTime gameTime) {


			//Up Key toggles back thruster
			if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W)) {
				thrust(gameTime);
			}

			//Left Key rotates ship left
			if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A)) {
				rotateLeft(gameTime);
			}

			//Right Key rotates ship right
			if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D)) {
				rotateRight(gameTime);
			}

			//Space key activates debugging brake
            /*
			if (keyState.IsKeyDown(Keys.Space)) {
				stop();
			}
            */

			if (keyState.IsKeyDown(Keys.LeftControl)) {
				//fire(gameTime);
				firing = true;
			} else {
				firing = false;
			}

			if (keyState.IsKeyDown(Keys.LeftShift)) {
				//altFire(gameTime);
				altFiring = true;
			} else {
				altFiring = false;
			}

			lastState = keyState;

		}

        public virtual void controlAI()
        {
            //firing = true;
        }

		/// <summary>
		/// Power to main thruster
		/// @Written by Troy. Edited by Tristan
		/// </summary>
		/// <param name="gameTime"></param>
		protected virtual void thrust(GameTime gameTime) {

			Vector2 tempVelocity = velocity;
			// Vectorize the unit acceleration
			Vector2 acceleration = new Vector2((float) Math.Sin(rotation), (float) -Math.Cos(rotation));
			acceleration *= accelSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
			Vector2.Add(ref tempVelocity, ref acceleration, out velocity);

			/*
			if (Math.Abs(tempVelocity.Length()) > maxSpeed) {


				
					Vector2 tempVelocity2 = tempVelocity;
					Vector2.Add(ref tempVelocity2, ref acceleration, out tempVelocity2);
					if (Math.Abs(tempVelocity2.Length()) < tempVelocity.Length()) {
						Vector2.Add(ref velocity, ref acceleration, out velocity);
					}
				} else if (Math.Abs(tempVelocity.Length()) < maxSpeed) {
					Vector2.Add(ref velocity, ref acceleration, out velocity);
				}
			}*/
		}


		/// <summary>
		/// Main weapon fire method.
		/// @Written by Kyle. Edited by Tristan.
		/// </summary>
		/// <param name="gameTime"></param>
		protected virtual void fire(GameTime gameTime) {

			// Fire only every interval we set as the fireTime
			if (gameTime.TotalGameTime - previousMainFireTime > mainFireDelay) {
				// Reset our current time
				previousMainFireTime = gameTime.TotalGameTime;

				// Call mainWeapon to fire, but add it to the weaponOrigin
				mainWeapon.fire(Vector2.Add(position, weaponOrigin));
				additionalFire(gameTime);
			}
		}

		/// <summary>
		/// If a ship has more than one main weapon, place the fire code here.
		/// </summary>
		/// <param name="gameTime"></param>
		protected abstract void additionalFire(GameTime gameTime);

		/// <summary>
		/// Alternate Weapon
		/// </summary>
		/// <param name="gameTime"></param>
		protected abstract void altFire(GameTime gameTime);

		public override void destroy() {
			isActive = false;
			explode();
			//base.destroy();
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
		/// @Written by Troy. Edited by Tristan
		/// </summary>
		/// <param name="gameTime"></param>
		protected virtual void rotateLeft(GameTime gameTime) {

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
		/// @Written by Troy. Edited by Tristan
		/// </summary>
		/// <param name="gameTime"></param>
		protected virtual void rotateRight(GameTime gameTime) {

			float oldRotation = rotation;

			if (rotation > 6.283185 || rotation < -6.283185) {
				rotation = rotation % 6.283185f;
			}
			rotation += turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

			rotateWeaponOrigin(rotation - oldRotation);
		}

		/// <summary>
		/// Rotate where the weapon projectile originates from.
		/// @Written by Tristan
		/// </summary>
		/// <param name="rotateAmount"></param>
		protected virtual void rotateWeaponOrigin(float rotateAmount) {
			Matrix transform = getWeaponOriginTransform(rotateAmount);
			Vector2.TransformNormal(ref weaponOrigin, ref transform, out weaponOrigin);
		}

		/// <summary>
		/// Calculates the rotation needed for the weaponOrigin to stay grapically consistent.
		/// @Written by Tristan
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
