using System;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Utilities
{
	public static class Math
	{
		public static float Remap(this float value, float aMin, float aMax, float bMin, float bMax)
		{
			return Mathf.Lerp(bMin, bMax, Mathf.InverseLerp(aMin, aMax, value));
		}

		public static float Remap(this float value, Vector2 from, Vector2 to)
		{
			return Mathf.Lerp(to.x, to.y, Mathf.InverseLerp(from.x, from.y, value));
		}

		public static float Distance(this float from, float to)
		{
			return Mathf.Abs(to - from);
		}

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
			return new Vector3
			(
				value.x.SnapToGrid(gridScale),
				value.y.SnapToGrid(gridScale),
				value.z.SnapToGrid(gridScale)
			);
		}

		public static Vector3 SnapToGrid(this Vector3 value, Vector3 gridScale)
		{
			return new Vector3
			(
				value.x.SnapToGrid(gridScale.x),
				value.y.SnapToGrid(gridScale.y),
				value.z.SnapToGrid(gridScale.z)
			);
		}

		public static Vector2 Shortest(params Vector2[] vectors)
		{
			Vector2 result = Vector2.positiveInfinity;

			foreach (var vector in vectors)
			{
				if (vector.magnitude < result.magnitude)
					result = vector;
			}

			return result;
		}

		public static Vector3 Shortest(params Vector3[] vectors)
		{
			Vector3 result = Vector3.positiveInfinity;

			foreach (var vector in vectors)
			{
				if (vector.magnitude < result.magnitude)
					result = vector;
			}

			return result;
		}

		public static Vector2 Longest(params Vector2[] vectors)
		{
			Vector2 result = Vector2.zero;

			foreach (var vector in vectors)
			{
				if (vector.magnitude > result.magnitude)
					result = vector;
			}

			return result;
		}

		public static Vector3 Longest(params Vector3[] vectors)
		{
			Vector3 result = Vector3.zero;

			foreach (var vector in vectors)
			{
				if (vector.magnitude > result.magnitude)
					result = vector;
			}

			return result;
		}

		public static float Damp
		(
			float current,
			float target,
			float damping,
			float deltaTime,
			float ft = 1.0f / 60.0f
		)
		{
			return Mathf.Lerp
			(
				current,
				target,
				1.0f
				- Mathf.Pow
				(
					1f / (1f - ft * damping),
					-deltaTime / ft
				)
			);
		}

		/// <summary>
		/// Smooth interpolation that is independent from frame rate.
		/// </summary>
		public static float Damp(float current, float target, float smoothTime)
		{
			return Mathf.Lerp(current, target, 1.0f - Mathf.Exp(-smoothTime * Time.deltaTime));
		}

		public static float Spring(float from, float to, ref float velocity, float tension = 200f, float damp = 5f, float maxVelocity = 100f)
		{
			damp = Mathf.Max(0f, damp) * Time.deltaTime;

			var difference = (from - to) * Time.deltaTime;

			var force = (-tension * difference) * Time.deltaTime;

			velocity += force;
			velocity *= Mathf.Max(0f, 1f - damp);
			velocity =  Mathf.Min(velocity, maxVelocity);

			return from + velocity;
		}

		// TODO: Test this method
		public static void SpringRotation(this Rigidbody rigidbody, float strength, float dampening, Vector3 direction, Vector3 worldDirection)
		{
			var springTorque = strength * Vector3.Cross(direction, worldDirection);
			var dampTorque = dampening * -rigidbody.angularVelocity;
			rigidbody.AddTorque(springTorque + dampTorque, ForceMode.Acceleration);
		}

		// TODO: Test this method
		public static float PhysicsSpringForce(float position, float target, float strength, float velocity, float dampening)
		{
			return ((target - position) * strength) - (velocity * dampening);
		}
	}
}