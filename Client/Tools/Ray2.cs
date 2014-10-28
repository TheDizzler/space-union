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
		public float t0x = Int32.MaxValue,
			t0y = Int32.MaxValue,
			t1x = Int32.MaxValue,
			t1y = Int32.MaxValue;
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
				t0x = Int32.MaxValue;
				t1x = Int32.MaxValue;
				t0y = (by0 - position.Y) / direction.Y;
				t1y = (by1 - position.Y) / direction.Y;
				// if ray originated between the x bounds of the box
				if (bx0 < position.X && position.X < bx1
					&& 0 <= t0y && 0 <= t1y) {
						closest = Math.Min(t0y, t1y);
					return (lastCheckHit = true); // hit!
				} else
					return (lastCheckHit = false); // miss!
			}

			if (horizontal) {
				t0y = Int32.MaxValue;
				t1y = Int32.MaxValue;
				t0x = (bx0 - position.X) / direction.X;
				t1x = (bx1 - position.X) / direction.X;
				// if ray originated between the y bounds of the box
				if (by0 < position.Y && position.Y < by1
					&& 0 <= t0x && 0 <= t1x) {
					closest = Math.Min(t0x, t1x);
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

			closest = Math.Max(t0x, t0y);
			if (closest >= 0) // check if hit is not a negitive direction hit
				return (lastCheckHit = true);

			return (lastCheckHit = false);

		}


		//public bool intersects(HitCircle circle) {

		//	return true;
		//}


		/// <summary>
		/// Same as intersects(HitBox box) but checks if hit is within
		/// direction.Length().
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


			if (t0x > t1x) {
				//float temp = t0x;
				//t0x = t1x;
				//t1x = temp;
				swap(ref t0x, ref t1x);
			}
			if (t0y > t1y) {
				//float temp = t0y;
				//t0y = t1y;
				//t1y = temp;
				swap(ref t0y, ref t1y);
			}

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

			float other;
			//closest = t1x;
			//if (t1y >= 0) {
				closest = Math.Min(t1x, t1y);
				other = Math.Max(closest, t1y);
			//}
			if (t0x >= 0)
				closest = Math.Min(closest, t0x);
			if (t0y >= 0)
				closest = Math.Min(closest, t0y);


			return closest;

		}

		/// <summary>
		/// Get the distance (in pixels) to the last checked hitbox.
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


		public float getDistance2() {
			Console.WriteLine("t1x " + t1x + ", t1y" + t1y + ", t0x" + t0x + ", t0y " + t0y);
			return closest;
		}

	}
}
