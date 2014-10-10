using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion.Animations {

	public abstract class Explosion : Sprite {

		public bool isExhausted = false;

		public static String Fireball1 = "Fireball 1";
		public static String Fireball2 = "Fireball 2";
		public static String Fireball3 = "Fireball 3";
		public static String BigFireball1 = "Big Fireball 1";
		public static String BigFireball2 = "Big Fireball death";


		public Explosion(Texture2D tex, Vector2 pos) : base(tex, pos) {
		
		}

		protected abstract void initialize(float scaleAnimation = 1.0f, int size = 16);


		public abstract void update(GameTime gameTime);

	}
}
