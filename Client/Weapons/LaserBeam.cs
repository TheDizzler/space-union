using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Ray;
using SpaceUnion.Tools;


namespace SpaceUnion.Weapons {
	class LaserBeam : Projectile {


		int beamLength = 250;


		private Rectangle rect;
		private Vector2 lineDirection;
		private List<Texture2D> dots = new List<Texture2D>();

		public LaserBeam(Vector2 startPoint, Ship ship)
			: base(assets.guiRectangle, startPoint, ship) {

			projectileTTL = .5f;
			projectileMoveSpeed = 2.2f;

			//velocity += new Vector2((float) Math.Sin(rotation) * projectileMoveSpeed,
			//	(float) -Math.Cos(rotation) * projectileMoveSpeed);

			projectileDamage = 3;
		}

		public override void destroy() {

		}

		public new void update(GameTime gameTime, QuadTree quadTree) {

			Ray ray = new Ray(new Vector3(0,0,0), new Vector3(0,0,0));
			lineDirection = new Vector2((float) Math.Sin(rotation), (float) -Math.Cos(rotation));
			//lineDirection.Normalize();
			dots.Clear();

			rect = new Rectangle((int) position.X, (int) position.Y, 1, 3);
			for (int i = 0; i < beamLength; ++i) {
				Texture2D dot = assets.Content.Load<Texture2D>("Projectiles/molten bullet (6x8)");
				dots.Add(dot);

			}

		}


		public override void draw(SpriteBatch spriteBatch) {
			foreach (Texture2D dot in dots) {

				position.X += lineDirection.X;
				position.Y += lineDirection.Y;
				rect.X = (int) position.X;
				rect.Y = (int) position.Y;
				spriteBatch.Draw(dot, rect, Color.White);
			}

		}

		public void updatePosition(Vector2 startPoint, float rot) {
			position = startPoint;
			rotation = rot;
		}

	}
}
