using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;


namespace SpaceUnion.Weapons {
	class LaserBeam : Sprite, WeaponSystem {


		int beamLength = 250;

		public Ship owner { get; set; }
		public int weaponDamage { get; set; }

		private Rectangle rect;
		private Vector2 beamDirection;
		private List<Texture2D> pixels = new List<Texture2D>();

		public LaserBeam(Vector2 startPoint, Ship ship)
			: base(assets.guiRectangle, startPoint) {

			owner = ship;

			weaponDamage = 1;
		}


		public new void update(GameTime gameTime, QuadTree quadTree) {

			Ray ray = new Ray(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
			beamDirection = new Vector2((float) Math.Sin(rotation), (float) -Math.Cos(rotation));
			pixels.Clear();
			rect = new Rectangle((int) position.X, (int) position.Y, 1, 3);

			float t = 1;
			// find targets within line of fire

			// if collision
				// find closest edge of target
				// and find how long beam will be
				// t = CollisionHandler.findT();
				// apply damage, etc
			for (int i = 0; i < beamLength*t; ++i) {
				
				pixels.Add(assets.Content.Load<Texture2D>("Projectiles/molten bullet (6x8)"));// test texture

			}

		}


		public override void draw(SpriteBatch spriteBatch) {
			foreach (Texture2D dot in pixels) {

				position.X += beamDirection.X;
				position.Y += beamDirection.Y;
				rect.X = (int) position.X;
				rect.Y = (int) position.Y;
				spriteBatch.Draw(dot, rect, Color.White);
			}

		}

		public void updatePosition(Vector2 startPoint, float rot) {
			position = startPoint;
			rotation = rot;
		}

		
		public void doDamage(Tangible target, GameTime gameTime) {
			throw new NotImplementedException();
		}

	}
}
