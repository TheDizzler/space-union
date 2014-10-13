using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Weapons;


namespace SpaceUnion.Ships {
	class Scout : Ship {


		bool beamOn = false;
		private LaserBeam beam;

		public Scout(Game1 game1)
			: base(assets.stunt, assets.laser, game1) {

			accelSpeed = 50;
			weaponOrigin = new Vector2(position.X, position.Y - height / 2);

			beam = new LaserBeam(Vector2.Add(position, weaponOrigin), this);
		}

		/// <summary>
		/// Main weapon fire method
		/// </summary>
		/// <param name="gameTime"></param>
		public override void fire(GameTime gameTime) {

			// Fire only every interval we set as the fireTime
			//if (!beamOn) {

			//}
			beam.updatePosition(Vector2.Add(position, weaponOrigin), rotation);
			beamOn = true;
			
		}

		protected override Projectile getProjectile() {
			throw new System.NotImplementedException();
		}


		public override void update(GameTime gameTime, List<Tangible> targets) {

			position += velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
			base.update(position);
			beam.updatePosition(Vector2.Add(position, weaponOrigin), rotation);

			checkWorldEdge();



			beam.update(gameTime, targets);

			checkForCollision(targets, gameTime);
		}

		public override void draw(SpriteBatch sBatch) {

			base.draw(sBatch);

			if (beamOn)
				beam.draw(sBatch);
		}


		public override void altFire(GameTime gameTime) {

		}

	}
}
