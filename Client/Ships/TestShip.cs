﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Weapons.Projectiles;


namespace SpaceUnionXNA.Ships {
	class TestShip : Ship {

		public TestShip(Game1 game1)
			: base(assets.shuttle, assets.missile, game1) {



		}


		protected override void altFire(GameTime gameTime) {
			
		}



		protected override void additionalUpdate(GameTime gameTime, QuadTree quadTree) {

		}

		protected override void additionalDraw(SpriteBatch sBatch) {

		}

		public override void drawMiniMap(SpriteBatch batch) {
			throw new NotImplementedException();
		}

		protected override void additionalFire(GameTime gameTime) {

		}
	}
}
