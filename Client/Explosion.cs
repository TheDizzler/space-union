using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion {

	class Explosion : Sprite {

		


		public Explosion(Texture2D tex, Vector2 pos, String explosiontype)
			: base(tex, pos) {

			// the size of each tile
			setSize(16, 16); // must be explicitly set for tile sheets
			
			AnimationClass anima = new AnimationClass();
			addAnimation("Fireball 1", 0, 4, anima.copy());
			addAnimation("Fireball 2", 1, 4, anima.copy());
			addAnimation("Fireball 3", 2, 4, anima.copy());

			// the initial animation
			animation = explosiontype; // !Good idea to set this! 

			frameLength = .25f;
		}

		public Explosion(Texture2D explosions, Vector2 location)
			: base(explosions, location) {
			

		}


		public void update(GameTime gameTime) {


			frameTimeElapsed += (float) gameTime.ElapsedGameTime.TotalSeconds;

			if (frameTimeElapsed >= frameLength) {
				frameTimeElapsed = 0;
				if (frameIndex >= animations[animation].frameCount - 1)
					frameIndex = 0;
				else
					frameIndex++;
			}


		}


		override public void draw(SpriteBatch batch) {

			base.draw(batch);
		}



	}
}
