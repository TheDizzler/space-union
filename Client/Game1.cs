#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace SpaceUnion
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D background;
        private Texture2D shuttle;
        private Texture2D earth;
        private SpriteFont font;
        private float spaceshipX = 500;
        private float spaceshipY = 300;
        private float angle, momentumAngle = 0;
        private KeyboardState state;
        private float velocity = 0;
        private float currentSpeed = 0;
        private float accelSpeed = 0.5f;
        private float maxSpeed = 7;
        private Boolean winflag = false;
        float shipVelocityDirectionX, shipVelocityDirectionY; //Calculated variables to determine X and Y movement
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Backgrounds/background");
            shuttle = Content.Load<Texture2D>("Spaceships/shuttle");
            font = Content.Load<SpriteFont>("SpriteFonts/SpriteFont1"); // Use the name of your sprite font file here instead of 'Score'.
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        //Collision check with screen boundries
        protected void checkScreenWrap()
        {
            if (spaceshipX < -5)
                spaceshipX = Window.ClientBounds.Width + 3;
            if (spaceshipX > Window.ClientBounds.Width + 5)
                spaceshipX = -3;
            if (spaceshipY < -5)
                spaceshipY = Window.ClientBounds.Height;
            if (spaceshipY > Window.ClientBounds.Height + 5)
                spaceshipY = 0;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Check if ship will wrap around edges
            checkScreenWrap();

            state = Keyboard.GetState();

            spaceshipX += shipVelocityDirectionX;
            spaceshipY -= shipVelocityDirectionY;

            //Up Key toggles back thruster
            if (state.IsKeyDown(Keys.Up))
            {
                //Checking if speed doesnt exceed the ship's maximum speed
                if (currentSpeed < maxSpeed)
                {
                    currentSpeed += accelSpeed;
                }
                //Ships cannot exceed maximum speed
                else {
                    currentSpeed = maxSpeed;
                }

                //Update Ship Velocity Direction
                shipVelocityDirectionX = (float)Math.Sin(angle) * currentSpeed;
                shipVelocityDirectionY = (float)Math.Cos(angle) * currentSpeed;
            }

            

            //Left Key rotates ship left
            if (state.IsKeyDown(Keys.Left))
            {
                if (angle > 6.283185 || angle < -6.283185)
                {
                    angle = angle % 6.283185f;
                }
                angle -= 0.15f;
            }
            //Right Key rotates ship right
            else if (state.IsKeyDown(Keys.Right))
            {
                if (angle > 6.283185 || angle < -6.283185)
                {
                    angle = angle % 6.283185f;
                }
                angle += 0.15f;
            }

            //Space key activates debugging brake
            if (state.IsKeyDown(Keys.Space))
            {
                shipVelocityDirectionX = 0;
                shipVelocityDirectionY = 0;
                currentSpeed = 0;
            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.DrawString(font, "Radian Angle =" + angle, new Vector2(100, 20), Color.Red);
            spriteBatch.DrawString(font, "Degree Angle =" + (angle * (180 / Math.PI)), new Vector2(100, 50), Color.Red);
            spriteBatch.DrawString(font, "X =" + shipVelocityDirectionX + " y = " + shipVelocityDirectionY, new Vector2(100, 80), Color.Red);
            spriteBatch.DrawString(font, "X =" + spaceshipX + " y = " + spaceshipY, new Vector2(100, 110), Color.Red);

            Vector2 location = new Vector2(spaceshipX, spaceshipY);
            Rectangle sourceRectangle = new Rectangle(0, 0, shuttle.Width, shuttle.Height);
            Vector2 origin = new Vector2(shuttle.Width / 2, shuttle.Height / 2);
            spriteBatch.Draw(shuttle, location, sourceRectangle, Color.White, angle, origin, 0.1f, SpriteEffects.None, 1);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
