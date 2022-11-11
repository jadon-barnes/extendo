using UnityEngine;

namespace Extendo.Utilities
{
	public static class Math
	{
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
	}
}