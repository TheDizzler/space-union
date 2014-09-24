		using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceUnion {
	/// <summary>
	/// Used sprite animations
	/// </summary>
	class AnimationClass {

		public Rectangle[] frames;
		public Color color = Color.White;
		public Vector2 center;
		public float rotation = 0f;
		public float scale = 1f;
		public SpriteEffects spriteEffect;
		public bool isLooping = true;
		public int frameCount;

		public AnimationClass copy() {

			AnimationClass anima = new AnimationClass();
			anima.frames = frames;
			anima.color = color;
			anima.center = center;
			anima.rotation = rotation;
			anima.scale = scale;
			anima.spriteEffect = spriteEffect;
			anima.isLooping = isLooping;
			anima.frameCount = frameCount;
			return anima;
		}
	}
}

	}
}
