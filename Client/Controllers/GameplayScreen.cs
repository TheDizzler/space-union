using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;

namespace SpaceUnion
{

    class GameplayScreen
    {

        private KeyboardState keyState;
        private MouseState mouseState;
        private SpriteBatch spriteBatch;
        private static Ship playerShip;
        private WinFlag winflag;
        private int WINFLAG_ = 0;
        private Game1 game;
        static private bool flashFlag = false;
        private static System.Timers.Timer invinsibilityTimer;
        private static System.Timers.Timer flashingTimer;
        List<Projectile> projectiles;

        // The rate of fire of the player laser
        TimeSpan fireTime;
        TimeSpan previousFireTime;

        Camera mainCamera;
        GUI gui;

        private Tools.AssetManager Assets;

        static public int worldWidth = 4000;
        static public int worldHeight = 2000;
        static protected bool invinsible;
        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;


        public GameplayScreen(Game1 game, SpriteBatch batch)
        {
            this.game = game;
            SCREEN_HEIGHT = game.getScreenHeight();
            SCREEN_WIDTH = game.getScreenWidth();

            spriteBatch = batch;
            invinsibilityTimer = new System.Timers.Timer(2000);
            invinsibilityTimer.Elapsed += OnTimedEvent;
            invinsibilityTimer.Enabled = false;
            flashingTimer = new System.Timers.Timer(50);
            flashingTimer.Elapsed += onTimedEventFlashing;
            flashingTimer.Enabled = false;
            invinsible = false;
            Assets = Game1.Assets;
            playerShip = new Ship(Assets.spaceShipTest, new Vector2(200, 200)); //Create new player ship
            winflag = new WinFlag(Assets.winflag1, new Vector2(300, 300));
            gui = new GUI(game, playerShip, winflag);
            flashFlag = true;
            Viewport mainViewport = new Viewport((int)playerShip.getX(), (int)playerShip.getY(),
                game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height - GUI.guiHeight);
            mainCamera = new Camera(mainViewport, worldWidth, worldHeight, 1.0f);

            projectiles = new List<Projectile>();

            // Set the laser to fire every quarter second
            fireTime = TimeSpan.FromSeconds(.15f);
        }

