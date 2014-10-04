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

		Random gen1, gen2;

		public ExplosionEngine(AssetManager assetMan) {

			assets = assetMan;

			 gen1 = new Random();
			 gen2 = new Random();
		}

		/// <summary>
		/// Cover a ship in little explosions.
		/// </summary>
		/// <param name="ship"></param>
		public void createExplosions(Ship ship) {

			// generate an explosion in a random spot on ship
			

			Vector2 location = new Vector2(ship.getX() + gen2.Next(ship.width) - ship.width / 2, ship.getY() - gen2.Next(ship.height) + ship.height / 2);

			float scale = (float) gen1.NextDouble() + .5f;

			Explosion explosion = null;
			switch (new Random().Next(4)) {
				case 0:
				case 1:
				case 2:
				explosion = new Explosion(assets.explosions, location, scale);
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
