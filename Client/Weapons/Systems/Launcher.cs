using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Ships;
using SpaceUnion.Tools;
using SpaceUnion.Weapons.Projectiles;


namespace SpaceUnion.Weapons.Systems {
	/// <summary>
	/// A basic Launcher Weapon System class for projectile weapons to fire from.
	/// Equip on a Ship's mainWeapon by using static function CreateLauncher().
	/// @Written by Tristan.
	/// </summary>
	/// <typeparam name="ProjectileType">A Projectile Class</typeparam>
	class Launcher<ProjectileType> : WeaponSystem where ProjectileType : Projectile {

		/// <summary>
		/// Total projectiles in flight.
		/// </summary>
		public List<ProjectileType> projectiles;

		/// <summary>
		/// Total projectiles in launcher.
		/// This recycles the projectiles to avoid repetitive class creation.
		/// </summary>
		public List<ProjectileType>  projectileStore;

		/// <summary>
		/// A hard limit for how many projectiles a ship can have active at once.
		/// </summary>
		private short maxCapacity;


		public Ship owner { get; set; }


		/// <summary>
		/// Creates a launcher for the specified projectile.
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="projectileClass"></param>
		/// <param name="maxCapacity"></param>
		/// <returns>the created launcher</returns>
		public static WeaponSystem CreateLauncher(Ship ship, Func<Vector2, Ship, ProjectileType> projectileClass, short maxCapacity = 8) {

			Launcher<ProjectileType> launcher = new Launcher<ProjectileType>(ship, maxCapacity);
			launcher.projectileStore = fillStore(ship, maxCapacity, projectileClass);

			return launcher;
		}


		private static List<ProjectileType> fillStore(Ship ship, short max, Func<Vector2, Ship, ProjectileType> proj) {

			List < ProjectileType >  temp = new List<ProjectileType>(max);
			for (int i = 0; i < max; ++i)
				temp.Add(proj(new Vector2(i * 2, i * 2), ship));
			return temp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="capacity"></param>
		private Launcher(Ship ship, short capacity) {

			owner = ship;
			maxCapacity = capacity;
			projectiles = new List<ProjectileType>();

		}




		/// <summary>
		/// Fire a projectile from the launcher if there are any left
		/// in the store.
		/// </summary>
		/// <param name="startPoint">Point where projectile is fired from.</param>
		public void fire(Vector2 startPoint) {

			if (projectileStore.Count - 1 >= 0) {
				ProjectileType proj = projectileStore[projectileStore.Count - 1];
				projectileStore.Remove(proj);
				proj.launch(startPoint, (float) owner.getRotation());
				projectiles.Add(proj);
			}
		}

		/// <summary>
		/// @Written by Kyle.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="quadTree"></param>
		public void update(GameTime gameTime, QuadTree quadTree) {
			// Update the Projectiles
			for (int i = projectiles.Count - 1; i >= 0; i--) {
				projectiles[i].update(gameTime, quadTree);

				if (!projectiles[i].isActive) {
					putInStore(projectiles[i]);


					projectiles.RemoveAt(i);
				}
			}
		}

		/// <summary>
		/// Recycle the projectile.
		/// Puts it back in the projectileStore and puts it off the map.
		/// </summary>
		/// <param name="projectile"></param>
		private void putInStore(ProjectileType projectile) {
			projectileStore.Add(projectile);
			projectile.position = new Vector2(25, 25);
		}

		/// <summary>
		/// Passes the spritebatch on to the projectiles. The launcher
		/// itself shouldn't draw itself.
		/// </summary>
		/// <param name="sBatch"></param>
		public void draw(SpriteBatch sBatch) {
			foreach (ProjectileType projectile in projectiles)
				projectile.draw(sBatch);
		}


	}
}
