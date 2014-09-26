using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceUnion {
	class Camera {

		private const float zoomUpperLimit = 2.5f;
		private const float zoomLowerLimit = .5f;
		private const float zoomIncrement = .1f;
		private float previousScroll = 0f;

		private float zoomRation;

		/// <summary>
		/// Camera View Matrix
		/// </summary>
		public Matrix transform;
		/// <summary>
		/// Inverse of View Matrix, used to get objects screen coordinates
		/// form its object coordinates
		/// </summary>
		public Matrix inverseTransform;
		private Vector2 pos;
		public float rotation;
		public Viewport viewport;

		private int worldWidth;
		private int worldHeight;




		public Camera(Viewport vport, int wrldWidth, int wrldHeight, float initZoom) {

			viewport = vport;

			worldWidth = wrldWidth;
			worldHeight = wrldHeight;
			zoomRation = initZoom;
			rotation = 0.0f;
			pos = Vector2.Zero;
		}


		public float Zoom {
			get { return zoomRation; }
			set {
				zoomRation = value;
				if (zoomRation < zoomLowerLimit)
					zoomRation = zoomLowerLimit;
				if (zoomRation > zoomUpperLimit)
					zoomRation = zoomUpperLimit;
			}
		}


		public void Move(Vector2 amount) {

			pos += amount;
		}


		public Vector2 Position {
			get { return pos; }
			set {
				float leftBarrier = (float) viewport.Width * .5f / zoomRation;
				float rightBarrier = worldWidth - (float) viewport.Width * .5f / zoomRation;
				float topBarrier = worldWidth - (float) viewport.Height * .5f / zoomRation;
				float bottomBarrier = (float) viewport.Height * .5f / zoomRation;
				pos.X = value.X - viewport.Width / 2;
				pos.Y = value.Y - viewport.Height / 2;
				if (pos.X < leftBarrier)
					pos.X = leftBarrier;
				if (pos.X > rightBarrier)
					pos.X = rightBarrier;
				if (pos.Y > topBarrier)
					pos.Y = topBarrier;
				if (pos.Y < bottomBarrier)
					pos.Y = bottomBarrier;
			}
		}


		public void update(GameTime gameTime, float rotation, Vector2 position, float zoom) {

			transform = Matrix.CreateTranslation(position.X, position.Y, 0) *
				Matrix.CreateTranslation(viewport.Width, viewport.Height, 0);

		}


		public Matrix getTransformation() {
			transform =
			   Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) *
			   Matrix.CreateRotationZ(rotation) *
			   Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
			   Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));

			return transform;
		}


		/// <summary>
		/// Update camera view.
		/// </summary>
		public void update(MouseState mstat, Vector2 position) {

			//mouseState = mstat;
			//input(mstat);

			//zoom = MathHelper.Clamp(zoom, 0.0f, 10.0f);

			//transform =
			//	Matrix.CreateScale(zoom, zoom, 0) *
			//	Matrix.CreateTranslation(position.X, position.Y, 0);

			//inverseTransform = Matrix.Invert(transform);
		}


		//private void input(MouseState mstat) {

		//	if (mstat.ScrollWheelValue > scroll) {

		//		zoom += 0.1f;
		//		scroll = mstat.ScrollWheelValue;
		//	} else if (mstat.ScrollWheelValue < scroll) {
		//		zoom -= 0.1f;
		//		scroll = mstat.ScrollWheelValue;
		//	}
		//}

		internal void zoom(int scrollWheelVal) {
			if (scrollWheelVal > previousScroll)
				Zoom += zoomIncrement;
			else if (scrollWheelVal < previousScroll)
				Zoom -= zoomIncrement;

			previousScroll = scrollWheelVal;
		}
	}
}
