using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.StellarObjects;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Animations;

using Data_Manipulation;
using Data_Structures;
using SpaceUnionXNA.Weapons.Projectiles;
using System.Threading;

namespace SpaceUnionXNA.Controllers
{
    public class GameplayScreen
    {
        private KeyboardState keyState;
        private MouseState mouseState;
        private SpriteBatch spriteBatch;

        protected QuadTree quadTree;
        protected Dictionary<String, Ship> ships;
        protected Dictionary<String, Ship> inactiveShips;
        /// <summary>
        /// All objects in this list get added to the quadtree every update.
        /// It is ESSENTIAL that newly spawned objects get added to this list when
        /// created. All objects remove themselves when destroy() is called.
        /// </summary>
        public static List<Tangible> targets;
        private List<Vector2> respawnpoints;
        private List<Vector2> usedspawn;
        private Background background;
        private Ship playerShip;
        private Game1 game;
        private Camera mainCamera;
        private Camera radarCamera;
        protected GUI gui;

        Random gen;

        private AssetManager Assets;

        static public int worldWidth = 1000;
        static public int worldHeight = 750;

        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;
        protected Viewport basicViewport;

        private GameData gameData = new GameData(); //Game Data is sent to server
        private GameFrame gameFrame; //Game Frame is received from the server. Game Frames contain multiple game data packets. 
        private byte playerShipChoice; //Used to assign the player's ship choice.

