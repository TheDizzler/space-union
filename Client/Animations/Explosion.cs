using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion.Animations {

	public class Explosion : Sprite {

		public static String Fireball1 = "Fireball 1";
		public static String Fireball2 = "Fireball 2";
		public static String Fireball3 = "Fireball 3";

		public bool isDead = false;
		private bool isReverse = false;


		/// <summary>
		/// Create a specific explosion
		/// </summary>
		/// <param name="tex"></param>
		/// <param name="pos"></param>
		/// <param name="explosiontype">Explosion.Fireball1, Explosion.Fireball2, Explosion.Fireball3</param>
		public Explosion(Texture2D tex, Vector2 pos, String explosiontype)
			: base(tex, pos) {

			initialize();

			// the initial animation
			animation = explosiontype; // !Good idea to set this! 
		}

		/// <summary>
		/// Creates a random explosion.
		/// </summary>
		/// <param name="explosions"></param>
		/// <param name="pos"></param>
		/// <param name="location"></param>
		/// <param name="scaleAnimation"></param>
		public Explosion(Texture2D explosions, Vector2 location, float scaleAnimation = 1.0f)
			: base(explosions, location) {

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


		private void initialize(float scaleAnimation = 1.0f, int size = 16) {

			// the size of each tile
			setSize(size, size); // must be explicitly set for tile sheets

			AnimationClass anima = new AnimationClass();
			anima.scale = scaleAnimation;
			addAnimation(Fireball1, 0, 4, anima.copy());
			addAnimation(Fireball2, 1, 4, anima.copy());
			addAnimation(Fireball3, 2, 4, anima.copy());



			frameLength = .1f;
		}


		public void update(GameTime gameTime) {


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
						isDead = true; // should get cleaned up here

			}
		}


		override public void draw(SpriteBatch batch) {

			base.draw(batch);
		}
	}
}
