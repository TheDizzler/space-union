using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceUnion.Ships;


namespace SpaceUnion.Weapons {
	/// <summary>
	/// An interface that all weapons should implement.
	/// @Written by Tristan
	/// </summary>
	interface WeaponSystem {

		/// <summary>
		/// The firerer of the weapon
		/// </summary>
		Ship owner { get; set; }

		/// <summary>
		/// The amount of damage the projectile can inflict
		/// </summary>
		int weaponDamage {get; set;}


		void doDamage(Tangible target, GameTime gameTime);
	}
}
