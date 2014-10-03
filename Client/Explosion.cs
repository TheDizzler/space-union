using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion {
	class Explosion : Sprite {

		//private double startTime;


		public Explosion(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			// the size of each tile
			width = height = 16; // must be explicitly set for tile sheets

			AnimationClass anima = new AnimationClass();
			addAnimation("Fireball 1", 0, 4, 16, anima.copy());
			addAnimation("Fireball 2", 1, 4, 16, anima.copy());
			addAnimation("Fireball 3", 2, 4, 16, anima.copy());

			animation = "Fireball 1";
		}


		//public void explode(GameTime gameTime) {

		//	startTime = gameTime.TotalGameTime.TotalMilliseconds;
		//}


		public void update(GameTime gameTime) {

			//frameTimeElapsed = (float) (gameTime.TotalGameTime.TotalMilliseconds - startTime);

			frameTimeElapsed += (float) gameTime.ElapsedGameTime.TotalSeconds;

			if (frameTimeElapsed >= frameLength)
				frameIndex++;

		}


		public void draw(SpriteBatch batch) {




		}

	}
}
