using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion {
	class Background {

		/// <summary>
		/// The Dimensions of the level
		/// </summary>
		private int worldWidth, worldHeight;
		/// <summary>
		/// The sprites that create the 3d background effect.
		/// </summary>
		private Texture2D layer0, layer1, layer2, layer3;


		public Background(int worldW, int worldH, Texture2D layer0, Texture2D layer1, Texture2D layer2, Texture2D layer3) {

			worldWidth = worldW;
			worldHeight = worldH;

			this.layer0 = layer0;
			this.layer1 = layer1;
			this.layer2 = layer2;
			this.layer3 = layer3;
		}


		public void draw(SpriteBatch spriteBatch, Camera camera) {

			/* Parallax Scrolling BG */
			spriteBatch.Draw(layer0,
				new Rectangle((int) (camera.Position.X * .9), (int) (camera.Position.Y * .9), worldWidth / 2, worldHeight / 2),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(layer1,
				new Rectangle((int) (camera.Position.X * 0.7), (int) (camera.Position.Y * 0.7), worldWidth / 4, worldHeight / 4),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(layer2,
				new Rectangle((int) (camera.Position.X * 0.6), (int) (camera.Position.Y * 0.6), worldWidth / 5, worldHeight / 5),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
			spriteBatch.Draw(layer3,
				new Rectangle((int) (camera.Position.X * 0.4), (int) (camera.Position.Y * 0.4), worldWidth / 6, worldHeight / 6),
				null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
		}
	}
}
