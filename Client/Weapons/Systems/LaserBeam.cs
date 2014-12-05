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


		public int beamLength;

		public Ship owner { get; set; }
		public int weaponDamage { get; set; }
		/// <summary>
		/// The bounds of the smallest constituent of the beam. Most likely a pixel
		/// or very close to one.
		/// </summary>
		//private Rectangle beamQuantum;
		/// <summary>
		/// List of all the beamQuanta that will combine to create the beam.
		/// </summary>
		private List<Rectangle> beamQuanta = new List<Rectangle>();
		private Vector2 beamDirection;
		public float distToTarget;
		private  bool beamOn;
		private  int startQuanta;

		Random particleSeed = new Random();


		public LaserBeam(Vector2 startPoint, Ship ship)
			: base(assets.redLaser2, startPoint) {

			owner = ship;

			weaponDamage = 1;
			beamLength = 175;

			setupAnimation();
		}

		private void setupAnimation() {

			// the size of each tile
			setSize(3, 11); // must be explicitly set for tile sheets

			AnimationClass anima = new AnimationClass();
			addAnimation("laser", 0, 10, anima.copy());

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
			anima.rotation = (float)Math.PI/4;
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
			/*List<Tangible> possibleCollisions = quadTree.retrieveNeighbors(owner);*/
										/* This method and may result in missed ships.
										 * A different retriever using a raycast
										 * will likely be necessary. -Tristan-*/

			List<Tangible> possibleCollisions = GameplayScreen.targets;

			Tangible currentTarget = null;

			foreach (Tangible target in possibleCollisions) {
				if (target != owner && target.isActive) {

					if (ray.intersectsToRange(target.getHitBox())) {
						
						float temp = ray.getDistance();
						if (temp <= distToTarget) {
							// fine hit detection
							narrowCheck(target, temp, ref currentTarget);
						}
					}
				}
			}

			if (currentTarget != null) {
				doDamage(currentTarget, gameTime);
			}

			frameIndex = startQuanta;

			for (int i = 0; i < beamLength * distToTarget; ++i) {

				beamQuanta.Add(getNextQuanta());
				if (++frameIndex > 4)
					frameIndex = 0;
			}

			if (--startQuanta < 0)
				startQuanta = 4;
			beamOn = false;
		}

		private void narrowCheck(Tangible target, float temp, ref Tangible currentTarget) {

			Color[] rawData = new Color[target.width * target.height];
			target.texture.GetData<Color>(rawData);
			Color[,] rawDataGrid = new Color[target.width, target.height];
			for (int x = 0; x < target.width; ++x)
				for (int y = 0; y < target.height; ++y)
					rawDataGrid[x, y] = rawData[x + y * target.width];

			float distanceTo = temp * beamLength;
			// start checking pixel-by-pixel from this point
			Vector2 startFrom = new Vector2(beamDirection.X * distanceTo + position.X, beamDirection.Y * distanceTo + position.Y);
			Vector2 checkV = startFrom;
			Point check = new Point((int) startFrom.X, (int) startFrom.Y);
			Rectangle hb = target.getHitBox().getArray();
			Vector2 length;
			length = position - checkV;
			if (length.Y > 0)
				check.Y -= 1;
			if (length.X > 0)
				check.X -= 1;
			// until out of hitbox or beamLength exceeded
			while (hb.Contains(check)) {
				Color color = rawDataGrid[Math.Abs(hb.X - check.X), Math.Abs(hb.Y - check.Y)];
				// if color is not transparent
				if (color.A != 0) {
					distToTarget = length.Length() / beamLength;
					currentTarget = target;
					return;
				}
				checkV.X += beamDirection.X;
				checkV.Y += beamDirection.Y;
				length = position - checkV;
				if (length.Length() > beamLength)
					return;
				check.X = (int) checkV.X;
				check.Y = (int) checkV.Y;
			}
		}

		private Rectangle getNextQuanta() {
			return animations[animation].frames[frameIndex];
		}

		public override void draw(SpriteBatch spriteBatch) {

			int numParticles = 0;
			Vector2 quantaPosition = position;

			Random rand1 = new Random();
			Random rand2 = new Random();
			int interation = 0;

			foreach (Rectangle rect in beamQuanta) {

				quantaPosition.X += beamDirection.X;
				quantaPosition.Y += beamDirection.Y;

				//numParticles = particleSeed.Next(3) -1;
				//for (int i = 0; i < numParticles; ++i) {
				if (interation % 17 == 0)
					Game1.particleEngine.createSparkle(quantaPosition, rand1.Next(10) - 5, rand2.NextDouble(), owner.velocity);
				if (interation % 8 == 0)
					Game1.particleEngine.createSparkle(quantaPosition, particleSeed.Next(10) - 5, rand1.NextDouble(), owner.velocity);
				
				spriteBatch.Draw(texture, quantaPosition, rect,
					animations[animation].color,
					rotation, origin, scale,
					animations[animation].spriteEffect, 0f);

				interation++;
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
