using Extendo.Utilities;
using UnityEngine;

namespace Extendo.Modulation
{
	public static class Modulate
	{
		// Sine
		public static float Sine(float time, float remapMin, float remapMax)
		{
			return Math.Remap
			(
				Mathf.Sin(time),
				-1f,
				1f,
				remapMin,
				remapMax
			);
		}

		public static float Sine(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax)
		{
			return Mathf.Clamp(Sine(time, remapMin, remapMax), cutoffMin, cutoffMax);
		}

		public static float Sine(float time, Vector2 remap)
		{
			return Sine(time, remap.x, remap.y);
		}

		public static float Sine(float time, Vector2 remap, Vector2 cutoff)
		{
			return Sine(time, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		// Cosine

		public static float Cosine(float time, float remapMin, float remapMax)
		{
			return Math.Remap
			(
				Mathf.Cos(time),
				-1f,
				1f,
				remapMin,
				remapMax
			);
		}

		public static float Cosine(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax)
		{
			return Mathf.Clamp(Cosine(time, remapMin, remapMax), cutoffMin, cutoffMax);
		}

		public static float Cosine(float time, Vector2 remap)
		{
			return Cosine(time, remap.x, remap.y);
		}

		public static float Cosine(float time, Vector2 remap, Vector2 cutoff)
		{
			return Cosine(time, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		// Linear

		public static float Linear(float time, float remapMin, float remapMax)
		{
			return Math.Remap
			(
				Mathf.PingPong(time, 1f),
				0f,
				1f,
				remapMin,
				remapMax
			);
		}

		public static float Linear(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax)
		{
			return Mathf.Clamp
			(
				Linear(time, remapMin, remapMax),
				cutoffMin,
				cutoffMax
			);
		}

		public static float Linear(float time, Vector2 remap)
		{
			return Linear(time, remap.x, remap.y);
		}

		public static float Linear(float time, Vector2 remap, Vector2 cutoff)
		{
			return Linear(time, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		// Bounce

		public static float Bounce(float time, float remapMin, float remapMax)
		{
			return Math.Remap
			(
				Mathf.Abs(Mathf.Sin(time) * 0.5f),
				0f,
				1f,
				remapMin,
				remapMax
			);
		}

		public static float Bounce(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax)
		{
			return Mathf.Clamp(Bounce(time, remapMin, remapMax), cutoffMin, cutoffMax);
		}

		public static float Bounce(float time, Vector2 remap)
		{
			return Bounce(time, remap.x, remap.y);
		}

		public static float Bounce(float time, Vector2 remap, Vector2 cutoff)
		{
			return Bounce(time, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		// Perlin Noise

		public static float PerlinNoise(float time, float remapMin, float remapMax)
		{
			return Math.Remap
			(
				Mathf.Clamp01(Mathf.PerlinNoise(time, time)),
				0f,
				1f,
				remapMin,
				remapMax
			);
		}

		public static float PerlinNoise(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax)
		{
			return Mathf.Clamp
			(
				PerlinNoise(time, remapMin, remapMax),
				cutoffMin,
				cutoffMax
			);
		}

		public static float PerlinNoise(float time, Vector2 remap)
		{
			return PerlinNoise(time, remap.x, remap.y);
		}

		public static float PerlinNoise(float time, Vector2 remap, Vector2 cutoff)
		{
			return PerlinNoise(time, remap.x, remap.y, cutoff.x, cutoff.y);
		}
	}
}