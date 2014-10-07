using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion {

	/// <summary>
	/// An object that has hitboxes must inherit from this class
	/// </summary>
	public abstract class Tangible : Sprite {

		/// <summary>
		/// If false, the object will be removed from the game.
		/// </summary>
		public bool isActive { get; set; }

		protected HitBox hitBox;
		//Return Hitbox for collision detection
		public HitBox getHitBox() {
			return hitBox;
		}

		public int maxHealth = 100;
		public int currentHealth;
		public int getHealth() {
			return currentHealth;
		}
		/// <summary>
		/// Get % health remaining
		/// </summary>
		float HealthPercentage {
			get { return currentHealth / maxHealth; }
		}

		
		protected Tangible(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

				hitBox = new HitBox(pos.X, pos.Y, width, height);
				isActive = true;
		}

		/// <summary>
		/// Update the hitbox
		/// </summary>
		/// <param name="newPosition"></param>
		protected void update(Vector2 newPosition) {

			hitBox.updatePosition(newPosition);

		}


	}
}
