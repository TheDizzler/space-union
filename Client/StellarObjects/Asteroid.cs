using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion.StellarObjects {

	/// <summary>
	/// 
	/// </summary>
	class Asteroid : Tangible, Tactile { // Tactile isn't fully implemented. It is just being used as a test case and example.

		//private List<HitBox> boxList = new List<HitBox>();

		public List<HitBox> hitBoxes { get; set; }
		public int health { get; set; }

		public Vector2 velocity;

		public Asteroid(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			hitBoxes = new List<HitBox> {createHitBox(pos.X, pos.Y, width, height)};
			Random r = new Random();
			double direction = r.NextDouble() * 2 * Math.PI; // angle of velocity
			int speed = r.Next(20);
			velocity = new Vector2((float) (Math.Sin(direction)* speed), (float) (-Math.Cos(direction)*speed));
		}

		public void update(GameTime gameTime, List<Tangible> targets) {

			// move in a straight line
			position += velocity;
		}


		

		public HitBox createHitBox(float x, float y, int w, int h) {

			return new HitBox(x, y, width, height);
		}

		public void updateHitBox(Vector2 amountMoved) {
			foreach (HitBox hitBox in hitBoxes) {


			}
		}

		public void destroy() {
			

		}

	}
}
