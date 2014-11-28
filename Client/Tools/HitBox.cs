using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceUnionXNA.Tools {
	/// <summary>
	/// This contains the main "rough" hitbox and the smaller, precise hitcircles.
	/// @Written by Konstantin. Editted by Tristan.
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
		/// DEPRECATED
		/// </summary>
		//public Dictionary<String, Vector2[]> edges = new Dictionary<String, Vector2[]>();

		public float rotation;

		/// <summary>
		/// Location of corners of rectangle for convenience.
		/// </summary>
		public Vector2 topLeft, bottomLeft, topRight, bottomRight;

		/// <summary>
		/// List of smaller, more precise hitboxes.
		/// These must be set up on a ship-per-ship basis.
		/// </summary>
		public List<HitCircle> circles = new List<HitCircle>();


		public HitBox(float x, float y, int w, int h) {
			width = w;
			height = h;

			position.X = x - width / 2;
			position.Y = y - height / 2;
			

			topLeft = new Vector2(position.X, position.Y);
			bottomLeft = new Vector2(position.X, position.Y + height);
			topRight = new Vector2(position.X + width, position.Y);
			bottomRight = new Vector2(position.X + width, position.Y + height);


			//edges.Add("left", new Vector2[2] { topLeft, bottomLeft });
			//edges.Add("bottom", new Vector2[2] { bottomLeft, bottomRight });
			//edges.Add("right", new Vector2[2] { bottomRight, topRight });
			//edges.Add("top", new Vector2[2] { topRight, topLeft });
		}

		public Rectangle getArray() {
			return rectHitBox = new Rectangle((int) position.X, (int) position.Y, width, height);
			//return rectHitBox = new Rectangle((int) position.X , (int) position.Y, width, height);
		}

		public void updatePosition(Vector2 newPosition, float rot) {
			position.X = newPosition.X - width / 2;
			position.Y = newPosition.Y - height / 2;

			topLeft = position;
			bottomLeft = new Vector2(position.X, position.Y + height);
			topRight = new Vector2(position.X + width, position.Y);
			bottomRight = new Vector2(position.X + width, position.Y + height);

			foreach (HitCircle circle in circles)
				circle.updatePosition(newPosition);
			//rotation = rot;
			//rectHitBox = new Rectangle((int) position.X, (int) position.Y, width, height);
		}


		public void createHitCircle(Vector2 pos, int radius) {

			circles.Add(new HitCircle(pos, radius));
		}

		/// <summary>
		/// Debugging draw.
		/// </summary>
		internal void draw(SpriteBatch batch, AssetManager assets) {
			// draw rough outer hit box
			batch.Draw(assets.guiRectangle, getArray(), Color.Pink * .5f);

			//draw inner hitcircles
			//foreach (HitCircle circle in circles)
			//	batch.Draw(assets.guiRectangle, circle.getCircle(), Color.Red * .5f);

			//batch.Draw(assets.guiRectangle, hitBox.position, hitBox.getArray(), Color.Pink, hitBox.rotation, hitBox.position, scale, SpriteEffects.None, 0);
		}
	}


}
