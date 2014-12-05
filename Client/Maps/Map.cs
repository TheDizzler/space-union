using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Animations;
using SpaceUnionXNA.StellarObjects;
using SpaceUnionXNA.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceUnionXNA.Maps {

	public class Map {

		public int worldWidth = 1000;
		public int worldHeight = 750;

		public List<LargeMassObject> planets;
		public List<Asteroid> asteroids;
		private List<Tangible> targets;
		public List<Vector2> respawnpoints;
		public List<Vector2> usedspawn;
		public Background background;

		Random gen;
		protected Game1 game;


		public Map(int mapWidth, int mapHeight, Game1 game) {
			
			this.game = game;
			gen = new Random();

			worldWidth = mapWidth;
			worldHeight = mapHeight;

			asteroids = new List<Asteroid>();

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


			background = new Background(worldWidth, worldHeight,
				Game1.Assets.starfield2, Game1.Assets.starfield1,
				Game1.Assets.starfield1, Game1.Assets.starfield1);

			planets = new List<LargeMassObject>();
			planets.Add(new Planet(Game1.Assets.waterPlanet, new Vector2(4000, 3000), 500f, 1000, game));
			planets.Add(new Planet(Game1.Assets.moon, new Vector2(1000, 1000), 250f, 800, game));
		}


		public void init(List<Tangible> trgts) {
			targets = trgts;
			for (int i = 0; i < 5; i++)
				AddAsteroid(new Vector2(gen.Next(100, worldWidth), gen.Next(100, worldHeight)));
			foreach (Planet planet in planets)
				targets.Add(planet);
		}


		private void AddAsteroid(Vector2 position) {
			Asteroid asteroid = new Asteroid(Game1.Assets.asteroid, position, game);
			asteroids.Add(asteroid);
			targets.Add(asteroid);
		}


		public void respawn(Ships.Ship ship) {

			Random randomspawn = new Random();
			ship.isActive = true;
			targets.Add(ship);

			ship.resetShip();
			Vector2 position = respawnpoints.ElementAt(randomspawn.Next(respawnpoints.Count));
			while (spawnPointOccupied(position))
				position = respawnpoints.ElementAt(randomspawn.Next(respawnpoints.Count));

			ship.Position = position;
			usedspawn.Add(respawnpoints.ElementAt(respawnpoints.IndexOf(ship.Position)));
			respawnpoints.RemoveAt(respawnpoints.IndexOf(ship.Position));
		}


		private bool spawnPointOccupied(Vector2 position) {

			foreach (Tangible target in targets) {
				if (target.isActive) {

					if (target.getHitBox().rectHitBox.Contains(new Point((int) position.X, (int) position.Y))) {
						return true;
					}
				}
			}
			return false;
		}


		public void update(GameTime gameTime, QuadTree quadTree) {

			foreach (Vector2 spawn in usedspawn.ToList()) {
				respawnpoints.Add(spawn);
				usedspawn.Remove(spawn);
			}

			foreach (Planet planet in planets)
				planet.update(gameTime, quadTree, targets);

			for (int i = asteroids.Count - 1; i >= 0; i--) {
				asteroids[i].update(gameTime, quadTree);
				if (!asteroids[i].isActive) {
					asteroids.RemoveAt(i);
				}
			}
		}


		public void drawMiniMap(SpriteBatch spriteBatch) {

			//draw grid
			drawGrid(spriteBatch);

			foreach (LargeMassObject planet in planets)
				planet.draw(spriteBatch);

			// Draw the Asteroids
			for (int i = 0; i < asteroids.Count; i++) {
				asteroids[i].draw(spriteBatch);
			}
		}


		public void draw(SpriteBatch spriteBatch) {

			foreach (LargeMassObject planet in planets)
				planet.draw(spriteBatch);

			// Draw the Asteroids
			for (int i = 0; i < asteroids.Count; i++) {
				asteroids[i].draw(spriteBatch);
			}

			drawBorder(spriteBatch, new Rectangle(0 - 8 - 10, 0 - 8 - 10,
					   worldWidth +8 + 20, worldHeight + 8 + 20), 10, Color.White);
		}


		/// <summary>
		/// Draw grid on radar screen
		/// </summary>
		private void drawGrid(SpriteBatch spriteBatch) {

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
		private void drawBorder(SpriteBatch batch, Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor) {
			// Draw top line
			batch.Draw(Game1.Assets.pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

			// Draw left line
			batch.Draw(Game1.Assets.pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

			// Draw right line
			batch.Draw(Game1.Assets.pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
											rectangleToDraw.Y,
											thicknessOfBorder,
											rectangleToDraw.Height), borderColor);
			// Draw bottom line
			batch.Draw(Game1.Assets.pixel, new Rectangle(rectangleToDraw.X,
											rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
											rectangleToDraw.Width,
											thicknessOfBorder), borderColor);
		}
	}
}
