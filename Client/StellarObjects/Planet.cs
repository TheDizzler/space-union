using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Ships;
using SpaceUnion.Tools;
using SpaceUnion.Weapons;
using SpaceUnion.Weapons.Projectiles;


namespace SpaceUnion.StellarObjects {

	public class Planet : LargeMassObject {

		/// <summary>
		/// Damage given from collision
		/// </summary>
		private int damage = 20;
		public int collisionDamage {
			get { return damage; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tex">planet texture</param>
		/// <param name="pos">Location in game coordinates of planet</param>
		/// <param name="mass">100 is weak, 1000 is very strong</param>
		/// <param name="range">Range (in pixels) that gravitational effects span</param>
		public Planet(Texture2D tex, Vector2 pos, float mass, float range)
			: base(tex, pos, mass, range) {

				
		}


		public override void drawMiniMap(SpriteBatch batch) {
			throw new NotImplementedException();
		}

		public override void collide(Tangible target, GameTime gameTime) {

			if (target is Projectile)
				target.collide(this, gameTime);
			else if (target is Ship)
				CollisionHandler.shipOnPlanet((Ship) target, this, gameTime);
			else if (target is Asteroid)
				CollisionHandler.asteroidOnPlanet((Asteroid) target, this, gameTime);
			else if (target is Planet)
				CollisionHandler.planetOnPlanet(this, (Planet) target, gameTime);
			else
				throw new NotImplementedException();
		}
	}
}
