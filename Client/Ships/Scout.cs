using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;
using SpaceUnion.Weapons;
using SpaceUnion.Weapons.Projectiles;
using SpaceUnion.Weapons.Systems;


namespace SpaceUnion.Ships {
	class Scout : Ship {


		bool beamOn = false;
		//private LaserBeam beam;

		public Scout(Game1 game1)
			: base(assets.stunt, assets.laser, game1) {

			maxSpeed = 7;
			accelSpeed = 250.5f;
			turnSpeed = 4.5f;
			maxSpeed = 500;

			weaponOrigin = new Vector2(position.X, position.Y - height / 2);

			mainWeapon = new LaserBeam(Vector2.Add(position, weaponOrigin), this);
		}

		/// <summary>
		/// Main weapon fire method
		/// </summary>
		/// <param name="gameTime"></param>
		public override void fire(GameTime gameTime) {

			// Fire only every interval we set as the fireTime
			//if (!beamOn) {

			//}

			((LaserBeam)mainWeapon).updatePosition(Vector2.Add(position, weaponOrigin), rotation);
			beamOn = true;

		}



		public override void update(GameTime gameTime, QuadTree quadTree) {

			position += velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
			base.update(position);
			((LaserBeam) mainWeapon).updatePosition(Vector2.Add(position, weaponOrigin), rotation);

			checkWorldEdge();


			if (beamOn)
				mainWeapon.update(gameTime, quadTree);

			checkForCollision(quadTree, gameTime);
		}

		public override void draw(SpriteBatch sBatch) {

			base.draw(sBatch);

			if (beamOn)
				mainWeapon.draw(sBatch);
		}

		public override void drawMiniMap(SpriteBatch batch) {
			throw new NotImplementedException();
		}

		public override void altFire(GameTime gameTime) {

		}


		protected override void additionalUpdate(GameTime gameTime, QuadTree quadTree) {

		}

		protected override void additionalDraw(SpriteBatch sBatch) {

		}

		protected override void additionalFire(GameTime gameTime) {

		}

	}
}
