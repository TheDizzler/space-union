using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Tools;


using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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


			projectileTTL = 3.5f;
			//projectileMoveSpeed = 1000.2f;
			accelSpeed = 1000f;
			turnSpeed = 4f;
			weaponDamage = 10;
			isActive = false;
		}


		public override void update(GameTime gameTime, QuadTree quadTree) {

			if (target == null) {
				target = findFirstTarget(quadTree);
			}

			if (target != null) {
				// rotate towards target
				double angle = Math.Atan2(this.Position.X - target.Position.X, this.Position.Y - target.Position.Y);
				//System.Console.WriteLine(angle);
				//if (rotation > angle - .025)
				//	rotation += turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
				//if (rotation < angle + .025)
				//	rotation -= turnSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
				rotation = (float) -angle;
			}

			timeActive += (float) gameTime.ElapsedGameTime.TotalSeconds;
			if (projectileTTL > timeActive) {

				Vector2 acceleration = new Vector2((float) Math.Sin(rotation), (float) -Math.Cos(rotation));
				acceleration *= accelSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
				Vector2.Add(ref velocity, ref acceleration, out velocity);


				position += velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;

				base.update(position);

				checkForCollision(quadTree, gameTime);

			} else {
				destroy();
			}

		}

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
			fireSFXs.Add(assets.laserbolt0);
			fireSFXs.Add(assets.laserbolt1);
			fireSFXs.Add(assets.laserbolt2);
			fireSFXs.Add(assets.laserbolt3);

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
