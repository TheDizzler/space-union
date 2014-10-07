using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion.StellarObjects {

	class Asteroid : Tangible, Tactile {

		//private List<HitBox> boxList = new List<HitBox>();

		public List<HitBox> hitBoxes { get; set; }

		public Asteroid(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			hitBoxes = new List<HitBox>();
			hitBoxes.Add(createHitBox(pos.X, pos.Y, width, height));

		}

		public void update(GameTime gameTime, List<Tangible> targets) {

		}




		public HitBox createHitBox(float x, float y, int w, int h) {

			return new HitBox(x, y, width, height);
		}

		public void updateHitBox(Vector2 amountMoved) {
			foreach (HitBox hitBox in hitBoxes) {


			}
		}

	}
}
