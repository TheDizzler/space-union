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

namespace SpaceUnion {
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game {

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
        private Boolean winflag = false;
        float x, y;
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
		protected override void Initialize() {

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
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
		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime) {
            if (spaceshipX < -5)
                spaceshipX = Window.ClientBounds.Width + 3;
            if (spaceshipX > Window.ClientBounds.Width + 5)
                spaceshipX = -3;
            if (spaceshipY < -5)
                spaceshipY = Window.ClientBounds.Height;
            if (spaceshipY > Window.ClientBounds.Height + 5)
                spaceshipY = 0;

            if (angle > 6.283185 || angle < -6.283185)
            {
                angle = angle % 6.283185f;
            }
            state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Up))
            {
                x = (float)Math.Sin(angle);
                y = (float)Math.Cos(angle);
            }
            spaceshipX += (4) * x;
            spaceshipY -= (4) * y;
            if (state.IsKeyDown(Keys.Left))
            {
                angle -= 0.15f;
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                angle += 0.15f;
            }

            if (state.IsKeyDown(Keys.Space))
            {
                x = 0;
                y = 0;
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
		protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (winflag == true)
            {
                spriteBatch.DrawString(font, "You Win", new Vector2(100, 300), Color.Red);
            }
            else
            {



                spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
                spriteBatch.DrawString(font, "Radian Angle =" + angle, new Vector2(100, 20), Color.Red);
                spriteBatch.DrawString(font, "Degree Angle =" + (angle * (180 / Math.PI)), new Vector2(100, 50), Color.Red);
                spriteBatch.DrawString(font, "X =" + x + " y = " + y, new Vector2(100, 80), Color.Red);
                spriteBatch.DrawString(font, "X =" + spaceshipX + " y = " + spaceshipY, new Vector2(100, 110), Color.Red);

                Vector2 location = new Vector2(spaceshipX, spaceshipY);
                Rectangle sourceRectangle = new Rectangle(0, 0, shuttle.Width, shuttle.Height);
                Vector2 origin = new Vector2(shuttle.Width / 2, shuttle.Height / 2);
                spriteBatch.Draw(shuttle, location, sourceRectangle, Color.White, angle, origin, 0.1f, SpriteEffects.None, 1);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
		}
	}
}