        /// <summary>
        /// Draws the stars background and debug information.
        /// </summary>
        protected void drawWorld()
        {
            //spriteBatch.Draw(background, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), Color.White);
            //spriteBatch.DrawString(font, "Radian Angle =" + playerShip.getAngle(), new Vector2(100, 20), Color.Red);
            //spriteBatch.DrawString(font, "Degree Angle =" + (playerShip.getAngle() * (180 / Math.PI)), new Vector2(100, 50), Color.Red);
            //spriteBatch.DrawString(font, "X =" + playerShip.getShipVelocityDirectionX()
            //	+ " y = " + playerShip.getShipVelocityDirectionY(), new Vector2(100, 80), Color.Red);
            //spriteBatch.DrawString(font, "X =" + playerShip.getSpaceshipX()
            //	+ " y = " + playerShip.getSpaceshipY(), new Vector2(100, 110), Color.Red);

            /* Parallax Scrolling BG */
            spriteBatch.Draw(Assets.starfield2,
                new Rectangle((int)(mainCamera.Position.X * .9), (int)(mainCamera.Position.Y * .9), worldWidth / 4, worldHeight / 4),
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(Assets.starfield1,
                new Rectangle((int)(mainCamera.Position.X * 0.7), (int)(mainCamera.Position.Y * 0.7), 1600, 1200),
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(Assets.starfield1,
                new Rectangle((int)(mainCamera.Position.X * 0.6), (int)(mainCamera.Position.Y * 0.6), 2400, 1800),
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(Assets.starfield1,
                new Rectangle((int)(mainCamera.Position.X * 0.4), (int)(mainCamera.Position.Y * 0.4), 800, 600),
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(Assets.starfield3,
                new Rectangle((int)(mainCamera.Position.X * .5), (int)(mainCamera.Position.Y * .5f), worldWidth / 10, worldHeight / 10),
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void AddProjectile(Vector2 position)
        {
            Projectile projectile = new Projectile(Assets.laser, playerShip.Position, playerShip);
            projectile.Initialize(game.GraphicsDevice.Viewport, Assets.laser);
            projectiles.Add(projectile);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// 
        /// Checks player edge wrap around.
        /// 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            
            //playerShip.checkScreenWrap(Window); //Check if ship will wrap around edges
            playerShip.checkScreenStop(game.Window);
            keyState = Keyboard.GetState(); //Get which keys are pressed or released
            mouseState = Mouse.GetState();



            //Up Key toggles back thruster
            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
            {
                playerShip.thrust();
            }

            //Left Key rotates ship left
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
            {
                playerShip.rotateLeft();
            }

            //Right Key rotates ship right
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
            {
                playerShip.rotateRight();
            }

            //Space key activates debugging brake
            if (keyState.IsKeyDown(Keys.Space))
            {
                playerShip.stop();
            }

            mainCamera.setZoom(mouseState.ScrollWheelValue);
            mainCamera.Position = playerShip.CenterPosition; // center the camera to player's position
            mainCamera.update(gameTime);

            /* Transform mouse input from view to world position
             * NOT currently used but may be useful in the future*/
            Matrix inverse = Matrix.Invert(mainCamera.getTransformation());
            Vector2 mousePos = Vector2.Transform(
               new Vector2(mouseState.X, mouseState.Y), inverse);

            if (keyState.IsKeyDown(Keys.LeftControl))
            {
                // Fire only every interval we set as the fireTime
                if (gameTime.TotalGameTime - previousFireTime > fireTime)
                {
                    // Reset our current time
                    previousFireTime = gameTime.TotalGameTime;

                    // Add the projectile, but add it to the front and center of the player
                    AddProjectile(playerShip.Position + new Vector2(0, 0));
                }
            }

            playerShip.update();
            UpdateDamageCollision();
            gui.update(playerShip);
            UpdateProjectiles();
            
        }

        private void UpdateDamageCollision()
        {
            // Use the Rectangle's built-in intersect function to 
            // determine if two objects are overlapping
            Rectangle rectangle1;
            Rectangle rectangle2;
            Assets = Game1.Assets;
            // Only create the rectangle once for the player
            rectangle1 = new Rectangle((int)(playerShip.Position.X ),
            (int)(playerShip.Position.Y),
            (int)(Assets.spaceShipTest.Width * playerShip.getScale()),
            (int)(Assets.spaceShipTest.Height * playerShip.getScale()));

            // Do the collision between the player and the enemies
  
                rectangle2 = new Rectangle((int)winflag.Position.X,
                (int)winflag.Position.Y,
                Assets.winflag1.Width,
                Assets.winflag1.Height);

                // Determine if the two objects collided with each
                // other
                if (rectangle1.Intersects(rectangle2) && invinsible == false)
                {
                    invinsible = true;
                    invinsibilityTimer.Enabled = true;
                    playerShip.setHealth(playerShip.getHealth() - 1);
                    flashingTimer.Enabled = true;
                }
                if (invinsible == false)
                {
                    invinsibilityTimer.Enabled = false;
                    flashingTimer.Enabled = false;
                }
                if (playerShip.getHealth() == 0)
                {
                    playerShip.setHealth(100);
                    game.EndMatch();
                    invinsibilityTimer.Enabled = false;
                }

        }

        private void UpdateProjectiles()
        {
            // Update the Projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update();

                if (projectiles[i].getActive() == false)
                {
                    projectiles.RemoveAt(i);
                }
            }
        }


        public void draw()
        {

            /* Main camera sprite batch */
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,
                SamplerState.LinearWrap, null, null, null, mainCamera.getTransformation());


            drawWorld(); //Draws background

            playerShip.draw(spriteBatch); //Draws player space ship
            winflag.draw(spriteBatch);
            // Draw the Projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].draw(spriteBatch);
            }


            spriteBatch.End();




            /* GUI spritebatch. Anything drawn here will remain
             * static and not be affected by cameras. */
            spriteBatch.Begin();

            gui.draw(spriteBatch);


            spriteBatch.End();
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            invinsible = false;
        }
        private static void onTimedEventFlashing(Object source, ElapsedEventArgs e)
        {
            if (flashFlag == true)
            {
                playerShip.setAlpha(0);
                flashFlag = false;
            }
            if (flashFlag == false)
            {
                playerShip.setAlpha(255);
                flashFlag = true;
            }
        }

    }
}
