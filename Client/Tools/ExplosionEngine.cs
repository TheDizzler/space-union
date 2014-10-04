using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using SpaceUnion.Animations;
using SpaceUnion.Controllers;
using SpaceUnion.Tools;

namespace SpaceUnion.Tools {

	public class ExplosionEngine {

		List<Explosion> explosions = new List<Explosion>();
		private AssetManager assets;

		Random gen;


		public ExplosionEngine(AssetManager assetMan) {

			assets = assetMan;
			gen = new Random();
		}

		/// <summary>
		/// Cover a ship in little explosions.
		/// </summary>
		/// <param name="ship"></param>
		public void createExplosions(Ship ship) {

			// generate an explosion in a random spot on ship
			Vector2 location = new Vector2(ship.getX() + gen.Next(ship.width) - ship.width / 2,
				ship.getY() - gen.Next(ship.height) + ship.height / 2);
			// randomize the size of the explosion
			float scale = (float) gen.NextDouble() + .5f;

			Explosion explosion = null;
			switch (new Random().Next(4)) {
				case 0:
				case 1:
				case 2:
					explosion = new ShortExplosion(assets.explosions, location, scale);
					break;
				case 3:
					explosion = new BigExplosion(assets.explosionsBig, location, scale);
					break;
			}
			explosions.Add(explosion);
		}


		public void update(GameTime gameTime) {

			for (int i = 0; i < explosions.Count; ++i) {
				explosions[i].update(gameTime);
				if (explosions[i].isDead)
					explosions.RemoveAt(i);
			}
		}

		public void draw(SpriteBatch spriteBatch) {

			foreach (Explosion explosion in explosions)
				explosion.draw(spriteBatch);
		}

	}
}
