using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace SpaceUnionXNA.Tools {
	public class HitCircle {

		/// <summary>
		/// Radius of Hit Circle.
		/// </summary>
		protected int radius;
		/// <summary>
		/// Hit circle center coordinates.
		/// </summary>
		Vector2 position;


		public HitCircle(Vector2 pos, int rad) {

			position = pos;
			radius = rad;
		}


		public void updatePosition(Vector2 pos) {

			position = pos;
		}

		internal Rectangle getCircle() {
			throw new NotImplementedException();
		}
	}
}
