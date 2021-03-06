﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Tools;


namespace SpaceUnionXNA.Weapons.Systems {
	/// <summary>
	/// An interface that all weapons should implement.
	/// @Written by Tristan
	/// </summary>
	public interface WeaponSystem {

		/// <summary>
		/// The firererer of the weapon
		/// </summary>
		Ship owner { get; set; }

		/// <summary>
		/// Fire the weapon.
		/// </summary>
		/// <param name="startPoint"></param>
		void fire(Vector2 startPoint);

		void update(GameTime gameTime, QuadTree quadTree);

		void draw(SpriteBatch sBatch);

		void updatePosition(Vector2 startPoint, float rot);

	}
}
