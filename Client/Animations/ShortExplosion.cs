using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;


namespace SpaceUnionXNA.Animations {

	public class ShortExplosion : Explosion {

		

		
		private bool isReverse = false;


		/// <summary>
		/// Create a specific explosion
		/// </summary>
		/// <param name="tex"></param>
		/// <param name="pos"></param>
		/// <param name="explosiontype">Explosion.Fireball1, Explosion.Fireball2, Explosion.Fireball3</param>
		public ShortExplosion(Texture2D tex, Vector2 pos, String explosiontype)
			: base(tex, pos) {

			initialize();

			// the initial animation
			animation = explosiontype; // !Good idea to set this! 
		}

		/// <summary>
		/// Creates a random explosion.
		/// </summary>
		/// <param name="location"></param>
		/// <param name="scaleAnimation"></param>
		public ShortExplosion(Vector2 location, float scaleAnimation = 1.0f)
			: base(assets.explosions, location) {

				initialize(scaleAnimation);

			Random gen = new Random();
			switch (gen.Next(animations.Count)) {
				case 0:
					animation = Explosion.Fireball1;
					break;
				case 1:
					animation = Explosion.Fireball2;
					break;
				case 2:
					animation = Explosion.Fireball3;
					break;
			}

		}


		protected override void initialize(float scaleAnimation = 1.0f, int size = 16) {

			// the size of each tile
			setSize(size, size); // must be explicitly set for tile sheets

			AnimationClass anima = new AnimationClass();
			anima.scale = scaleAnimation;
			addAnimation(Fireball1, 0, 4, anima.copy());
			addAnimation(Fireball2, 1, 4, anima.copy());
			addAnimation(Fireball3, 2, 4, anima.copy());



			frameLength = .1f;
		}


		public override void update(GameTime gameTime) {


			frameTimeElapsed += (float) gameTime.ElapsedGameTime.TotalSeconds;

			if (frameTimeElapsed >= frameLength) {
				frameTimeElapsed = 0;
				if (!isReverse) {
					if (frameIndex < animations[animation].frameCount - 1)
						++frameIndex;
					else
						isReverse = true;
				} else
					if (--frameIndex < 0)
						isExhausted = true; // should get cleaned up here

			}
		}


		
	}
}
