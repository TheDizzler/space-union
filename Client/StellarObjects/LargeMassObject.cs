using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Weapons;


namespace SpaceUnion.StellarObjects {
	/// <summary>
	/// An object with enough mass to create gravity effects
	/// </summary>
	public abstract class LargeMassObject : Tangible {

		/// <summary>
		/// How much gravitational 'power' the planet has
		/// </summary>
		float mass;

		/// <summary>
		/// Range (in pixels) after which gravitational effects are concidered negligable
		/// </summary>
		float range;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tex">planet texture</param>
		/// <param name="pos">Location in game coordinates of planet</param>
		/// <param name="mass">100 is weak, 1000 is very strong</param>
		/// <param name="range">Range (in pixels) that gravitational effects span</param>
		protected LargeMassObject(Texture2D tex, Vector2 pos, float mass, float range)
			: base(tex, pos) {
			this.mass = mass * 1000;
			this.range = range;
		}


		public void update(GameTime gameTime, List<Tangible> tangibles) {

			pull(gameTime, tangibles);

			checkForCollision(tangibles);
		}

		/// <summary>
		/// Apply gravitational force to objects within range.
		/// Call from update.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="tangibles">List of objects subject to gravity wells</param>
		private void pull(GameTime gameTime, List<Tangible> tangibles) {

			foreach (Tangible tangible in tangibles) {
				float distance = (tangible.Position - this.Position).Length();
				if (distance < range) {
					float pullForce = mass / (distance * distance);
					// angle in radians that object is off 
					double angle = Math.Atan2(this.Position.Y - tangible.Position.Y, this.Position.X - tangible.Position.X);
					// Find the vector to apply to the ships velocity
					Vector2 pullVector = new Vector2(
						(float) Math.Cos(angle) * pullForce * (float) gameTime.ElapsedGameTime.TotalSeconds,
						(float) Math.Sin(angle) * pullForce * (float) gameTime.ElapsedGameTime.TotalSeconds);
					Vector2.Add(ref tangible.velocity, ref pullVector, out tangible.velocity);
				}
			}
		}



		/// <summary>
		/// Most impressive....
		/// </summary>
		public override void destroy() {
			throw new NotImplementedException();
		}

	}
}
