using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;

namespace SpaceUnion {

	/// <summary>
	/// Base abstract ship class.
	/// CURRENTLY NOT ABSTRACT FOR TESTING
	/// </summary>
	class Ship : Sprite {

		protected Vector2 velocity;

		public float maxHealth;
		public float currentHealth;

		/// <summary>
		/// Get % health remaining
		/// </summary>
		public float HealthPercentage {
			get { return currentHealth / maxHealth; }
		}

		public bool alive = true;

		internal float attackDelay;
		protected float attackTimer;


		public Ship(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			velocity = Vector2.Zero;
		}
	}
}
