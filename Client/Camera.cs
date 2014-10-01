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

		private float zoomRatio;

		/// <summary>
		/// Camera View Matrix
		/// </summary>
		private Matrix transform;
		/// <summary>
		/// Inverse of View Matrix, used to get objects screen coordinates
		/// form its object coordinates
		/// </summary>
		public Matrix inverseTransform;
		private Vector2 cameraPosition;
		private Vector3 origin;
		public float rotation;
		public Viewport viewport;

		private int worldWidth;
		private int worldHeight;




		public Camera(Viewport vport, int wrldWidth, int wrldHeight, float initZoom) {

			viewport = vport;

			worldWidth = wrldWidth;
			worldHeight = wrldHeight;
			zoomRatio = initZoom;
			rotation = 0.0f;
			cameraPosition = Vector2.Zero;
			origin = new Vector3(viewport.Width / 2, viewport.Height / 2, 0);

		}


		public float zoom {
			get { return zoomRatio; }
			set {
				zoomRatio = value;
				if (zoomRatio < zoomLowerLimit)
					zoomRatio = zoomLowerLimit;
				if (zoomRatio > zoomUpperLimit)
					zoomRatio = zoomUpperLimit;
			}
		}


		public Vector2 Position {
			get { return cameraPosition; }
			set {
				//float leftBarrier = (float) viewport.Width * .5f / zoom;
				//float rightBarrier = worldWidth - (float) viewport.Width * .5f / zoom;
				//float topBarrier = worldHeight - (float) viewport.Height * .5f / zoom;
				//float bottomBarrier = (float) viewport.Height * .5f / zoom;

				float leftBarrier = 0;
				float rightBarrier = worldWidth - viewport.Width;
				float topBarrier = 0;
				float bottomBarrier = worldHeight - viewport.Height;

				cameraPosition.X = value.X - viewport.Width / 2;
				cameraPosition.Y = value.Y - viewport.Height / 2;

				if (cameraPosition.X < leftBarrier)
					cameraPosition.X = leftBarrier;
				if (cameraPosition.X > rightBarrier)
					cameraPosition.X = rightBarrier;
				if (cameraPosition.Y < topBarrier)
					cameraPosition.Y = topBarrier;
				if (cameraPosition.Y > bottomBarrier)
					cameraPosition.Y = bottomBarrier;
			}
		}

		/// <summary>
		/// Update camera view.
		/// </summary>
		public void update(GameTime gameTime) {


			transform =
				Matrix.CreateTranslation(new Vector3(-cameraPosition, 0)) *
				Matrix.CreateTranslation(-origin) *
				//Matrix.CreateRotationZ(rotation) * // Might be fun to play with this...
				Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
			Matrix.CreateTranslation(origin);
		}


		public Matrix getTransformation() {

			return transform;
		}


		public void setZoom(int scrollWheelVal) {

			if (scrollWheelVal > previousScroll)
				zoom += zoomIncrement;
			else if (scrollWheelVal < previousScroll)
				zoom -= zoomIncrement;

			previousScroll = scrollWheelVal;
		}
	}
}
