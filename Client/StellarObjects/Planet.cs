﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion.StellarObjects {

	class Planet : Sprite {

		/// <summary>
		/// How much gravitational 'power' the planet has
		/// </summary>
		float mass = 5000;

		/// <summary>
		/// Range at which gravitational effects are concidered negligable
		/// </summary>
		float range = 500;


		public double angle;


		public Planet(Texture2D tex, Vector2 pos)
			: base(tex, pos) {


		}


		public void update(GameTime gameTime, Ship ship) {

			pull(gameTime, ship);
			angle = Math.Atan2(this.CenterPosition.Y - ship.CenterPosition.Y, this.CenterPosition.X - ship.CenterPosition.X);
		}

		/// <summary>
		/// Call from update.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="ship"></param>
		private void pull(GameTime gameTime, Ship ship) {

			float distance = (ship.CenterPosition - this.CenterPosition).Length();

			if (distance < range) {
				float pullForce = mass / (distance * distance);
				// angle in radians 
				double angle = Math.Atan2(this.CenterPosition.Y - ship.CenterPosition.Y, this.CenterPosition.X - ship.CenterPosition.X);
				// Find the vector to apply to the ships velocity
				//Vector2 pullVector = new Vector2((float) Math.Sin(angle), (float) Math.Cos(angle));
				//pullVector *=mass / (distance * distance);

				//Vector2 pullVector = Vector2.Zero;
				//if (this.CenterPosition.Y - ship.CenterPosition.Y > 0) { // if this is below ship
				//	pullVector.Y = (float) Math.Cos(angle) * pullForce;
				//} else {

				//}

				Vector2 pullVector = new Vector2(
					(float) Math.Cos(angle) * pullForce,
					(float) -Math.Sin(angle) * pullForce);
				Vector2.Add(ref ship.velocity, ref pullVector, out ship.velocity);
			}

		}

	}
}
