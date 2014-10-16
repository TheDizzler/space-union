using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace SpaceUnion.Weapons {
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
