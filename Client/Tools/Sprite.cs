using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceUnion.Tools {

	/// <summary>
	/// The base class for all objects that have visual representation
	/// </summary>
	public abstract class Sprite {


		public int alphaValue;

		protected Texture2D texture;
		/// <summary>
		/// Sprite dimensions
		/// </summary>
		public int height, width;
		/// <summary>
		/// Top-left corner of sprite Game World (x, y) co-ordinates
		/// </summary>
		protected Vector2 position;
		/// <summary>
		/// The center point of the sprite
		/// </summary>
		public Vector2 origin;
		/// <summary>
		/// Angle in radians of sprite orientation
		/// </summary>
		protected float rotation;

		public double getRotation() {
			return rotation;
		}

		protected float scale = 1.0f;

		/* Animation related variables */
		public Dictionary<string, AnimationClass> animations
			= new Dictionary<string, AnimationClass>();
		protected int frameIndex = 0;
		/// <summary>
		/// The animation cycle currently showing
		/// </summary>
		public string animation;

		/// <summary>
		/// Length of time (in seconds) current frame has been on screen.
		/// </summary>
		protected float frameTimeElapsed = 0;
		/// <summary>
		/// Length of time (in seconds) frame stays on screen.
		/// </summary>
		protected float frameLength;
		public int FramesPerSecond {
			set { frameLength = (1f / value); }
		}
		/* End of animations */

		/// <summary>
		/// Get sprites center position in game world coordinates
		/// </summary>
		public Vector2 Position { get { return position; } }

		/// <summary>
		/// Get sprite's center X position
		/// </summary>
		/// <returns></returns>
		public float getX() {

			return position.X;
		}
		/// <summary>
		/// Get sprite's center Y position
		/// </summary>
		/// <returns></returns>
		public float getY() {

			return position.Y;
		}


		/// <summary>
		/// Constructor. May want to remove position as a required param.
		/// </summary>
		/// <param name="tex"></param>
		/// <param name="pos"></param>
		protected Sprite(Texture2D tex, Vector2 pos) {
			texture = tex;
			position = pos;

			setSize(texture.Width, texture.Height);

			animation = null;
		}

		/// <summary>
		/// Sets width, height and origin of sprite
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		protected void setSize(int wdth, int hght) {
			width = wdth;
			height = hght;

			origin = new Vector2(width / 2, height / 2);
		}


		/// <summary>
		/// Loads sprite animation.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="row"></param>
		/// <param name="frameCount"></param>
		/// <param name="anima"></param>
		public virtual void addAnimation(string name, int row, int frameCount,
			AnimationClass anima) {

			Rectangle[] recs = new Rectangle[frameCount];

			for (int i = 0; i < frameCount; i++)
				recs[i] = new Rectangle(i + i * width,
					(row) * height, width, height);

			anima.frameCount = frameCount;
			anima.frames = recs;
			animations.Add(name, anima);
		}

		/// <summary>
		/// Draw the sprite to the screen
		/// </summary>
		/// <param name="sBatch"></param>
		public virtual void draw(SpriteBatch sBatch) {

			if (animation == null)
				sBatch.Draw(texture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
			else
				animate(sBatch);
		}


		private void animate(SpriteBatch sBatch) {

			sBatch.Draw(texture, position,
				animations[animation].frames[frameIndex],
				animations[animation].color,
				animations[animation].rotation,
				origin,
				animations[animation].scale,
				animations[animation].spriteEffect,
				0f);

		}


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
	}
}
