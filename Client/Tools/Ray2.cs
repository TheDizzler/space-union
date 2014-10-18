using System;
using Microsoft.Xna.Framework;


namespace SpaceUnion.Tools {
	/// <summary>
	/// Adapted from the 3D Ray in XNA.
	/// @Adapted by Tristan.
	/// </summary>
	class Ray2 : IEquatable<Ray2> {

		private Vector2 direction;
		private Vector2 position;

		const float epsilon = 1e-6f;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="position">Start location of ray</param>
		/// <param name="direction">Direction or end point of ray</param>
		public Ray2(Vector2 position, Vector2 direction) {

			this.position = position;
			this.direction = direction;

		}


		public bool Equals(Ray2 other) {
			return this.position.Equals(other.position) && this.direction.Equals(other.direction);
		}

		public override bool Equals(object obj) {
			return (obj is Ray2) && this.Equals((Ray2) obj);
		}

		//public float? intersects(HitBox hitBox) {

		//	float? tMin = null, tMax= null;

		//	if (Math.Abs(direction.X) < epsilon) {
		//		if (position.X < hitBox.position.X || position.X > hitBox.position.X)
		//			return null;
		//	} else {
		//		tMin = (hitBox.


		//}

	}
}
