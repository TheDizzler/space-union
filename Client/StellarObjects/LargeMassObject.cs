using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;


namespace SpaceUnionXNA.StellarObjects {
	/// <summary>
	/// An object with enough mass to create gravity effects
	/// </summary>
	public abstract class LargeMassObject : Tangible {

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
		protected LargeMassObject(Texture2D tex, Vector2 pos, float mass, float range, Game1 game)
			: base(tex, pos, game) {

			this.mass = mass * 50000;
			this.range = range;

			maxHealth = 10000;
			currentHealth = maxHealth;
		}


		public void update(GameTime gameTime, QuadTree quadTree, List<Tangible> targets) {

			pull(gameTime, targets); // this could benefit from the quadtree but would require some tweaking

			checkForCollision(quadTree, gameTime);
			velocity = Vector2.Zero;
		}

		/// <summary>
		/// Apply gravitational force to objects within range.
		/// Called from update.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="tangibles">List of objects subject to gravity wells</param>
		private void pull(GameTime gameTime, List<Tangible> tangibles) {

			foreach (Tangible tangible in tangibles) {
				float distance = (tangible.Position - this.Position).Length();
				if (distance < range) {
					float pullForce = mass / (distance * distance);
					// angle in radians that object is off x-axis
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
