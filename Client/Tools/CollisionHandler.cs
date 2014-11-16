using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.StellarObjects;
using SpaceUnionXNA.Weapons;


namespace SpaceUnionXNA.Tools {
	/// <summary>
	/// A class to handle collisions between tangible objects.
	/// Does not handle weapon collisions as those are probably weapon specific.
	/// Will likely expand to include ray casting.
	/// @Written by Tristan
	/// </summary>
	public static class CollisionHandler {


		/// <summary>
		/// The outcome of a Ship on Ship collision
		/// </summary>
		/// <param name="ship1"></param>
		/// <param name="ship2"></param>
		/// <param name="gameTime"></param>
		public static void shipOnShip(Ship ship1, Ship ship2, GameTime gameTime) {

			//reflect(ship1, ship2);
		}

		/// <summary>
		/// The outcome of a Ship on Planet collision
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="planet"></param>
		/// <param name="gameTime"></param>
		public static void shipOnPlanet(Ship ship, Planet planet, GameTime gameTime) {

			ship.takeDamage(planet.collisionDamage, gameTime, ship);
			reflect(ship, planet);
		}

		/// <summary>
		/// The outcome of a Ship on Asteroid collision
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="asteroid"></param>
		/// <param name="gameTime"></param>
		public static void shipOnAsteroid(Ship ship, Asteroid asteroid, GameTime gameTime) {
			ship.takeDamage(asteroid.collisionDamage, gameTime, ship);
			//asteroid.takeDamage(asteroid.collisionDamage, gameTime);
			reflect(ship, asteroid);
		}

		/// <summary>
		/// The outcome of a Asteroid on Asteroid collision
		/// </summary>
		/// <param name="asteroid1"></param>
		/// <param name="asteroid2"></param>
		/// <param name="gameTime"></param>
		public static void asteroidOnAsteroid(Asteroid asteroid1, Asteroid asteroid2, GameTime gameTime) {

			reflect(asteroid1, asteroid2);
		}

		/// <summary>
		/// The outcome of a Asteroid on Planet collision
		/// </summary>
		/// <param name="asteroid"></param>
		/// <param name="planet"></param>
		/// <param name="gameTime"></param>
		public static void asteroidOnPlanet(Asteroid asteroid, Planet planet, GameTime gameTime) {

			asteroid.destroy();
		}

		/// <summary>
		/// :O
		/// </summary>
		/// <param name="planet1"></param>
		/// <param name="planet2"></param>
		/// <param name="gameTime"></param>
		public static void planetOnPlanet(Planet planet1, Planet planet2, GameTime gameTime) {
			throw new NotImplementedException();
		}



		/// <summary>
		/// "Bounce" an object off of another. Not great.... :/
		/// @Written by Tristan with help fromXNA 4.0 Game Development by Example -Jaegers Packt(2010)
		/// </summary>
		public static void reflect(Tangible tangible1, Tangible tangible2) {

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

			//Vector2 tang1VelFinal = (tangible1.velocity * (tangible1.mass - tangible2.mass) + 2 * tangible2.mass * tangible2.velocity) / (tangible1.mass + tangible1.mass);
			//tang1VelFinal += tangible1.velocity;


		}

		/// <summary> *DEPRECATED?*
		/// Helper function for ray casting.
		/// Finds how far along the ray that the collision occurs (t in the parametric equation)
		/// </summary>
		/// <param name="lineStart"></param>
		/// <param name="lineEnd"></param>
		/// <param name="rayStart">the point the ray (or segment) originates from</param>
		/// <param name="rayEnd">either the end point of segment for the direction of the ray</param>
		/// <returns>for a segment 0 to 1 means a hit, for a ray any positive number is a hit </returns>
		public static float findT(Vector2 lineStart, Vector2 lineEnd, Vector2 rayStart, Vector2 rayEnd) {

			float dividend = rayStart.X * (lineEnd.Y - lineStart.Y) + lineStart.X * (rayStart.Y - lineEnd.Y) + lineEnd.X * (lineStart.Y - rayStart.Y);
			float divisor = lineEnd.X * (rayEnd.Y - rayStart.Y) - lineStart.X * (rayEnd.Y - rayStart.Y) - lineEnd.Y * (rayEnd.X - rayStart.X) + lineStart.Y * (rayEnd.X - rayStart.X);

			if (Math.Abs(divisor) < Ray2.EPSILON) // someone goofed
				return -1;

			return dividend / divisor;

		}


	}
}
