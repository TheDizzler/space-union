using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Controllers;

namespace SpaceUnionXNA.Weapons.Projectiles {
	public class HomingMissile : Projectile {

		protected float maxSpeed = 5000;
		/// <summary>
		/// How many units(pixels) per second a ship will travel more per second of thrust
		/// </summary>
		protected float accelSpeed;
		/// <summary>
		/// Turn speed in radians per second
		/// </summary>
		protected float turnSpeed;
		protected Ship target;

		public HomingMissile(Vector2 startPoint, Ship ship)
			: base(assets.homingMissile, startPoint, ship) {


			projectileTTL = 2.5f;
			accelSpeed = 300f;
			turnSpeed = 10f;
			weaponDamage = 10;
			isActive = false;
		}


		public override void update(GameTime gameTime, QuadTree quadTree) {

			timeActive += (float) gameTime.ElapsedGameTime.TotalSeconds;
			if (projectileTTL > timeActive) {

				if (timeActive > .5) { // gives some time for missiles to get a safe distance from ship
					if (target == null) {
						target = findFirstTarget(quadTree);
					}

					if (target != null) {
						// rotate towards target
						double angle = Math.Atan2(this.Position.X - target.Position.X, this.Position.Y - target.Position.Y);
						float difference = rotation - (float) -angle;


							//float checkPlus = rotation + turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
							//float checkMinus = rotation - turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

							if (difference < -.01)
								rotation = rotation + turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
							else if (difference > .01)
								rotation = rotation - turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
						

						//rotation = (float) -angle;
						//if (rotation > 6.283185 || rotation < -6.283185) {
						//	rotation = rotation % 6.283185f;
						//}

					}
				}
				accelerate(gameTime, quadTree);
			} else {
				destroy();
			}

		}

		private void accelerate(GameTime gameTime, QuadTree quadTree) {
			Vector2 acceleration = new Vector2((float) Math.Sin(rotation), (float) -Math.Cos(rotation));
			acceleration *= accelSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
			Vector2.Add(ref velocity, ref acceleration, out velocity);

			position += velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;

			base.update(position);

			checkForCollision(quadTree, gameTime);

		}

		/// <summary>
		/// A very unintelligent target finder. Findest the first available target in list.
		/// </summary>
		/// <param name="quadTree"></param>
		/// <returns></returns>
		private Ship findFirstTarget(QuadTree quadTree) {

			List<Tangible> possibleCollisions = quadTree.retrieve(this); // Need a better retrieve method for rays

			foreach (Tangible tang in possibleCollisions.ToList<Tangible>()) {
				if (tang is Ship && tang.isActive && tang != this) {
					Ship ship = (Ship) tang;
					if (ship.blueTeam != owner.blueTeam) {
						return ship;
					}
				}
			}

			return null;
		}


		public override List<SoundEffect> getFireSFX() {

			List<SoundEffect> fireSFXs = new List<SoundEffect>();
			fireSFXs.Add(assets.torpedo);
			//fireSFXs.Add(assets.laserbolt1);
			//fireSFXs.Add(assets.laserbolt2);
			//fireSFXs.Add(assets.laserbolt3);

			return fireSFXs;
		}

		/// <summary>
		/// Add unique collision behaviour here
		/// </summary>
		/// <param name="target"></param>
		/// <param name="gameTime"></param>
		public override void collide(Tangible target, GameTime gameTime) {

			base.collide(target, gameTime);
		}

		/// <summary>
		/// Add unique damage behaviour here
		/// </summary>
		/// <param name="target"></param>
		/// <param name="gameTime"></param>
		public override void doDamage(Tangible target, GameTime gameTime) {
			explosionEngine.createBigExplosion(position);
			base.doDamage(target, gameTime);
		}


		public override void destroy() {
			target = null;
			base.destroy();
		}
	}
}
