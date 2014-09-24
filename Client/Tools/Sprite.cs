using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceUnion {

	/// <summary>
	/// The base class for all objects that have visual representation
	/// </summary>
	public abstract class Sprite {

		protected Texture2D texture;
		/// <summary>
		/// Sprite dimensions
		/// </summary>
		public int height, width;
		/// <summary>
		/// Game world x,y co-ordinates
		/// </summary>
		public Vector2 position;
		/// <summary>
		/// The center point of the sprite
		/// </summary>
		protected Vector2 origin;
		public float rotation;

		/// <summary>
		/// Animation related variables
		/// </summary>
		public Dictionary<string, AnimationClass> animations
			= new Dictionary<string, AnimationClass>();
		protected int frameIndex = 0;
		public string animation;

		protected float timeElapsed;
		protected float timeToUpdate = .2f;
		public int FramesPerSecond {
			set { timeToUpdate = (1f / value); }
		}
		// End of animations

		

		
		

		/// <summary>
		/// Constructor. May want to remove position as a required param.
		/// </summary>
		/// <param name="tex"></param>
		/// <param name="pos"></param>
		public Sprite(Texture2D tex, Vector2 pos) {
			texture = tex;
			position = pos;
			

			origin = new Vector2(texture.Width / 2, texture.Height / 2);
		}


		/// <summary>
		/// Loads sprite animation.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="row"></param>
		/// <param name="frameCount"></param>
		/// <param name="anima"></param>
		public virtual void addAnimation(string name, int row, int frameCount,
			int frameSize, AnimationClass anima) {

			Rectangle[] recs = new Rectangle[frameCount];

			for (int i = 0; i < frameCount; i++)
				recs[i] = new Rectangle(i * width,
					(row - 1) * height, width, height);

			anima.frameCount = frameCount;
			anima.frames = recs;
			this.animations.Add(name, anima);
		}

		/// <summary>
		/// Draw the sprite to the screen
		/// </summary>
		/// <param name="sBatch"></param>
		public virtual void draw(SpriteBatch sBatch) {
			sBatch.Draw(texture, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
		}


		/// <summary>
		/// Draw to toolbar
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="portraitPosition"></param>
		public virtual void draw(SpriteBatch spriteBatch, Vector2 portraitPosition) { }


		/// <summary>
		/// Checks if co-ordinates are within sprite bounds.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool contains(int x, int y) {

			if (position.X < x && position.X + width > x &&
				position.Y < y && position.Y + height > y) {

				return true;
			}
			return false;
		}


		/// <summary>
		/// Convenience method to retrieve topleft corner of sprite.
		/// </summary>
		/// <returns></returns>
		public Vector2 getTopLeft() {
			return new Vector2(position.X - width / 2,
				position.Y - height / 2);
		}
	}
}
