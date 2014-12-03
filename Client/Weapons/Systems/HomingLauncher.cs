using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Controllers;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons.Projectiles;
using Microsoft.Xna.Framework.Audio;

namespace SpaceUnionXNA.Weapons.Systems {
	public class HomingLauncher : WeaponSystem {

		HomingMissile missile;
		private  List<SoundEffect> fireSFXs;
		private Random random;

		public Ship owner { get; set; }

		public HomingLauncher(Ship ship) {
			owner = ship;
			missile = new HomingMissile(Vector2.Zero, ship);

			fireSFXs = missile.getFireSFX();
			random = new Random();
		}


		public void fire(Vector2 startPoint) {

			if (!missile.isActive) {
				missile.launch(startPoint, (float) owner.getRotation(), owner.velocity);
				GameplayScreen.targets.Add(missile);
				playFireSFX();
			} else
				playEmptySFX();
		}

		public void update(GameTime gameTime, QuadTree quadTree) {
			if (missile.isActive)
				missile.update(gameTime, quadTree);
		}

		public void draw(SpriteBatch sBatch) {
			if (missile.isActive || owner.isActive)
				missile.draw(sBatch);
		}

		public void updatePosition(Vector2 startPoint, float rot) {
			if (!missile.isActive) {
				missile.position = startPoint;
				missile.rotation = rot;
			}
		}


		public void playFireSFX() {

			int num = random.Next(fireSFXs.Count);
			fireSFXs[num].Play();
		}


		public void playEmptySFX() {

		}
	}
}