        public GameplayScreen(Game1 game, SpriteBatch batch)
        {
            this.game = game;
            SCREEN_HEIGHT = game.getScreenHeight();
            SCREEN_WIDTH = game.getScreenWidth();

            //Test
            playerShipChoice = 1;

            quadTree = new QuadTree(0, new Rectangle(0, 0, worldWidth, worldHeight));

            spriteBatch = batch;

            gen = new Random();
            Assets = Game1.Assets;

            ships = new Dictionary<String, Ship>();
            inactiveShips = new Dictionary<String, Ship>();
            targets = new List<Tangible>();

            //Respawn Points
            respawnpoints = new List<Vector2>();
            usedspawn = new List<Vector2>();

            respawnpoints.Add(new Vector2(worldWidth / 2, worldHeight - 100));
            respawnpoints.Add(new Vector2(worldWidth - 100, worldHeight / 2));
            respawnpoints.Add(new Vector2(worldWidth / 2, worldHeight));
            respawnpoints.Add(new Vector2(100, worldHeight / 2));
            respawnpoints.Add(new Vector2(100, 100));
            respawnpoints.Add(new Vector2(worldWidth - 100, 100));
            respawnpoints.Add(new Vector2(worldWidth - 100, worldHeight - 100));
            respawnpoints.Add(new Vector2(100, worldHeight - 100));

            int roomNumber = game.Player.GameRoom;
            RoomInfo roomInfo = (RoomInfo)game.Communication.sendRoomInfoRequest(game.Player, roomNumber);

            //Assign all other ships in lobby to ship list.
            if (roomInfo != null)
            {
                foreach (var p in game.roomInfo.Players)
                {
                    if (p.Player.Username != game.Player.Username)
                    {
                        Ship ship;
                        switch (p.Player.ShipChoice)
                        {
                            case 0:
                                ship = new AlphaShip(this.game, p.Team);
                                break;
                            case 1:
                                ship = new ThetaShip(this.game, p.Team);
                                break;
                            case 2:
                                ship = new OmegaShip(this.game, p.Team);
                                break;
                            default:
                                ship = new ThetaShip(this.game, p.Team);
                                break;
                        }
                        ship.Position = respawnpoints.ElementAt(p.SpawnPosition);
                        ship.username = p.Player.Username;
                        ships.Add(ship.username, ship);
                    }
                    else
                    {
                        playerShipChoice = p.Player.ShipChoice;
                    }
                }
            }

            /*
            for (int i = 0; i < 3; i++)
            {
                Ship enemyship = new Bug(game);
                enemyship.Position = respawnpoints.ElementAt(i + 3);
                enemyship.blueTeam = true;
                enemyship.rotation = (float)Math.PI / 4;
                enemyship.controlAI();
                ships.Add(enemyship);
            }
            for (int i = 0; i < 2; i++)
            {
                Ship friendlyship = new Scout(game);
                friendlyship.Position = respawnpoints.ElementAt(i + 1);
                friendlyship.redTeam = true;
                ships.Add(friendlyship);
            }
            */

            background = new Background(worldWidth, worldHeight, Assets.starfield2, Assets.starfield1, Assets.starfield1, Assets.starfield1);

            //Create the player ship
            switch (playerShipChoice)
            {
                case 0:
                    playerShip = new AlphaShip(this.game, game.PlayerTeam);
                    playerShip.isPlayerShip = true;
                    playerShip.spawnPosition = game.PlayerSpawnPosition;
                    break;
                case 1:
                    playerShip = new ThetaShip(this.game, game.PlayerTeam);
                    playerShip.isPlayerShip = true;
                    playerShip.spawnPosition = game.PlayerSpawnPosition;
                    break;
                case 2:
                    playerShip = new OmegaShip(this.game, game.PlayerTeam);
                    playerShip.isPlayerShip = true;
                    playerShip.spawnPosition = game.PlayerSpawnPosition;
                    break;
                default:
                    playerShip = new AlphaShip(this.game, game.PlayerTeam);
                    playerShip.isPlayerShip = true;
                    playerShip.spawnPosition = game.PlayerSpawnPosition;
                    break;
            }

            playerShip.username = game.Player.Username;
            playerShip.Position = respawnpoints.ElementAt(playerShip.spawnPosition);

            basicViewport = game.GraphicsDevice.Viewport;

            gui = new GUI(game);

            Viewport mainViewport = new Viewport
            {
                X = 0,
                Y = 0,
                Width = game.GraphicsDevice.Viewport.Width,
                Height = game.GraphicsDevice.Viewport.Height - GUI.guiHeight
            };

            Viewport radarViewport = new Viewport
            {
                X = gui.radarBox.X,
                Y = gui.radarBox.Y,
                Width = gui.radarBox.Width,
                Height = gui.radarBox.Height
            };

            mainCamera = new Camera(mainViewport, worldWidth, worldHeight, 0.5f);
            radarCamera = new Camera(radarViewport, worldWidth, worldHeight, 0.05f);

            ships.Add(game.Player.Username, playerShip);

            foreach (var key in ships.Keys)
                targets.Add(ships[key]);

            gameData.Angle = playerShip.rotation;
            gameData.Player = this.game.Player;
            gameData.IsActive = true;
            gameData.XPosition = playerShip.Position.X;
            gameData.YPosition = playerShip.Position.Y;
            gameData.SpawnPosition = playerShip.spawnPosition;
            gameData.Health = (byte)playerShip.currentHealth;
            gameData.Bullets = ConvertProjectileToArray(playerShip.projectiles);
            game.Communication.sendGameData(gameData);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// Basic flow of "turn":
        /// create QuadTree, get player input, apply gravity,
        /// movement (ships, asteroids?), camera update, collision detection(?),
        /// explosion handling, gui update.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
            GameFrame gameFrame = game.Communication.getGameData();
            if (gameFrame == null)
                gameFrame = this.gameFrame;

            if (gameFrame != null)
            {

                for (int i = 0; i < gameFrame.Data.Count(); i++)
                {
                    if (gameFrame.Data[i] != null && ships.ContainsKey(gameFrame.Data[i].Player.Username))
                    {
                        ships[gameFrame.Data[i].Player.Username].currentHealth = gameFrame.Data[i].Health;
                        ships[gameFrame.Data[i].Player.Username].projectiles = new List<Laser>();
                        ships[gameFrame.Data[i].Player.Username].projectiles = ConvertArrayProjectileToList(gameFrame.Data[i].Bullets, gameFrame.Data[i].Player.Username);
                        ships[gameFrame.Data[i].Player.Username].kills = gameFrame.Data[i].Kills;
                        ships[gameFrame.Data[i].Player.Username].spawnPosition = gameData.SpawnPosition;
                        ships[gameFrame.Data[i].Player.Username].position.X = gameFrame.Data[i].XPosition;
                        ships[gameFrame.Data[i].Player.Username].position.Y = gameFrame.Data[i].YPosition;
                        ships[gameFrame.Data[i].Player.Username].rotation = gameFrame.Data[i].Angle;
                    }
                }
            }
            gui.update(gameTime, quadTree);

            quadTree.clear();
            foreach (Tangible target in targets)
                quadTree.insert(target);


            keyState = Keyboard.GetState(); //Get which keys are pressed or released
            mouseState = Mouse.GetState();


            playerShip.control(keyState, gameTime); //Get keyboard input for player ship.

            foreach (Ship ship in ships.Values.ToList())
            {
                if (!ship.isActive)
                {
                    ship.inactiveStart = gameTime.TotalGameTime;
                    inactiveShips.Add(ship.username, ship);
                    ships.Remove(ship.username);
                }
                ship.update(gameTime, quadTree);
            }

            foreach (Ship ship in inactiveShips.Values.ToList())
            {
                    if (ship.isActive)
                    {
                        ship.inactiveTime = TimeSpan.Zero;
                        ship.Position = respawnpoints.ElementAt(ship.spawnPosition);
                        Console.WriteLine("respawn");
                        ships.Add(ship.username, ship);
                        targets.Add(ship);
                        inactiveShips.Remove(ship.username);
                        if (ship.username == playerShip.username)
                        {
                            gameData.Angle = ship.rotation;
                            gameData.Player = this.game.Player;
                            gameData.XPosition = ship.Position.X;
                            gameData.YPosition = ship.Position.Y;
                            gameData.SpawnPosition = ship.spawnPosition; 
                            gameData.Bullets = ConvertProjectileToArray(ship.projectiles);
                            gameData.Health = (byte)ship.currentHealth;
                            game.Communication.sendGameData(gameData);

                        }
                    /*
                    ship.Position = respawnpoints.ElementAt(randomspawn.Next(respawnpoints.Count));
                    usedspawn.Add(respawnpoints.ElementAt(respawnpoints.IndexOf(ship.Position)));
                    respawnpoints.RemoveAt(respawnpoints.IndexOf(ship.Position));
                    ships.Add(ship.username, ship);
                    inactiveShips.Remove(ship.username);
                      */
                }
                ship.update(gameTime, quadTree);
            }
            /*
            foreach (Vector2 spawn in usedspawn.ToList())
            {
                respawnpoints.Add(spawn);
                usedspawn.Remove(spawn);
            }*/


            /** Camera Debugging **/
            if (keyState.IsKeyDown(Keys.P))
                mainCamera.zoom += Camera.zoomIncrement;
            if (keyState.IsKeyDown(Keys.O))
                mainCamera.zoom -= Camera.zoomIncrement;
            /** Camera Debugging **/


            mainCamera.setZoom(mouseState.ScrollWheelValue);
            mainCamera.Position = playerShip.Position; // center the camera to player's position
            mainCamera.update(gameTime);

            radarCamera.Position = playerShip.Position; // center the camera to player's position
            radarCamera.update(gameTime);

            Game1.explosionEngine.update(gameTime);


            //Networking.
            //Send Game Data to Server
            if (playerShip.isActive)
            {
                gameData.Angle = playerShip.rotation;
                gameData.Player = this.game.Player;
                gameData.XPosition = playerShip.Position.X;
                gameData.YPosition = playerShip.Position.Y;
                gameData.Kills = playerShip.kills;
                gameData.SpawnPosition = playerShip.spawnPosition;
                gameData.Bullets = ConvertProjectileToArray(playerShip.projectiles);
                gameData.Health = (byte)playerShip.currentHealth;
                game.Communication.sendGameData(gameData);
            }
            Thread.Sleep(3);


            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void draw(GameTime gameTime)
        {
            game.graphics.GraphicsDevice.Viewport = mainCamera.viewport;

            //Networking

            /* Main camera sprite batch */
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.LinearClamp, null, null, null, mainCamera.getTransformation());


            background.draw(spriteBatch, mainCamera);
            drawBorder(spriteBatch, new Rectangle(0 - Assets.galactusship.Width / 2 - 10, 0 - Assets.galactusship.Height / 2 - 10,
                   worldWidth + Assets.galactusship.Width + 20, worldHeight + Assets.galactusship.Height + 20), 10, Color.White);
            drawShips();
            spriteBatch.End();



            /* GUI spritebatch. Anything drawn here will remain
             * static and not be affected by cameras. */
            game.graphics.GraphicsDevice.Viewport = basicViewport;
            spriteBatch.Begin();
            gui.draw(spriteBatch);
            spriteBatch.End();


            /** Radar camera spritebatch */
            game.graphics.GraphicsDevice.Viewport = radarCamera.viewport;
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                    SamplerState.LinearClamp, null, null, null, radarCamera.getTransformation());
            drawGrid(); //draw grid

