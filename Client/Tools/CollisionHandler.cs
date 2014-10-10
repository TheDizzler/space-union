using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceUnion.StellarObjects;
using SpaceUnion.Weapons;


namespace SpaceUnion.Tools {
	/// <summary>
	/// A class to handle collisions between tangible objects.
	/// </summary>
	public class CollisionHandler {

		/// <summary>
		/// Holds all pending collisions.
		/// </summary>
		//public MultiValueDictionary<Tangible, Tangible> collisions = new MultiValueDictionary<Tangible, Tangible>();

		/// <summary>
		/// At end of update dictionary needs to be erased.
		/// </summary>
		//public void update() {

		//	// Go through each collision
		//	foreach (Tangible key in collisions.Keys) {
		//		foreach (Tangible value in collisions.GetValues(key, false)) {

		//			// determine if value is a projectile
		//			if (value is Projectile)
		//				value.collide(key);
		//			else
		//				key.collide(value);


		//			//determine what kind of collision is occuring
		//			//else if (key is Ship) {
		//			//	if (value is Ship)
		//			//		shipOnShip(key, value);
		//			//	if (value is Asteroid)
		//			//		shipOnAsteroid(key, value);
		//			//}
		//		}
		//	}


		//	collisions.Clear();
		//}

		/// <summary>
		/// Add a collision to be handled.
		/// </summary>
		/// <param name="tangible1"></param>
		/// <param name="tangible2"></param>
		//public void addCollision(Tangible tangible1, Tangible tangible2) {

		//	// check to see if collision already is in dictionary to prevent a double calculation of collision
		//	if (!collisionExists(tangible1, tangible2)) {
		//		collisions.Add(tangible1, tangible2);
		//	}
		//}

		/// <summary>
		/// Since collisions are added as the original updater being tangible1, any overlaps are likely
		/// to have tangible2 being the key, therefore tangible2 is checked first in each && statement
		/// </summary>
		/// <param name="tangible1"></param>
		/// <param name="tangible2"></param>
		/// <returns>True if the collision already exists, false otherwise</returns>
		//private bool collisionExists(Tangible tangible1, Tangible tangible2) {

		//	// If this collision already exists, dictionary has to contain one or the other as a key
		//	if (!collisions.ContainsKey(tangible2) && !collisions.ContainsKey(tangible1)) {
		//		return false;
		//	}
		//	// The collision may exist as key-value pair of <tangible1, tangible2> or <tangible2, tangible1>
		//	if (!collisions.ContainsValue(tangible2, tangible1) && !collisions.ContainsValue(tangible1, tangible2))
		//		return false;

		//	return true;

		//}

		/// <summary>
		/// The outcome of a Ship on Ship collision
		/// </summary>
		/// <param name="ship1"></param>
		/// <param name="ship2"></param>
		public void shipOnShip(Ship ship1, Ship ship2) {

			throw new NotImplementedException();
		}

		/// <summary>
		/// The outcome of a Ship on Planet collision
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="planet"></param>
		public void shipOnPlanet(Ship ship, Planet planet) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// The outcome of a Ship on Asteroid collision
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="asteroid"></param>
		public void shipOnAsteroid(Ship ship, Asteroid asteroid) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// The outcome of a Asteroid on Asteroid collision
		/// </summary>
		/// <param name="asteroid1"></param>
		/// <param name="asteroid2"></param>
		public void asteroidOnAsteroid(Asteroid asteroid1, Asteroid asteroid2) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// The outcome of a Asteroid on Planet collision
		/// </summary>
		/// <param name="asteroid"></param>
		/// <param name="planet"></param>
		public void asteroidOnPlanet(Asteroid asteroid, Planet planet) {
			
			asteroid.destroy();
		}

		/// <summary>
		/// :O
		/// </summary>
		/// <param name="planet1"></param>
		/// <param name="planet2"></param>
		public void planetOnPlanet(Planet planet1, Planet planet2) {
			throw new NotImplementedException();
		}

		

	}
}
