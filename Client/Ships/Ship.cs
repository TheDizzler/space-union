using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceUnionXNA.StellarObjects;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Weapons.Projectiles;
using SpaceUnionXNA.Weapons.Systems;


namespace SpaceUnionXNA.Ships
{

    /// <summary>
    /// Base abstract ship class.
    /// </summary>
    public abstract class Ship : Tangible
    {

        SpriteFont font;
        public Vector2 username_label;
        public Vector2 health_label;
        public String username { get; set; }
        protected Color teamColor;

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

        /* Used to map ship's actions to keys */
        public enum shipAction
        {
            forward,
            turnLeft,
            turnRight,
            shoot,
            altShoot
        }

        public List<Laser> projectiles;

        /// <summary>
        /// Ship constructor
        /// </summary>
        /// <param name="tex">Ship texture</param>
        /// <param name="wpnTex">Weapon texture</param>
        /// <param name="game1"></param>
        protected Ship(Texture2D tex, Texture2D wpnTex, Game1 game1, Color color)
            : base(tex, Vector2.Zero)
        {
            teamColor = color;
            this.game = game1;
            velocity = Vector2.Zero;

            currentHealth = maxHealth = 20;

            mass = 1000;

            miniMapIcon = new MapIcon(assets.shipMapIcon, position);

            font = Game1.Assets.font;
            projectiles = new List<Laser>();
        }

        ///* TEST */
        //public LaserBeam getBeam() {

        //	if (this is Scout)
        //		return ((LaserBeam) mainWeapon);
        //	return null;
        //}

        /// <summary>
        /// @Written by Troy and Kyle with additions by Tristan.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="quadTree"></param>
        public virtual void update(GameTime gameTime, QuadTree quadTree)
        {

            /** NOTE: This gets called twice when a ship is made inactive on its update!
            *	It would be great to keep these updating even when a ship is inactive
            *	instead of orphaning its projectiles. */
            //mainWeapon.update(gameTime, quadTree);
            //additionalUpdate(gameTime, quadTree);
            if (isActive)
            {
                position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                base.update(position);
                checkForCollision(quadTree, gameTime);
                checkWorldEdge();
                velocity *= dampening; // apply a little resistance. Any thrust should over power this.
            }

            if (isActive == false)
            {
                inactiveTime = gameTime.TotalGameTime - inactiveStart;
                if (inactiveTime.Seconds >= 2)
                {
                    isActive = true;
                    currentHealth = maxHealth;
                }
            }

            // Update the Projectiles
            if (projectiles != null)
            {
                for (int i = projectiles.Count - 1; i >= 0; i--)
                {
                    projectiles[i].update(gameTime, quadTree);

                    if (!projectiles[i].isActive)
                    {
                        projectiles.RemoveAt(i);
                    }
                }
            }
        }

        public virtual void inactiveUpdate(GameTime gameTime)
        {

        }

        /// <summary>
        /// If a ship has more than one main weapon, place the update code for it here.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="quadTree"></param>
        //protected abstract void additionalUpdate(GameTime gameTime, QuadTree quadTree);

