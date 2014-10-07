using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace SpaceUnion {
	/// <summary>
	/// An object that has hitboxes must inherit from this interface.
	/// To use it properly, a list of hitboxes must be created
	/// </summary>
	interface Tactile {

		/// <summary>
		/// Container for all hitboxes on this object
		/// </summary>
		List<HitBox> hitBoxes {
			get;
			set;
		}

		/// <summary>
		/// If false, the object will be destroyed and removed from the game.
		/// </summary>
		bool isActive { get; set; }


		int health { get; set; }


		HitBox createHitBox(float x, float y, int w, int h);

		/// <summary>
		/// Call from update after position has been calculated
		/// </summary>
		/// <param name="amountMoved"></param>
		void updateHitBox(Vector2 amountMoved);

		/// <summary>
		/// Called when an tactile object is destroyed
		/// </summary>
		void destroy();
	}
}
