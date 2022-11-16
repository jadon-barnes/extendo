using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Utilities
{
	public static class Math
	{
		public static float Remap(this float value, float fromA, float toA, float fromB, float toB)
		{
			float value01 = Mathf.InverseLerp(fromA, toA, value);
			return Mathf.Lerp(fromB, toB, value01);
		}

		public static float Remap(this float value, Vector2 from, Vector2 to)
		{
			float value01 = Mathf.InverseLerp(from.x, from.y, value);
			return Mathf.Lerp(to.x, to.y, value01);
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

		private delegate float ModulateWaveFormula(float t);

		private static float ModulateWave
		(
			ModulateWaveFormula modulationFormula,
			float time,
			float seed,
			Vector2 remap,
			Vector2 cutoff
		)
		{
			var formula = time + seed;
			var result = modulationFormula(formula);
			result = Math.Remap(result, new (-1f, 1f), remap);
			return Mathf.Clamp(result, cutoff.x, cutoff.y);
		}


		public static float ModulateSine(float time, float seed, Vector2 remap, Vector2 cutoff)
		{
			return ModulateWave(Mathf.Sin, time, seed, remap, cutoff);
		}

		public static float ModulateCosine(float time, float seed, Vector2 remap, Vector2 cutoff)
		{
			return ModulateWave(Mathf.Cos, time, seed, remap, cutoff);
		}

		public static float ModulateLinear(float time, float seed, Vector2 remap, Vector2 cutoff)
		{
			var formula = time + seed;
			var result = Mathf.PingPong(formula, 1f);
			result = Math.Remap(result, new (0f, 1f), remap);
			return Mathf.Clamp(result, cutoff.x, cutoff.y);
		}

		public static float ModulatePerlinNoise(float time, float seed, Vector2 remap, Vector2 cutoff)
		{
			var formula = time + seed;
			var result = Mathf.Clamp01(Mathf.PerlinNoise(formula, formula));
			result = Remap(result, new (0f, 1f), remap);
			return Mathf.Clamp(result, cutoff.x, cutoff.y);
		}
	}
}