        /// <summary>
        /// @Written by Troy and Kyle.
        /// </summary>
        /// <param name="sBatch"></param>
        public override void draw(SpriteBatch sBatch)
        {
            //mainWeapon.draw(sBatch);
            //additionalDraw(sBatch);

            //Update the player's username tag.
            username_label.X = (position.X - 80);
            username_label.Y = (position.Y - 80);

            health_label.X = (position.X - 20);
            health_label.Y = (position.Y - 80);

            //Draw the players name over the ship.
            sBatch.DrawString(font, username,
                username_label, teamColor, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
            //Draw the current healt over the ship.
            sBatch.DrawString(font, base.currentHealth + "%",
                health_label, teamColor, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

            if (projectiles != null)
            {
                foreach (SpaceUnionXNA.Weapons.Projectiles.Projectile projectile in projectiles)
                    projectile.draw(sBatch);
            }

            base.draw(sBatch);
        }

        /// <summary>
        /// If a ship has more than one main weapon, place the draw code for it here.
        /// </summary>
        /// <param name="sBatch"></param>
        //protected abstract void additionalDraw(SpriteBatch sBatch);

        /// <summary>
        /// Draws the ship to the radar.
        /// </summary>
        /// <param name="batch"></param>
        public override void drawMiniMap(SpriteBatch batch)
        {

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
        public override void collide(Tangible target, GameTime gameTime)
        {

            if (target is Projectile)
                target.collide(this, gameTime); // the projectile can handle it from here
            else if (target is Ship)
                CollisionHandler.shipOnShip(this, (Ship)target, gameTime);
            else if (target is Asteroid)
                CollisionHandler.shipOnAsteroid(this, (Asteroid)target, gameTime);
            else if (target is Planet)
                CollisionHandler.shipOnPlanet(this, (Planet)target, gameTime);
            else
                throw new NotImplementedException();
        }


        public virtual void control(KeyboardState keyState, GameTime gameTime)
        {
            /* Uses key bindings */
            if (keyState.IsKeyDown(game.keylist[(int)shipAction.forward]))
            {
                thrust(gameTime);
            }
            if (keyState.IsKeyDown(game.keylist[(int)shipAction.turnLeft]))
            {
                rotateLeft(gameTime);
            }
            if (keyState.IsKeyDown(game.keylist[(int)shipAction.turnRight]))
            {
                rotateRight(gameTime);
            }
            if (keyState.IsKeyDown(game.keylist[(int)shipAction.shoot]))
            {
                Laser p = new Laser(this.Position, this);
                p.launch(this.Position, this.rotation, this.velocity);
                p.timeActive = 0;
                projectiles.Add(p);
            }
            if (keyState.IsKeyDown(game.keylist[(int)shipAction.altShoot]))
            {
                altFiring = true;
            }
            else
            {
                altFiring = false;
            }

            lastState = keyState;

        }

        public virtual void controlAI()
        {
            firing = true;
        }

        /// <summary>
        /// Power to main thruster
        /// @Written by Troy. Edited by Tristan
        /// </summary>
        /// <param name="gameTime"></param>
        protected virtual void thrust(GameTime gameTime)
        {
            ///Max speed logic by
            ///Matthew Baldock
            ///Steven Chen
            Vector2 tempVelocity = velocity;
            Vector2 acceleration = new Vector2((float)Math.Sin(rotation), (float)-Math.Cos(rotation));
            acceleration *= accelSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2.Add(ref tempVelocity, ref acceleration, out tempVelocity);

            if (tempVelocity.Length() > maxSpeed)
            {
                acceleration *= 0;
                Vector2.Add(ref velocity, ref acceleration, out velocity);
            }
            if (tempVelocity.Length() < maxSpeed)
            {
                Vector2.Add(ref velocity, ref acceleration, out velocity);
            }

        }



        /// <summary>
        /// Main weapon fire method.
        /// @Written by Kyle. Edited by Tristan.
        /// </summary>
        /// <param name="gameTime"></param>
        protected virtual void fire(GameTime gameTime)
        {

            // Fire only every interval we set as the fireTime
            if (gameTime.TotalGameTime - previousMainFireTime > mainFireDelay)
            {
                // Reset our current time
                previousMainFireTime = gameTime.TotalGameTime;

                // Call mainWeapon to fire, but add it to the weaponOrigin
                mainWeapon.fire(Vector2.Add(position, weaponOrigin));
                //additionalFire(gameTime);
            }
        }

        /// <summary>
        /// If a ship has more than one main weapon, place the fire code here.
        /// </summary>
        /// <param name="gameTime"></param>
        //protected abstract void additionalFire(GameTime gameTime);

        /// <summary>
        /// Alternate Weapon
        /// </summary>
        /// <param name="gameTime"></param>
        //protected abstract void altFire(GameTime gameTime);

        public override void destroy()
        {
            explode();
            velocity = Vector2.Zero;

            base.destroy();
        }

        /// <summary>
        /// Call when destroyed
        /// </summary>
        protected void explode()
        {

            explosionEngine.explodeShip(this);
        }


        /// <summary>
        /// Rotates the ship left
        /// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
        /// @Written by Troy. Edited by Tristan
        /// </summary>
        /// <param name="gameTime"></param>
        protected virtual void rotateLeft(GameTime gameTime)
        {

            float oldRotation = rotation;

            if (rotation > 6.283185 || rotation < -6.283185)
            {
                rotation = rotation % 6.283185f;
            }
            // rotates ship by an amount weighted by the amount time that has passed since last update
            rotation -= turnSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            rotateWeaponOrigin(rotation - oldRotation);

        }


        /// <summary>
        /// Rotates the ship right
        /// Resets the angle to 0 when completing a full rotation, which prevents integer overflow.
        /// @Written by Troy. Edited by Tristan
        /// </summary>
        /// <param name="gameTime"></param>
        protected virtual void rotateRight(GameTime gameTime)
        {

            float oldRotation = rotation;

            if (rotation > 6.283185 || rotation < -6.283185)
            {
                rotation = rotation % 6.283185f;
            }
            rotation += turnSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            rotateWeaponOrigin(rotation - oldRotation);
        }

        /// <summary>
        /// Rotate where the weapon projectile originates from.
        /// @Written by Tristan
        /// </summary>
        /// <param name="rotateAmount"></param>
        protected virtual void rotateWeaponOrigin(float rotateAmount)
        {
            Matrix transform = getWeaponOriginTransform(rotateAmount);
            Vector2.TransformNormal(ref weaponOrigin, ref transform, out weaponOrigin);
        }

        /// <summary>
        /// Calculates the rotation needed for the weaponOrigin to stay grapically consistent.
        /// @Written by Tristan
        /// </summary>
        /// <param name="rotateAmount"></param>
        /// <returns></returns>
        protected Matrix getWeaponOriginTransform(float rotateAmount)
        {

            return Matrix.CreateTranslation(-origin.X, -origin.Y, 0)
                * Matrix.CreateRotationZ(rotateAmount)
                * Matrix.CreateTranslation(origin.X, origin.Y, 0);
        }

        //Debugging Ship Brake
        public void stop()
        {
            velocity = Vector2.Zero;

            explode();
        }

    }
}
