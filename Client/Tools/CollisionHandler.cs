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

			//elasticCollision(ship1, ship2);
			reflect(ship1, ship2);
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
			//elasticCollision(ship, asteroid);
			reflect(ship, asteroid);
		}

		/// <summary>
		/// The outcome of a Asteroid on Asteroid collision
		/// </summary>
		/// <param name="asteroid1"></param>
		/// <param name="asteroid2"></param>
		/// <param name="gameTime"></param>
		public static void asteroidOnAsteroid(Asteroid asteroid1, Asteroid asteroid2, GameTime gameTime) {

			elasticCollision(asteroid1, asteroid2);
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

		/// <summary>
		/// An elastic collision that takes angle of contact into consideration but not mass.
		/// @Written by Tristan
		/// </summary>
		/// <param name="tangible1"></param>
		/// <param name="tangible2"></param>
		public static void elasticCollision(Tangible tangible1, Tangible tangible2) {


			Vector2 v2 = tangible2.velocity;
			// Change frame of reference to tangible2
			Vector2 u1 = tangible1.velocity - v2;
			Vector2 u2 = Vector2.Zero;

			// Find vector between center of masses
			Vector2 c = new Vector2(tangible2.position.X - tangible1.position.X, tangible2.position.Y - tangible1.position.Y);
			c.Normalize();

			// Find rotation for u1 to be aligned with y-axis and rotate c by that angle
			float rotation = (float) Math.Acos(u1.Y / u1.Length());


			Vector2 cR = Vector2.Transform(c, Matrix.CreateRotationZ(rotation));
			// Find angle of deflection
			float deflection = (float) Math.Acos(cR.Y / cR.Length());

			// Solve for u1prime and u2prime scalar values
			Vector2 u1prime = Vector2.Zero, u2prime = Vector2.Zero;
			float u1primeLength = u1.Length() * (float) Math.Sin(deflection);
			float u2primeLength = u1.Length() * (float) Math.Cos(deflection);

			// find c's parallel
			Vector2 p = new Vector2(-c.Y, c.X);

			// find u1prime & u2prime
			u1prime.X = u1primeLength * p.X;
			u1prime.Y = u1primeLength * p.Y;

			u2prime.X = u2primeLength * c.X;
			u2prime.Y = u2primeLength * c.Y;

			// Change back to lab frame and calculate final velocities
			tangible1.velocity = u1prime + v2;
			tangible2.velocity = u2prime + v2;
		}
	}
}
