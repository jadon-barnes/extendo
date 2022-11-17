using System;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Utilities
{
	public static class Math
	{
		public static float Remap(this float value, float fromA, float toA, float fromB, float toB)
		{
			return Mathf.Lerp(fromB, toB, Mathf.InverseLerp(fromA, toA, value));
		}

		public static float Remap(this float value, Vector2 from, Vector2 to)
		{
			return Mathf.Lerp(to.x, to.y, Mathf.InverseLerp(from.x, from.y, value));
		}

		public static float Distance(this Vector3 a, Vector3 b)
		{
			return Vector3.Distance(Vector3.back, Vector3.down);
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

		public static float ModulateSine(float time, Vector2 remap)
		{
			return Math.Remap
			(
				Mathf.Sin(time),
				new (-1f, 1f),
				remap
			);
		}

		public static float ModulateSine(float time, Vector2 remap, Vector2 cutoff)
		{
			return Mathf.Clamp
			(
				ModulateSine(time, remap),
				cutoff.x,
				cutoff.y
			);
		}

		public static float ModulateCosine(float time, Vector2 remap)
		{
			return Math.Remap
			(
				Mathf.Cos(time),
				new (-1f, 1f),
				remap
			);
		}

		public static float ModulateCosine(float time, Vector2 remap, Vector2 cutoff)
		{
			return Mathf.Clamp
			(
				ModulateCosine(time, remap),
				cutoff.x,
				cutoff.y
			);
		}

		public static float ModulateLinear(float time, Vector2 remap)
		{
			return Math.Remap
			(
				Mathf.PingPong(time, 1f),
				new (0f, 1f),
				remap
			);
		}

		public static float ModulateLinear(float time, Vector2 remap, Vector2 cutoff)
		{
			return Mathf.Clamp
			(
				ModulateLinear(time, remap),
				cutoff.x,
				cutoff.y
			);
		}

		public static float ModulateBounce(float time, Vector2 remap)
		{
			return Remap
			(
				Mathf.Abs(Mathf.Sin(time)),
				new (0f, 1f),
				remap
			);
		}

		public static float ModulateBounce(float time, Vector2 remap, Vector2 cutoff)
		{
			return Mathf.Clamp
			(
				ModulateBounce(time, remap),
				cutoff.x,
				cutoff.y
			);
		}

		public static float ModulatePerlinNoise(float time, Vector2 remap)
		{
			return Math.Remap
			(
				Mathf.Clamp01(Mathf.PerlinNoise(time, time)),
				new (0f, 1f),
				remap
			);
		}

		public static float ModulatePerlinNoise(float time, Vector2 remap, Vector2 cutoff)
		{
			return Mathf.Clamp
			(
				ModulatePerlinNoise(time, remap),
				cutoff.x,
				cutoff.y
			);
		}
	}
}