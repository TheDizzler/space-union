using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using SpaceUnion.Tools;

namespace SpaceUnion {
	/// <summary>
	/// Hitboxes will contain an array of rectangles
	/// </summary>
	public class HitBox {

		private int width;
		private int height;
		private Rectangle rectHitBox;
		/// <summary>
		/// Hit box coordinates
		/// </summary>
		private Vector2 position;



		public HitBox(float x, float y, int w, int h) {
			position.X = x - w / 2;
			position.Y = y - h / 2;
			width = w;
			height = h;

			//rectHitBox = new Rectangle((int) position.X, (int) position.Y, width, height);

		}

		public Rectangle getArray() {
			return rectHitBox = new Rectangle((int) position.X - width / 2, (int) position.Y - height / 2, width, height);
		}

		public void updatePosition(Vector2 newPosition) {
			position = newPosition;
			//rectHitBox = new Rectangle((int) position.X, (int) position.Y, width, height);
		}

	}
}
