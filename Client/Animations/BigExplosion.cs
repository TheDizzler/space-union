using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceUnion.Animations {

	class BigExplosion : Explosion {


		public BigExplosion(Texture2D explosions, Vector2 location, float scaleAnimation = 1)
			: base(explosions, location) {

			initialize(scaleAnimation, 32);

			//Random gen = new Random();
			//switch (gen.Next(animations.Count)) {
			//	case 0:
			animation = Explosion.BigFireball1;
			//break;
			//case 1:
			//	animation = Explosion.Fireball2;
			//	break;
			//case 2:
			//	animation = Explosion.Fireball3;
			//	break;
			//}
		}


		protected override void initialize(float scaleAnimation = 1.0f, int size = 16) {

			// the size of each tile
			setSize(size, size); // must be explicitly set for tile sheets

			AnimationClass anima = new AnimationClass();
			anima.scale = scaleAnimation;
			addAnimation(Explosion.BigFireball1, 0, 8, anima.copy());
			addAnimation(Explosion.BigFireball2, 1, 8, anima.copy());


			frameLength = .05f;
		}


		public override void update(GameTime gameTime) {


			frameTimeElapsed += (float) gameTime.ElapsedGameTime.TotalSeconds;

			if (frameTimeElapsed >= frameLength) {
				frameTimeElapsed = 0;
				if (frameIndex < animations[animation].frameCount - 1)
					++frameIndex;
				else {
					if (animation.Equals(BigFireball1)) {
						animation = BigFireball2;
						frameIndex = 0;
					} else
						isExhausted = true;

				}
			}
		}


	}
}
