﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion.StellarObjects {

	class Planet : Tangible {

		/// <summary>
		/// How much gravitational 'power' the planet has
		/// </summary>
		float mass = 200000;

		/// <summary>
		/// Range at which gravitational effects are concidered negligable
		/// </summary>
		float range = 1000;


		public Planet(Texture2D tex, Vector2 pos)
			: base(tex, pos) {


		}


		public Planet(Texture2D tex, Vector2 pos, float mass, float range)
			: base(tex, pos) {

			this.mass = mass;
			this.range = range;
		}

		public void update(GameTime gameTime, List<Ship> ships) {

			pull(gameTime, ships);
		}

		/// <summary>
		/// Apply gravitational force to objects within range.
		/// Call from update.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="ship"></param>
		private void pull(GameTime gameTime, List<Ship> ships) {

			foreach (Ship ship in ships) {
				float distance = (ship.Position - this.Position).Length();
				if (distance < range) {
					float pullForce = mass / (distance * distance);
					// angle in radians that object is off 
					double angle = Math.Atan2(this.Position.Y - ship.Position.Y, this.Position.X - ship.Position.X);
					// Find the vector to apply to the ships velocity
					Vector2 pullVector = new Vector2(
						(float) Math.Cos(angle) * pullForce * (float) gameTime.ElapsedGameTime.TotalSeconds,
						(float) Math.Sin(angle) * pullForce * (float) gameTime.ElapsedGameTime.TotalSeconds);
					Vector2.Add(ref ship.velocity, ref pullVector, out ship.velocity);
				}
			}
		}
	}
}