using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Controllers;


namespace SpaceUnionXNA.Weapons.Systems {
	/// <summary>
	/// A special type of weapon that fires a single beam of energy in
	/// a continuous stream.
	/// @Written by Tristan
	/// </summary>
	public class LaserBeam : Sprite, WeaponSystem {


		public int beamLength = 250;

		public Ship owner { get; set; }
		public int weaponDamage { get; set; }
		/// <summary>
		/// The bounds of the smallest constituent of the beam. Most likely a pixel
		/// or very close to one.
		/// </summary>
		private Rectangle beamQuantum;
		/// <summary>
		/// List of all the beamQuanta that will combine to create the beam.
		/// </summary>
		private List<Rectangle> beamQuanta = new List<Rectangle>();
		private Vector2 beamDirection;
		public float distToTarget;
		private  bool beamOn;
		private  int startQuanta;

		public LaserBeam(Vector2 startPoint, Ship ship)
			: base(assets.guiRectangle, startPoint) {

			owner = ship;

			weaponDamage = 1;
			beamLength = 250;

			setupAnimation();
		}

		private void setupAnimation() {

			// the size of each tile
			setSize(5, 11); // must be explicitly set for tile sheets

			AnimationClass anima = new AnimationClass();
			addAnimation("laser", 0, 5, anima.copy());

			frameLength = .8f;
			animation = "laser";
			frameIndex = 0;
		}

		public override void addAnimation(string name, int row, int frameCount,
			AnimationClass anima) {

			Rectangle[] recs = new Rectangle[frameCount];

			for (int i = 0; i < frameCount; i++)
				recs[i] = new Rectangle(i * width,
					row * height, width, height);

			anima.frameCount = frameCount;
			anima.frames = recs;
			animations.Add(name, anima);
		}

		

		public void update(GameTime gameTime, QuadTree quadTree) {

			if (!beamOn)
				return;
			beamQuanta.Clear();
			//beamQuantum = new Rectangle((int) position.X, (int) position.Y, 1, 3);

			beamDirection = new Vector2((float) Math.Sin(rotation), (float) -Math.Cos(rotation));
			Ray2 ray = new Ray2(position, beamDirection * beamLength);


			distToTarget = 1;

			// find targets within line of fire
			/*List<Tangible> possibleCollisions = quadTree.retrieveNeighbors(owner);*/ /* This method and may result in missed ships.
																		   * A different retriever using a raycast
																		   will likely be necessary. -Tristan-*/

			List<Tangible> possibleCollisions = GameplayScreen.targets;

			Tangible currentTarget = null;

			foreach (Tangible target in possibleCollisions) {
				if (target != owner) {

					if (ray.intersectsToRange(target.getHitBox())) {
						float temp = ray.getDistance();
						if (temp <= distToTarget) {
							// fine hit detection

							//Color[] rawData = new Color[target.width * target.height];
							//target.texture.GetData<Color>(rawData);

							//Color[,] rawDataGrid = new Color[height, width];
							//int row = 0, col = 0;
							//for (row = 0; row < height; ++row)
							//	for (col = 0; col < width; ++col)
							//		rawDataGrid[row, col] = rawData[row * width + col];

							//// get t0x and t0y in texture cooridinates
							//Vector2 t0 = new Vector2(ray.t0x - (target.getX() - width / 2),
							//	ray.t0y - (target.getY() - height / 2));

							//Vector2 check = t0;

							//for (int i = (int) (beamLength * temp); i < beamLength; ++i) {

							//	check.X += beamDirection.X;
							//	check.Y += beamDirection.Y;

							//	if (check.X > col || check.X < 0 ||
							//		check.Y > row || check.Y < 0)
							//		break;

							//	if (rawDataGrid[(int) check.Y, (int) check.X].A != 0) {
							//		float add = (float) Math.Sqrt(check.X * check.X + check.Y * check.Y) / beamLength;
							//		distToTarget = temp + add;
							//		currentTarget = target;
							//		break;
							//	}

							distToTarget = temp;
							currentTarget = target;
							//}
						}
					}
				}
			}

			if (currentTarget != null) {
				doDamage(currentTarget, gameTime);
			}

			frameIndex = startQuanta;

			for (int i = 0; i < beamLength * distToTarget; ++i) {

				//beamQuanta.Add(assets.Content.Load<Texture2D>("Projectiles/molten bullet (6x8)"));// test texture


				beamQuanta.Add(getNextQuanta());
				if (++frameIndex > 4)
					frameIndex = 0;
			}

			if (--startQuanta < 0)
				startQuanta = 4;
			beamOn = false;
		}

		private Rectangle getNextQuanta() {

			return animations[animation].frames[frameIndex];
		}


		public override void draw(SpriteBatch spriteBatch) {
			foreach (Rectangle rect in beamQuanta) {

				position.X += beamDirection.X;
				position.Y += beamDirection.Y;
				//beamQuantum.X = (int) position.X;
				//beamQuantum.Y = (int) position.Y;
				//spriteBatch.Draw(dot, beamQuantum, Color.White);

				spriteBatch.Draw(texture, position, rect,
				animations[animation].color,
				animations[animation].rotation,
				origin,
				scale,
				animations[animation].spriteEffect,
				0f);
			}
			beamQuanta.Clear();

		}





		public void updatePosition(Vector2 startPoint, float rot) {
			position = startPoint;
			rotation = rot;
			beamOn = true;
		}


		public void doDamage(Tangible target, GameTime gameTime) {
			target.takeDamage(weaponDamage, gameTime, owner);
		}


		public void fire(Vector2 startPoint) {
			throw new NotImplementedException();
		}

	}
}
