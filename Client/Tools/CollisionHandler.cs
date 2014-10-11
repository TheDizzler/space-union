﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceUnion.StellarObjects;
using SpaceUnion.Weapons;


namespace SpaceUnion.Tools {
	/// <summary>
	/// A class to handle collisions between tangible objects.
	/// Does not handle weapon collisions as those are probably weapon specific.
	/// </summary>
	public class CollisionHandler {


		/// <summary>
		/// The outcome of a Ship on Ship collision
		/// </summary>
		/// <param name="ship1"></param>
		/// <param name="ship2"></param>
		public void shipOnShip(Ship ship1, Ship ship2, GameTime gameTime) {

			throw new NotImplementedException();
		}

		/// <summary>
		/// The outcome of a Ship on Planet collision
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="planet"></param>
		public void shipOnPlanet(Ship ship, Planet planet, GameTime gameTime) {

			ship.takeDamage(planet.collisionDamage, gameTime);
			reflect(ship, planet);
		}

		/// <summary>
		/// The outcome of a Ship on Asteroid collision
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="asteroid"></param>
		public void shipOnAsteroid(Ship ship, Asteroid asteroid, GameTime gameTime) {
			ship.takeDamage(asteroid.collisionDamage, gameTime);
			asteroid.takeDamage(asteroid.collisionDamage, gameTime);
			reflect(ship, asteroid);
		}

		/// <summary>
		/// The outcome of a Asteroid on Asteroid collision
		/// </summary>
		/// <param name="asteroid1"></param>
		/// <param name="asteroid2"></param>
		public void asteroidOnAsteroid(Asteroid asteroid1, Asteroid asteroid2, GameTime gameTime) {

			reflect(asteroid1, asteroid2);
		}

		/// <summary>
		/// The outcome of a Asteroid on Planet collision
		/// </summary>
		/// <param name="asteroid"></param>
		/// <param name="planet"></param>
		public void asteroidOnPlanet(Asteroid asteroid, Planet planet, GameTime gameTime) {

			asteroid.destroy();
		}

		/// <summary>
		/// :O
		/// </summary>
		/// <param name="planet1"></param>
		/// <param name="planet2"></param>
		public void planetOnPlanet(Planet planet1, Planet planet2, GameTime gameTime) {
			throw new NotImplementedException();
		}



		/// <summary>
		/// "Bounce" an object off of another
		/// </summary>
		public void reflect(Tangible tangible1, Tangible tangible2) {

			Vector2 combinedMassVel = // if both masses stick together (inelastic collision) than the resulting velocity is combinedMassVel
				(tangible1.velocity + tangible2.velocity) / (tangible1.mass + tangible2.mass);
			
			Vector2 normal1 = tangible2.position - tangible1.position;
			normal1.Normalize();
			Vector2 normal2 = tangible1.position - tangible2.position;
			normal2.Normalize();

			tangible1.velocity -= combinedMassVel;
			tangible1.velocity = Vector2.Reflect(tangible1.velocity, normal1);
			tangible1.velocity += combinedMassVel;

			tangible2.velocity -= combinedMassVel;
			tangible2.velocity = Vector2.Reflect(tangible2.velocity, normal1);
			tangible2.velocity += combinedMassVel;

		}

	}
}