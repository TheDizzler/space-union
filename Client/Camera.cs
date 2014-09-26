using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceUnion {
	class Camera {

		public float zoom;
		/// <summary>
		/// Camera View Matrix
		/// </summary>
		public Matrix transform;
		/// <summary>
		/// Inverse of View Matrix, used to get objects screen coordinates
		/// form its object coordinates
		/// </summary>
		public Matrix inverseTransform;
		protected Vector2 position;
		protected float rotation;
		Viewport viewport;
		MouseState mouseState;
		KeyboardState keyState;
		int scroll;

		
		public Camera(Viewport vport) {

			zoom = 1.0f;
			scroll = 1;
			rotation = 0.0f;
			position = Vector2.Zero;
			viewport = vport;
		}

		/// <summary>
		/// Update camera view.
		/// </summary>
		public void update(MouseState mstat) {

			mouseState = mstat;
			input();


		}


		private void input() {

			

		}
	}
}
