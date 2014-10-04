using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceUnion.Animations {

	class BigExplosion : Explosion {

		public static String BigFireball1 = "Big Fireball 1";

		public BigExplosion(Texture2D explosions, Vector2 location, float scaleAnimation = 1) : base(explosions, location, scaleAnimation) {

			initialize(scaleAnimation, 32);

			//Random gen = new Random();
			//switch (gen.Next(animations.Count)) {
			//	case 0:
					animation = BigExplosion.BigFireball1;
					//break;
				//case 1:
				//	animation = Explosion.Fireball2;
				//	break;
				//case 2:
				//	animation = Explosion.Fireball3;
				//	break;
			//}
		}


		protected new void initialize(float scaleAnimation = 1.0f, int size = 16) {

			// the size of each tile
			setSize(size, size); // must be explicitly set for tile sheets

			AnimationClass anima = new AnimationClass();
			anima.scale = scaleAnimation;
			addAnimation(BigFireball1, 0, 8, anima.copy());



			frameLength = .07f;
		}



	}
}
