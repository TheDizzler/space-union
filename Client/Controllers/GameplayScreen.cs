using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using SpaceUnion.StellarObjects;
using Client_Comm_Module;
using Data_Structures;


namespace SpaceUnion.Controllers {

    class GameplayScreen
    {

        private KeyboardState keyState;
        private MouseState mouseState;
        private SpriteBatch spriteBatch;
        private Ship playerShip;
		private Planet planet;
        private Game1 game;
        //static private bool flashFlag = false;
        //private static System.Timers.Timer invinsibilityTimer;
        //private static System.Timers.Timer flashingTimer;
        List<Projectile> projectiles;
        List<Ship> ships;

        // The rate of fire of the player laser
        TimeSpan fireTime;
        TimeSpan previousFireTime;

        Camera mainCamera;
        GUI gui;

        private Tools.AssetManager Assets;

        static public int worldWidth = 4000;
        static public int worldHeight = 2000;
        //static protected bool invinsible;
        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;

        //NETWORKING


        public GameplayScreen(Game1 game, SpriteBatch batch)
        {
            this.game = game;
            SCREEN_HEIGHT = game.getScreenHeight();
            SCREEN_WIDTH = game.getScreenWidth();

            spriteBatch = batch;

            /*
            invinsibilityTimer = new System.Timers.Timer(2000);
            invinsibilityTimer.Elapsed += OnTimedEvent;
            invinsibilityTimer.Enabled = false;
            flashingTimer = new System.Timers.Timer(50);
            flashingTimer.Elapsed += onTimedEventFlashing;
            flashingTimer.Enabled = false;
            flashFlag = true;
            invinsible = false;
            */

            
            Assets = Game1.Assets;
            playerShip = new Ship(Assets.spaceShipTest, new Vector2(200, 200)); //Create new player ship
			planet = new Planet(Assets.waterPlanet, new Vector2(1000, 1000));

			gui = new GUI(game, playerShip, planet);

            Viewport mainViewport = new Viewport((int)playerShip.getX(), (int)playerShip.getY(),
                game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height - GUI.guiHeight);
            mainCamera = new Camera(mainViewport, worldWidth, worldHeight, 1.0f);

            projectiles = new List<Projectile>();
            ships = new List<Ship>();

            // Set the laser to fire every quarter second
            fireTime = TimeSpan.FromSeconds(.15f);
            ships.Add(playerShip);


        }

        /// <summary>
		/// Draws the stars background.
        /// </summary>
        protected void drawWorld()
        {

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
		/// Checks player edge wrap around/edge stop.
        /// 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            /*
            GameMessage testString = clientCommHandler.getGameMessage(); //Receive server string
            if (testString != null)
            {
                Console.WriteLine("Game Message: " + testString.Message);
            }
            else {
                //Console.WriteLine("No Message Received from Server");
            }
            */


            //playerShip.checkScreenWrap(Window); //Check if ship will wrap around edges
            playerShip.checkScreenStop(game.Window);
            keyState = Keyboard.GetState(); //Get which keys are pressed or released
            mouseState = Mouse.GetState();



            //Up Key toggles back thruster
            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W)) {
				playerShip.thrust(gameTime);
            }

            //Left Key rotates ship left
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A)) {
				playerShip.rotateLeft(gameTime);
            }

            //Right Key rotates ship right
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D)){
				playerShip.rotateRight(gameTime);
            }

            //Space key activates debugging brake
            if (keyState.IsKeyDown(Keys.Space))
            {
                playerShip.stop();
            }


			planet.update(gameTime, playerShip);
			playerShip.update(gameTime);

			

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

            UpdateDamageCollision();
			playerShip.update(gameTime);
            gui.update(playerShip);
            UpdateProjectiles();

            //NETWORKING
            GameData data = new GameData();
            data.Player = MainMenuScreen.player;
            data.Health = (byte)playerShip.getHealth();
            data.Time = DateTime.Now;
            data.Angle = (float)playerShip.getRotation();
            data.XPosition = playerShip.getX();
            data.YPosition = playerShip.getY();
            MainMenuScreen.clientCommHandler.sendGameData(data);
        }

        private void UpdateDamageCollision()
        {
            // Use the Rectangle's built-in intersect function to 
            // determine if two objects are overlapping
            foreach(Projectile p in projectiles){
                if (p.getProjectileHitBox().getArray().Intersects(playerShip.getShipHitBox().getArray())) {
                    playerShip.setHealth(-1);
                }
            }
            //Assets = Game1.Assets;
            // Only create the rectangle once for the player
            //rectangle1 = new Rectangle((int)(playerShip.Position.X ),
            //(int)(playerShip.Position.Y),
            //(int)(Assets.spaceShipTest.Width * playerShip.getScale()),
            //(int)(Assets.spaceShipTest.Height * playerShip.getScale()));

            /*
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
            */
        }

        //Comment
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

            GameData[] players = MainMenuScreen.clientCommHandler.getPlayersData();
            for (int x = 0; x < players.Length; x++)
            {
                if (players[x] != null)
                {
                    Console.WriteLine(players[x].Player.Username);
                    Ship otherPlayer = new Ship(Assets.spaceShipTest, new Vector2(200, 200)); //Create new player ship
                    otherPlayer.updateX(players[x].XPosition);
                    otherPlayer.updateY(players[x].YPosition);
                    otherPlayer.updateRotation(players[x].Angle); 
                    otherPlayer.draw(spriteBatch); //Draws other player
                }
            }

            //loop everty ship and draw 
			planet.draw(spriteBatch);
            
            
            // Draw the Projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                spriteBatch.Draw(Assets.shuttle, projectiles[i].getProjectileHitBox().getArray(), Color.Red);
                //projectiles[i].draw(spriteBatch); //This is a debug statement to view where the hitboxes are
                
            }


            spriteBatch.End();




            /* GUI spritebatch. Anything drawn here will remain
             * static and not be affected by cameras. */
            spriteBatch.Begin();

            gui.draw(spriteBatch);


            spriteBatch.End();
        }
        /*
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
        */
    }
}
