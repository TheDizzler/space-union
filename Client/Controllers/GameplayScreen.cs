using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceUnion
{
    class GameplayScreen
    {
        private Texture2D background;
        private Texture2D shuttle;
        private SpriteFont font;
        private KeyboardState state;
        private SpriteBatch spriteBatch;
        private Ship playerShip;
        private Game1 game;
        private Boolean collide = false;

        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;

        public GameplayScreen(Game1 game)
        {
            this.game = game;
            SCREEN_HEIGHT = game.getScreenHeight();
            SCREEN_WIDTH = game.getScreenWidth();
            background = game.Content.Load<Texture2D>("Backgrounds/background");
            shuttle = game.Content.Load<Texture2D>("Spaceships/spaceshiptest");
            font = game.Content.Load<SpriteFont>("SpriteFonts/SpriteFont1"); // Use the name of your sprite font file here instead of 'Score'.
            playerShip = new Ship(shuttle, new Vector2(200, 200)); //Create new player ship
        }

        /// <summary>
        /// Draws the stars background and debug information.
        /// </summary>
        protected void DrawWorld()
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), Color.White);
            spriteBatch.DrawString(font, "Radian Angle =" + playerShip.getAngle(), new Vector2(100, 20), Color.Red);
            spriteBatch.DrawString(font, "Degree Angle =" + (playerShip.getAngle() * (180 / Math.PI)), new Vector2(100, 50), Color.Red);
            spriteBatch.DrawString(font, "X =" + playerShip.getShipVelocityDirectionX()
                + " y = " + playerShip.getShipVelocityDirectionY(), new Vector2(100, 80), Color.Red);
            spriteBatch.DrawString(font, "X =" + playerShip.getSpaceshipX()
                + " y = " + playerShip.getSpaceshipY(), new Vector2(100, 110), Color.Red);
        }

        /*
        public void UpdateCollisions() 
        {
            // Use the Rectangle's built-in intersect function to 
            // determine if two objects are overlapping
            Rectangle rectangle1;
            Rectangle rectangle2;

            // Only create the rectangle once for the player
            rectangle1 = playerShip.getHitBox();
            rectangle2 = enemyShip.getHitBox();
            if (rectangle1.Intersects(rectangle2))
            {
                collide = true;
            }
            else {
                collide = false;
            }
        }
        */

         
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
            playerShip.checkScreenWrap(game.Window); //Check if ship will wrap around edges
            //UpdateCollisions();
            state = Keyboard.GetState(); //Get which keys are pressed or released

            //Up Key toggles back thruster
            if (state.IsKeyDown(Keys.Up))
            {
                playerShip.thrust();
            }

            //Left Key rotates ship left
            if (state.IsKeyDown(Keys.Left))
            {
                playerShip.rotateLeft();
            }

            //Right Key rotates ship right
            else if (state.IsKeyDown(Keys.Right))
            {
                playerShip.rotateRight();
            }

            //Space key activates debugging brake
            if (state.IsKeyDown(Keys.Space))
            {
               playerShip.stop();
                
            }

        }

        /// <summary>
        /// Draws the ship
        /// </summary>
        protected void DrawShip()
        {
            playerShip.draw(spriteBatch);
        }

        public void Draw(SpriteBatch sBatch)
        {
            spriteBatch = sBatch;
            DrawWorld(); //Draws background
            DrawShip();  //Draws player space ship
        }

    }
}