            foreach (Ship ship in ships.Values.ToList())
                ship.drawMiniMap(spriteBatch);

            spriteBatch.End();
        }

        private void drawShips()
        {

            foreach (Ship ship in ships.Values.ToArray())
            {
                ship.draw(spriteBatch);

                if (ship.projectiles != null)
                {
                    foreach (SpaceUnionXNA.Weapons.Projectiles.Projectile p in ship.projectiles)
                    {
                        p.draw(spriteBatch);
                    }
                }
            }
        }

        /// <summary>
        /// Draw grid on radar screen
        /// </summary>
        private void drawGrid()
        {

            drawBorder(spriteBatch, new Rectangle(0, 0, worldWidth, worldHeight), 15, Color.White); // World enclosing box

            drawBorder(spriteBatch, new Rectangle(0, 0, worldWidth / 4, worldHeight / 4), 15, Color.White); //top
            drawBorder(spriteBatch, new Rectangle(worldWidth / 2, 0, worldWidth / 4, worldHeight / 4), 15, Color.White); //top right

            drawBorder(spriteBatch, new Rectangle(worldWidth / 4, worldHeight / 4, worldWidth / 4, worldHeight / 4), 15, Color.White);
            drawBorder(spriteBatch, new Rectangle(worldWidth * 3 / 4, worldHeight / 4, worldWidth / 4, worldHeight / 4), 15, Color.White);

            drawBorder(spriteBatch, new Rectangle(0, worldHeight / 2, worldWidth / 4, worldHeight / 4), 15, Color.White);
            drawBorder(spriteBatch, new Rectangle(worldWidth / 2, worldHeight / 2, worldWidth / 4, worldHeight / 4), 15, Color.White);

            drawBorder(spriteBatch, new Rectangle(worldWidth / 4, worldHeight * 3 / 4, worldWidth / 4, worldHeight / 4), 15, Color.White);
            drawBorder(spriteBatch, new Rectangle(worldWidth * 3 / 4, worldHeight * 3 / 4, worldWidth / 4, worldHeight / 4), 15, Color.White);
        }


