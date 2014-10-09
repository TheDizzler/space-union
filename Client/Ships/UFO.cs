using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;
using SpaceUnion.Weapons;


namespace SpaceUnion.Ships {
	class UFO : Ship {

		Shield shield;


		public UFO(Vector2 pos, Game1 game)
			: base(assets.ufo, assets.laser, pos, game) {

			// Set the laser to fire every quarter second
			mainFireDelay = TimeSpan.FromSeconds(.5f);
			altFireDelay = TimeSpan.FromSeconds(1f);
			shield = new Shield(assets.shield, pos);


		}


		public override void update(GameTime gameTime, List<Tangible> targets) {

			base.update(gameTime, targets);

			shield.update(gameTime, position);
			base.update(gameTime, targets);
		}

		public override void draw(SpriteBatch batch) {

			base.draw(batch);

			if (shield.on)
				shield.draw(batch);
		}

		public override void altFire(GameTime gameTime) {

			if (gameTime.TotalGameTime - previousAltFireTime > altFireDelay) {

				previousAltFireTime = gameTime.TotalGameTime;
				shield.on = !shield.on;
			}
		}

	}
}
