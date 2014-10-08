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
		private bool shieldOn = false;


		public UFO(Vector2 pos, Game1 game)
			: base(assets.ufo, assets.laser, pos, game) {

			shield = new Shield(assets.shield, pos);
		}


		public override void update(GameTime gameTime, List<Tangible> targets) {

			shield.update(gameTime, position);
			if (shieldOn)
				base.update(gameTime, targets);

		}

		public override void draw(SpriteBatch batch) {

			base.draw(batch);
			if (shieldOn)
				shield.draw(batch);
		}

	}
}
