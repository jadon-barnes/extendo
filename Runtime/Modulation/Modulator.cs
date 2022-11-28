using UnityEngine;

namespace Extendo.Modulation
{
	public abstract class Modulator<T>
	{
		public enum ModulationMethod
		{
			PerlinNoise = 0,
			Linear      = 1,
			Sine        = 2,
			Cosine      = 3,
			Bounce      = 4,
		}

		public         ModulationMethod modulationMethod = ModulationMethod.Sine;
		public         float            strength         = 1f;
		[Space] public T                speed;
		public         T                offset;
		[Space] public T                from;
		public         T                to;
		[Space] public T                cutoffFrom;
		public         T                cutoffTo;

		protected delegate float Modulate(float time, float remapMin, float remapMax, float cutoffMin, float cutoffMax);

		private Modulate sine        = Modulation.Modulate.Sine;
		private Modulate cosine      = Modulation.Modulate.Cosine;
		private Modulate linear      = Modulation.Modulate.Linear;
		private Modulate perlinNoise = Modulation.Modulate.PerlinNoise;
		private Modulate bounce      = Modulation.Modulate.Bounce;

		public T Evaluate(float time)
		{
			switch (modulationMethod)
			{
				case ModulationMethod.Sine:        return GetSine(time);
				case ModulationMethod.Cosine:      return GetCosine(time);
				case ModulationMethod.Linear:      return GetLinear(time);
				case ModulationMethod.PerlinNoise: return GetPerlinNoise(time);
				case ModulationMethod.Bounce:      return GetBounce(time);
				default:                           return default;
			}
		}

		protected abstract T GetValue
		(
			Modulate method,
			float    time,
			T        remapMin,
			T        remapMax,
			T        cutoffMin,
			T        cutoffMax
		);

		protected T GetSine(float time)
		{
			return GetValue
			(
				sine,
				time,
				from,
				to,
				cutoffFrom,
				cutoffTo
			);
		}

		protected T GetCosine(float time)
		{
			return GetValue
			(
				cosine,
				time,
				from,
				to,
				cutoffFrom,
				cutoffTo
			);
		}

		protected T GetLinear(float time)
		{
			return GetValue
			(
				linear,
				time,
				from,
				to,
				cutoffFrom,
				cutoffTo
			);
		}

		protected T GetPerlinNoise(float time)
		{
			return GetValue
			(
				perlinNoise,
				time,
				from,
				to,
				cutoffFrom,
				cutoffTo
			);
		}

		protected T GetBounce(float time)
		{
			return GetValue
			(
				bounce,
				time,
				from,
				to,
				cutoffFrom,
				cutoffTo
			);
		}
	}
}