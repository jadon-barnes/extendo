using UnityEngine;

namespace Extendo.Utilities
{
	public static class Math
	{
		/// <summary>
		/// Remaps an input value from A to B.
		/// </summary>
		public static float Remap(this float value, float aMin, float aMax, float bMin, float bMax)
		{
			return Mathf.Lerp(bMin, bMax, Mathf.InverseLerp(aMin, aMax, value));
		}

		/// <summary>
		/// Remaps an input value from A to B.
		/// </summary>
		public static float Remap(this float value, Vector2 from, Vector2 to)
		{
			return Mathf.Lerp(to.x, to.y, Mathf.InverseLerp(from.x, from.y, value));
		}

		/// <summary>
		/// Calculates the distance between two values.
		/// </summary>
		public static float Distance(this float from, float to)
		{
			return Mathf.Abs(to - from);
		}

		/// <summary>
		/// Gets the direction from A to B.
		/// </summary>
		public static Vector2 Direction(this Vector2 from, Vector2 to)
		{
			return to - from;
		}

		/// <summary>
		/// Gets the direction from A to B.
		/// </summary>
		public static Vector3 Direction(this Vector3 from, Vector3 to)
		{
			return to - from;
		}

		public static float SnapToGrid(this float value, float gridScale)
		{
			return Mathf.Round(value / gridScale) * gridScale;
		}

		public static Vector3 SnapToGrid(this Vector3 value, float gridScale)
		{
			return new(value.x.SnapToGrid(gridScale), value.y.SnapToGrid(gridScale), value.z.SnapToGrid(gridScale));
		}

		public static Vector3 SnapToGrid(this Vector3 value, Vector3 gridScale)
		{
			return new(value.x.SnapToGrid(gridScale.x), value.y.SnapToGrid(gridScale.y), value.z.SnapToGrid(gridScale.z));
		}

		public static Vector2 Shortest(params Vector2[] vectors)
		{
			Vector2 result = Vector2.positiveInfinity;

			foreach (Vector2 vector in vectors)
				if (vector.magnitude < result.magnitude)
					result = vector;

			return result;
		}

		public static Vector3 Shortest(params Vector3[] vectors)
		{
			Vector3 result = Vector3.positiveInfinity;

			foreach (Vector3 vector in vectors)
				if (vector.magnitude < result.magnitude)
					result = vector;

			return result;
		}

		public static Vector2 Longest(params Vector2[] vectors)
		{
			Vector2 result = Vector2.zero;

			foreach (Vector2 vector in vectors)
				if (vector.magnitude > result.magnitude)
					result = vector;

			return result;
		}

		public static Vector3 Longest(params Vector3[] vectors)
		{
			Vector3 result = Vector3.zero;

			foreach (Vector3 vector in vectors)
				if (vector.magnitude > result.magnitude)
					result = vector;

			return result;
		}

		/// <summary>
		/// Lerp-based smooth interpolation that is not affected by frame rate.
		/// </summary>
		public static float Damp(float current, float target, float smoothTime)
		{
			return Mathf.Lerp(current, target, 1f - Mathf.Exp(smoothTime * -Time.deltaTime));
		}

		/// <summary>
		/// Lerp-based smooth interpolation that is not affected by frame rate.
		/// </summary>
		public static float DampAngle(float current, float target, float smoothTime)
		{
			return Mathf.LerpAngle(current, target, 1f - Mathf.Exp(smoothTime * -Time.deltaTime));
		}

		/// <summary>
		/// Lerp-based smooth interpolation that is not affected by frame rate.
		/// </summary>
		public static Vector2 Damp(Vector2 current, Vector2 target, float smoothTime)
		{
			return new(Damp(current.x, target.x, smoothTime), Damp(current.y, target.y, smoothTime));
		}

		/// <summary>
		/// Lerp-based smooth interpolation that is not affected by frame rate.
		/// </summary>
		public static Vector3 Damp(Vector3 current, Vector3 target, float smoothTime)
		{
			return new(Damp(current.x, target.x, smoothTime), Damp(current.y, target.y, smoothTime), Damp(current.z, target.z, smoothTime));
		}

