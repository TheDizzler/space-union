using System;
using Microsoft.Xna.Framework;


namespace SpaceUnion.Tools {
	/// <summary>
	/// A 2d ray.
	/// @Written by Tristan.
	/// </summary>
	class Ray2 {

		private Vector2 direction;
		private Vector2 position;

		/// <summary>
		/// Distance to hitbox bounding box edge
		/// </summary>
		public float t0x = Int32.MinValue,
			t0y = Int32.MinValue,
			t1x = Int32.MinValue,
			t1y = Int32.MinValue;
		/// <summary>
		/// Ray is completely horizontal (or close enough).
		/// </summary>
		private bool horizontal = false;
		/// <summary>
		/// Ray is completely vertical (or close enough).
		/// </summary>
		private bool vertical = false;

		private bool lastCheckHit;
		private float closest;

		/// <summary>
		/// Tolerance for division, i.e. so close to zero we probably shouldn't divide with
		/// anything smaller than this.
		/// </summary>
		public const float EPSILON = 1e-6f;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="position">Start location of ray</param>
		/// <param name="direction">Direction or end point of ray</param>
		public Ray2(Vector2 position, Vector2 direction) {

			this.position = position;
			this.direction = direction;
			// Let's avoid divide by zero errors, shall we?
			if (Math.Abs(direction.X) < EPSILON)
				vertical = true;
			if (Math.Abs(direction.Y) < EPSILON)
				horizontal = true;
		}

		/// <summary>
		/// Checks if the ray intersects the bounding box.
		/// Records distance to target which can be received by calling
		/// getDistance();
		/// </summary>
		/// <param name="box"></param>
		/// <returns></returns>
		public bool intersects(HitBox box) {

			float bx0 = box.position.X;
			float by0 = box.position.Y;
			float bx1 = box.position.X + box.width;
			float by1 = box.position.Y + box.height;

			if (vertical) {
				t0x = Int32.MinValue;
				t1x = Int32.MinValue;
				t0y = (by0 - position.Y) / direction.Y;
				t1y = (by1 - position.Y) / direction.Y;
				// if ray originated between the x bounds of the box
				if (bx0 < position.X && position.X < bx1
					&& 0 <= t0y && 0 <= t1y) {
					getDist();
					return (lastCheckHit = true); // hit!
				} else
					return (lastCheckHit = false); // miss!
			}

			if (horizontal) {
				t0y = Int32.MinValue;
				t1y = Int32.MinValue;
				t0x = (bx0 - position.X) / direction.X;
				t1x = (bx1 - position.X) / direction.X;
				// if ray originated between the y bounds of the box
				if (by0 < position.Y && position.Y < by1
					&& 0 <= t0x && 0 <= t1x) {
					getDist();
					return (lastCheckHit = true);
				} else
					return (lastCheckHit = false);
			}


			t0x = (bx0 - position.X) / direction.X;
			t0y = (by0 - position.Y) / direction.Y;
			t1x = (bx1 - position.X) / direction.X;
			t1y = (by1 - position.Y) / direction.Y;


			if (t0x > t1x)
				swap(ref t0x, ref t1x);
			if (t0y > t1y)
				swap(ref t0y, ref t1y);

			if (t0x > t1y || t0y > t1x)
				return (lastCheckHit = false);


			if (getDist() >= 0) // check if hit is not a negitive direction hit
				return (lastCheckHit = true);

			return (lastCheckHit = false);

		}

		/// <summary>
		/// As as intersects(HitBox box) but checks if hit is within
		/// direction.Length()
		/// </summary>
		/// <param name="box"></param>
		/// <returns></returns>
		public bool intersectsToRange(HitBox box) {

			float bx0 = box.position.X;
			float by0 = box.position.Y;
			float bx1 = box.position.X + box.width;
			float by1 = box.position.Y + box.height;

			if (vertical) {
				t0x = Int32.MinValue;
				t1x = Int32.MinValue;
				t0y = (by0 - position.Y) / direction.Y;
				t1y = (by1 - position.Y) / direction.Y;
				// if ray originated between the x bounds of the box
				if (bx0 < position.X && position.X < bx1
					&& 0 <= t0y && 0 <= t1y && getDist() <= direction.Length()) {
					return (lastCheckHit = true); // hit!
				} else
					return (lastCheckHit = false); // miss!
			}

			if (horizontal) {
				t0y = Int32.MinValue;
				t1y = Int32.MinValue;
				t0x = (bx0 - position.X) / direction.X;
				t1x = (bx1 - position.X) / direction.X;
				// if ray originated between the y bounds of the box
				if (by0 < position.Y && position.Y < by1
					&& 0 <= t0x && 0 <= t1x && getDist() <= direction.Length()) {
					return (lastCheckHit = true);
				} else
					return (lastCheckHit = false);
			}


			t0x = (bx0 - position.X) / direction.X;
			t0y = (by0 - position.Y) / direction.Y;
			t1x = (bx1 - position.X) / direction.X;
			t1y = (by1 - position.Y) / direction.Y;


			if (t0x > t1x)
				swap(ref t0x, ref t1x);
			if (t0y > t1y)
				swap(ref t0y, ref t1y);

			if (t0x > t1y || t0y > t1x)
				return (lastCheckHit = false);

			float dist = getDist();
			if (dist >= 0 && dist <= direction.Length()) // check if hit is not a negitive direction hit
				return (lastCheckHit = true);

			return (lastCheckHit = false);

		}


		/// <summary>
		/// Retrieves the smallest, non-negative distance to the closest edge of the last checked hitbox.
		/// Note that this value doesn't mean much if intersects() returned false and will probably
		/// cause strange behaviour.
		/// </summary>
		/// <returns>The closest distance equal to or larger than 0</returns>
		private float getDist() {


			closest = t1x;
			if (t1y >= 0)
				closest = Math.Min(closest, t1y);
			if (t0x >= 0)
				closest = Math.Min(closest, t0x);
			if (t0y >= 0)
				closest = Math.Min(closest, t0y);


			return closest;

		}

		/// <summary>
		/// Get the distance to the last checked hitbox.
		/// Note that this value doesn't mean much if intersects() returned false and will probably
		/// cause strange behaviour.
		/// </summary>
		/// <returns></returns>
		public float getDistance() {

			return closest;
		}

		private static void swap(ref float x, ref float y) {
			float temp = x;
			x = y;
			y = temp;
		}


		

	}
}
