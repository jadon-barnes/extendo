using Extendo.Utilities;
using UnityEngine;

namespace Extendo.Oscillation
{
	public static class Oscillate
	{
		// Oscillate Sine
		public static float OscillateSine(float time, float remapMin, float remapMax)
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

		public static float OscillateSine(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax)
		{
			return Mathf.Clamp(OscillateSine(time, remapMin, remapMax), cutoffMin, cutoffMax);
		}

		public static float OscillateSine(float time, Vector2 remap)
		{
			return OscillateSine(time, remap.x, remap.y);
		}

		public static float OscillateSine(float time, Vector2 remap, Vector2 cutoff)
		{
			return OscillateSine(time, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		// Oscillate Cosine

		public static float OscillateCosine(float time, float remapMin, float remapMax)
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

		public static float OscillateCosine(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax)
		{
			return Mathf.Clamp(OscillateCosine(time, remapMin, remapMax), cutoffMin, cutoffMax);
		}

		public static float OscillateCosine(float time, Vector2 remap)
		{
			return OscillateCosine(time, remap.x, remap.y);
		}

		public static float OscillateCosine(float time, Vector2 remap, Vector2 cutoff)
		{
			return OscillateCosine(time, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		// Oscillate Linear

		public static float OscillateLinear(float time, float remapMin, float remapMax)
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

		public static float OscillateLinear(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax)
		{
			return Mathf.Clamp
			(
				OscillateLinear(time, remapMin, remapMax),
				cutoffMin,
				cutoffMax
			);
		}

		public static float OscillateLinear(float time, Vector2 remap)
		{
			return OscillateLinear(time, remap.x, remap.y);
		}

		public static float OscillateLinear(float time, Vector2 remap, Vector2 cutoff)
		{
			return OscillateLinear(time, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		// Oscillate Bounce

		public static float OscillateBounce(float time, float remapMin, float remapMax)
		{
			return Math.Remap
			(
				Mathf.Abs(Mathf.Sin(time)),
				0f,
				1f,
				remapMin,
				remapMax
			);
		}

		public static float OscillateBounce(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax)
		{
			return Mathf.Clamp(OscillateBounce(time, remapMin, remapMax), cutoffMin, cutoffMax);
		}

		public static float OscillateBounce(float time, Vector2 remap)
		{
			return OscillateBounce(time, remap.x, remap.y);
		}

		public static float OscillateBounce(float time, Vector2 remap, Vector2 cutoff)
		{
			return OscillateBounce(time, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		// Oscillate Perlin Noise

		public static float OscillatePerlinNoise(float time, float remapMin, float remapMax)
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

		public static float OscillatePerlinNoise(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax)
		{
			return Mathf.Clamp
			(
				OscillatePerlinNoise(time, remapMin, remapMax),
				cutoffMin,
				cutoffMax
			);
		}

		public static float OscillatePerlinNoise(float time, Vector2 remap)
		{
			return OscillatePerlinNoise(time, remap.x, remap.y);
		}

		public static float OscillatePerlinNoise(float time, Vector2 remap, Vector2 cutoff)
		{
			return OscillatePerlinNoise(time, remap.x, remap.y, cutoff.x, cutoff.y);
		}
	}
}