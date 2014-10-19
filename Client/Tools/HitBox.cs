using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace SpaceUnion.Tools {
	/// <summary>
	/// Hitboxes will contain an array of rectangles
	/// </summary>
	public class HitBox {

		public int width;
		public int height;
		private Rectangle rectHitBox;
		/// <summary>
		/// Hit box coordinates
		/// </summary>
		public Vector2 position;

		/// <summary>
		/// Dictionary of edges for raycasting.
		/// </summary>
		public Dictionary<String, Vector2[]> edges = new Dictionary<String, Vector2[]>();

		public float rotation;


		public HitBox(float x, float y, int w, int h) {
			position.X = x;
			position.Y = y;
			width = w;
			height = h;

			Vector2 topLeft = new Vector2(x, y);
			Vector2 bottomLeft = new Vector2(x, y + height);
			Vector2 topRight = new Vector2(x + width, y);
			Vector2 bottomRight = new Vector2(x + width, y + height);


			edges.Add("left", new Vector2[2] { topLeft, bottomLeft});
			edges.Add("bottom", new Vector2[2] { bottomLeft, bottomRight });
			edges.Add("right", new Vector2[2] { bottomRight, topRight });
			edges.Add("top", new Vector2[2] { topRight, topLeft });


			//rectHitBox = new Rectangle((int) position.X, (int) position.Y, width, height);

		}

		public Rectangle getArray() {
			return rectHitBox = new Rectangle((int) position.X - width / 2, (int) position.Y - height / 2, width, height);
		}

		public void updatePosition(Vector2 newPosition, float rot) {
			position = newPosition;
			rotation = rot;
			//rectHitBox = new Rectangle((int) position.X, (int) position.Y, width, height);
		}

	}
}