		/// <summary>
		/// Creates a spring effect from the input value.
		/// </summary>
		public static float Spring
		(
			float     from,
			float     to,
			ref float velocity,
			float     strength    = 200f,
			float     damp        = 5f,
			float     maxVelocity = 100f
		)
		{
			damp = Mathf.Max(0f, damp) * Time.deltaTime;
			float direction = (to - from) * Time.deltaTime;
			float force     = strength * direction * Time.deltaTime;

			velocity += force;
			velocity *= Mathf.Max(0f, 1f - damp);
			velocity =  Mathf.Min(velocity, maxVelocity);

			return from + velocity;
		}

		/// <summary>
		/// Creates a spring effect from the input value.
		/// </summary>
		public static Vector2 Spring(Vector2 from, Vector2 to, ref Vector2 velocity, float strength = 200f, float damp = 5f)
		{
			damp = Mathf.Max(0f, damp) * Time.deltaTime;
			Vector2 direction = (to - from) * Time.deltaTime;
			Vector2 force     = direction * (strength * Time.deltaTime);

			velocity += force;
			velocity *= Mathf.Max(0f, 1f - damp);

			return from + velocity;
		}

		/// <summary>
		/// Creates a spring effect from the input value.
		/// </summary>
		public static Vector3 Spring(Vector3 from, Vector3 to, ref Vector3 velocity, float strength = 200f, float damp = 5f)
		{
			damp = Mathf.Max(0f, damp) * Time.deltaTime;
			Vector3 direction = (to - from) * Time.deltaTime;
			Vector3 force     = direction * (strength * Time.deltaTime);

			velocity += force;
			velocity *= Mathf.Max(0f, 1f - damp);

			return from + velocity;
		}

		/// <summary>
		/// Creates a spring effect for rotation values.
		/// </summary>
		public static void SpringRotation(this Rigidbody rigidbody, float strength, float dampening, Vector3 direction, Vector3 worldDirection)
		{
			Vector3 springTorque = strength * Vector3.Cross(direction, worldDirection);
			Vector3 dampTorque   = Mathf.Max(0, dampening) * -rigidbody.angularVelocity;
			rigidbody.AddTorque(springTorque + dampTorque, ForceMode.Acceleration);
		}

		/// <summary>
		/// Calculates the force needed to create a spring effect.
		/// </summary>
		/// <returns>Force to be applied for spring effect</returns>
		public static float SpringForce(float position, float target, float velocity, float strength = 200f, float damp = 5f)
		{
			return (target - position) * strength - velocity * Mathf.Max(0, damp);
		}

		/// <summary>
		/// Calculates the force needed to create a spring effect.
		/// </summary>
		/// <returns>Force to be applied for spring effect</returns>
		public static Vector2 SpringForce(Vector2 position, Vector2 target, Vector2 velocity, float strength = 200f, float damp = 5f)
		{
			return (target - position) * strength - velocity * Mathf.Max(0, damp);
		}

		/// <summary>
		/// Calculates the force needed to create a spring effect.
		/// </summary>
		/// <returns>Force to be applied for spring effect</returns>
		public static Vector3 SpringForce(Vector3 position, Vector3 target, Vector3 velocity, float strength = 200f, float damp = 5f)
		{
			return (target - position) * strength - velocity * Mathf.Max(0, damp);
		}

		/// <summary>
		/// Calculates the force needed to create a spring effect.
		/// </summary>
		/// <returns>Force to be applied for spring effect</returns>
		public static Vector3 SpringForce(this Rigidbody rigidbody, Vector3 target, float strength = 200f, float damp = 5f)
		{
			return SpringForce(rigidbody.position, target, rigidbody.velocity, strength, damp);
		}

		/// <summary>
		/// Rotates a vector point around a pivot point.
		/// </summary>
		public static Vector3 RotateAround(this Vector3 point, Vector3 pivot, Vector3 axis, float angle)
		{
			Vector3 direction = point - pivot;
			direction = Quaternion.Euler(axis * angle) * direction;
			point     = direction + pivot;
			return point;
		}

		/// <summary>
		/// Rotates a vector point around a pivot point.
		/// </summary>
		public static Vector3 RotateAround(this Vector3 point, Vector3 pivot, Vector3 axis, float angle, float maxRadius)
		{
			Vector3 direction = point - pivot;
			direction = Vector3.ClampMagnitude(direction, maxRadius);
			direction = Quaternion.Euler(axis * angle) * direction;
			point     = direction + pivot;
			return point;
		}
	}
}