        /// <summary>
        /// Hollow rectangle drawing code from:
        /// http://bluelinegamestudios.com/posts/drawing-a-hollow-rectangle-border-in-xna-4-0/
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="rectangleToDraw"></param>
        /// <param name="thicknessOfBorder"></param>
        /// <param name="borderColor"></param>
        private void drawBorder(SpriteBatch batch, Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            batch.Draw(Assets.pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            batch.Draw(Assets.pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            batch.Draw(Assets.pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            batch.Draw(Assets.pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }


        /// <summary>
        /// Converts the Projectile Dictionary to an array of server projectiles.
        /// </summary>
        /// <param name="projectiles"></param>
        /// <returns></returns>
        public Data_Structures.Projectile[] ConvertProjectileToArray(List<SpaceUnionXNA.Weapons.Projectiles.Laser> projectiles)
        {

            Data_Structures.Projectile[] result = new Data_Structures.Projectile[projectiles.Count];
            int i = 0;
            foreach (SpaceUnionXNA.Weapons.Projectiles.Projectile projectile in projectiles.ToList())
            {
                Data_Structures.Projectile arrproj = new Data_Structures.Projectile();
                arrproj.PositionX = projectile.Position.X;
                arrproj.PositionY = projectile.Position.Y;
                arrproj.Rotation = projectile.rotation;
                arrproj.VelocityX = projectile.getVelocityX();
                arrproj.VelocityY = projectile.getVelocityY();
                arrproj.Username = projectile.owner.username;
                arrproj.TimeActive = projectile.timeActive;
                result[i] = arrproj;
                i++;
                //projectiles.Remove(projectile);
            }
            return result;
        }

        /// <summary>
        /// Converts the server projectiles back to projectile dictionary.
        /// </summary>
        /// <param name="arrproj"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<SpaceUnionXNA.Weapons.Projectiles.Laser> ConvertArrayProjectileToList(Data_Structures.Projectile[] arrproj, string username)
        {
            List<SpaceUnionXNA.Weapons.Projectiles.Laser> laserList = new List<SpaceUnionXNA.Weapons.Projectiles.Laser>();

            if (arrproj == null || arrproj.Length < 1)
            {
                return laserList;
            }

            foreach (Data_Structures.Projectile projectile in arrproj)
            {
                if (projectile != null)
                {
                    Laser laser = new Laser(new Vector2(projectile.PositionX, projectile.PositionY), ships[username]);
                    laser.timeActive = projectile.TimeActive;
                    laserList.Add(laser);
                    laser.launch(new Vector2(projectile.PositionX, projectile.PositionY)
                        , projectile.Rotation, new Vector2(projectile.VelocityX, projectile.VelocityY));

                }
            }
            return laserList;
        }


    }
}
