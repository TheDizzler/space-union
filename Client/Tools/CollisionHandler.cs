using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.StellarObjects;
using SpaceUnionXNA.Weapons;
using SpaceUnion.Tools;
using SpaceUnionXNA.Weapons.Projectiles;


namespace SpaceUnionXNA.Tools {
	/// <summary>
	/// A class to handle collisions between tangible objects.
	/// Does not handle weapon collisions as those are probably weapon specific.
	/// Will likely expand to include ray casting.
	/// @Written by Tristan
	/// </summary>
	public class CollisionHandler {


		/// <summary>
		/// Holds all pending collisions.
		/// </summary>
		public MultiValueDictionary<Tangible, Tangible> collisions = new MultiValueDictionary<Tangible, Tangible>();

		/// <summary>
		/// At end of update dictionary needs to be erased.(?)
		/// </summary>
		/// <param name="gameTime"></param>
		public void update(GameTime gameTime) {
			// Go through each collision
			foreach (Tangible key in collisions.Keys) {
				foreach (Tangible value in collisions.GetValues(key, false)) {
					//System.Console.WriteLine("Resolving collision " + key.GetType().Name + " with " + value.GetType().Name + " at " + gameTime.TotalGameTime.TotalMilliseconds);
					key.collide(value, gameTime);
				}
			}
			//System.Console.WriteLine("Before collisions.Count " + collisions.Count);
			collisions.Clear();
			//System.Console.WriteLine("After collisions.Count " + collisions.Count);
		}

		/// <summary>
		/// Add a collision to be handled.
		/// </summary>
		/// <param name="tangible1"></param>
		/// <param name="tangible2"></param>
		public void addCollision(Tangible tangible1, Tangible tangible2, GameTime gameTime) {
			// check to see if collision already is in dictionary to prevent a double calculation of collision
			if (!collisionExists(tangible1, tangible2)) {
				collisions.Add(tangible1, tangible2);
				System.Console.WriteLine("new collision " + tangible1.GetType().Name + " with " + tangible2.GetType().Name + " at " + gameTime.TotalGameTime.TotalMilliseconds);
			} else
			System.Console.WriteLine("Collision already exists at " + gameTime.TotalGameTime.TotalMilliseconds);
		}

		/// <summary>
		/// Since collisions are added as the original updater being tangible1, any overlaps are likely
		/// to have tangible2 being the key, therefore tangible2 is checked first in each && statement
		/// </summary>
		/// <param name="tangible1"></param>
		/// <param name="tangible2"></param>
		/// <returns>True if the collision already exists, false otherwise</returns>
		private bool collisionExists(Tangible tangible1, Tangible tangible2) {
			// If this collision already exists, dictionary has to contain one or the other as a key
			//if (!collisions.ContainsKey(tangible2) && !collisions.ContainsKey(tangible1)) {
			//	return false;
			//}
			// The collision may exist as key-value pair of <tangible1, tangible2> or <tangible2, tangible1>
			if (!collisions.ContainsValue(tangible2, tangible1) && !collisions.ContainsValue(tangible1, tangible2))
				return false;
			return true;
		}


		/// <summary>
		/// The outcome of a Ship on Ship collision
		/// </summary>
		/// <param name="ship1"></param>
		/// <param name="ship2"></param>
		/// <param name="gameTime"></param>
		public void shipOnShip(Ship ship1, Ship ship2, GameTime gameTime) {

			//elasticCollision(ship1, ship2);
			reflect(ship1, ship2);
		}

		/// <summary>
		/// The outcome of a Ship on Planet collision
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="planet"></param>
		/// <param name="gameTime"></param>
		public void shipOnPlanet(Ship ship, Planet planet, GameTime gameTime) {

			reflectPlanet(ship, planet);
			//elasticCollision(ship, planet);
			ship.takeDamage(planet.collisionDamage, gameTime, null);

		}

		/// <summary>
		/// The outcome of a Ship on Asteroid collision
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="asteroid"></param>
		/// <param name="gameTime"></param>
		public void shipOnAsteroid(Ship ship, Asteroid asteroid, GameTime gameTime) {
			elasticCollision(ship, asteroid);
			//reflect(ship, asteroid);
			//ship.takeDamage(asteroid.collisionDamage, gameTime, null);
			//asteroid.takeDamage(asteroid.collisionDamage, gameTime);
		}

		/// <summary>
		/// The outcome of a Asteroid on Asteroid collision
		/// </summary>
		/// <param name="asteroid1"></param>
		/// <param name="asteroid2"></param>
		/// <param name="gameTime"></param>
		public void asteroidOnAsteroid(Asteroid asteroid1, Asteroid asteroid2, GameTime gameTime) {

			elasticCollision(asteroid1, asteroid2);
		}

		/// <summary>
		/// The outcome of a Asteroid on Planet collision
		/// </summary>
		/// <param name="asteroid"></param>
		/// <param name="planet"></param>
		/// <param name="gameTime"></param>
		public void asteroidOnPlanet(Asteroid asteroid, Planet planet, GameTime gameTime) {

			asteroid.destroy();
		}

		/// <summary>
		/// :O
		/// </summary>
		/// <param name="planet1"></param>
		/// <param name="planet2"></param>
		/// <param name="gameTime"></param>
		public void planetOnPlanet(Planet planet1, Planet planet2, GameTime gameTime) {
			throw new NotImplementedException();
		}



		/// <summary>
		/// "Bounce" an object off of another. Not great.... :/
		/// @Written by Tristan with help fromXNA 4.0 Game Development by Example -Jaegers Packt(2010)
		/// </summary>
		public void reflect(Tangible tangible1, Tangible tangible2) {

			Vector2 combinedMassVel = // if both masses stick together (inelastic collision) than the resulting velocity is combinedMassVel
				(tangible1.velocity + tangible2.velocity) / 2;

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


		private void reflectPlanet(Ship ship, Planet planet) {

			Vector2 combinedMassVel = ship.velocity / 5;

			Vector2 normal1 = planet.position - ship.position;
			normal1.Normalize();

			ship.velocity -= combinedMassVel;
			ship.velocity = Vector2.Reflect(ship.velocity, normal1);
			ship.velocity += combinedMassVel;
		}

		/// <summary>
		/// An elastic collision that takes angle of contact into consideration but not mass.
		/// @Written by Tristan
		/// </summary>
		/// <param name="tangible1"></param>
		/// <param name="tangible2"></param>
		public void elasticCollision(Tangible tangible1, Tangible tangible2) {


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
