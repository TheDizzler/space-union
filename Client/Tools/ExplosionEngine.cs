using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using SpaceUnion.Controllers;
using SpaceUnion.Tools;

namespace SpaceUnion.Tools {

	public class ExplosionEngine {

		List<Explosion> explosions = new List<Explosion>();
		private AssetManager assets;

		public ExplosionEngine(AssetManager assetMan) {
			

			assets = assetMan;
		}

		/// <summary>
		/// Cover a ship in little explosions.
		/// </summary>
		/// <param name="ship"></param>
		public void createExplosions(Ship ship) {

			// generate an explosion in a random spot on ship
			Random gen = new Random();

			Vector2 location = new Vector2(gen.Next(ship.width), gen.Next(ship.height));
			Explosion explosion = new Explosion(assets.explosions, location);

			//explosion.
		}
	}
}
