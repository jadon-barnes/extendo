using UnityEngine;

namespace Extendo.Oscillation
{
	public abstract class Oscillator<T>
	{
		public enum OscillationMethod
		{
			PerlinNoise = 0,
			Linear      = 1,
			Sine        = 2,
			Cosine      = 3,
			Bounce      = 4,
		}

		public OscillationMethod oscillationMethod = OscillationMethod.Sine;
		public float             strength          = 1f;
		[Space]
		public T speed;
		public T offset;
		[Space]
		public T from;
		public T to;
		[Space]
		public T cutoffFrom;
		public T cutoffTo;

		protected delegate float Oscillate(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax);

		private Oscillate oscillateSine   = Oscillation.Oscillate.OscillateSine;
		private Oscillate oscillateCosine = Oscillation.Oscillate.OscillateCosine;
		private Oscillate oscillateLinear = Oscillation.Oscillate.OscillateLinear;
		private Oscillate oscillatePerlin = Oscillation.Oscillate.OscillatePerlinNoise;
		private Oscillate oscillateBounce = Oscillation.Oscillate.OscillateBounce;

		public T Evaluate(float time)
		{
			switch (oscillationMethod)
			{
				case OscillationMethod.Sine:        return GetSine(time);
				case OscillationMethod.Cosine:      return GetCosine(time);
				case OscillationMethod.Linear:      return GetLinear(time);
				case OscillationMethod.PerlinNoise: return GetPerlinNoise(time);
				case OscillationMethod.Bounce:      return GetBounce(time);
				default:                            return default;
			}
		}

		protected abstract T GetOscillationValue
		(
			Oscillate method,
			float time,
			T remapMin,
			T remapMax,
			T cutoffMin,
			T cutoffMax
		);

		protected T GetSine(float time)
		{
			return GetOscillationValue(oscillateSine, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetCosine(float time)
		{
			return GetOscillationValue(oscillateCosine, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetLinear(float time)
		{
			return GetOscillationValue(oscillateLinear, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetPerlinNoise(float time)
		{
			return GetOscillationValue(oscillatePerlin, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetBounce(float time)
		{
			return GetOscillationValue(oscillateBounce, time, from, to, cutoffFrom, cutoffTo);
		}
	}
}