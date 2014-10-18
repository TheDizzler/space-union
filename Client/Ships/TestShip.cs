using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;
using SpaceUnion.Weapons;
using SpaceUnion.Weapons.Projectiles;


namespace SpaceUnion.Ships {
	class TestShip : Ship {

		public TestShip(Game1 game1)
			: base(assets.shuttle, assets.missile, game1) {



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
