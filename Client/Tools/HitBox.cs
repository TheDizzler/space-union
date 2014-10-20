using Microsoft.Xna.Framework;


namespace SpaceUnion.Tools {
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
		public Vector2 position;

		public float rotation;


		public HitBox(float x, float y, int w, int h) {
			position.X = x;
			position.Y = y;
			width = w;
			height = h;

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
