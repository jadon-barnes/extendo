using System;
using UnityEngine;

namespace Extendo.Modulation
{
	public abstract class Modulation<T>
	{
		public enum ModulationMethod
		{
			PerlinNoise = 0,
			Linear      = 1,
			Sine        = 2,
			Cosine      = 3,
			Bounce      = 4,
		}

		public bool             enable = true;
		public T                Result { get; private set; }
		public ModulationMethod modulationMethod = ModulationMethod.Sine;

		protected delegate float ModulateDelegate(float time, float seed, Vector2 remap, Vector2 cutoff);

		protected abstract T CalculateModulation(ModulateDelegate method, float time, T seed, T remapMin, T remapMax, T cutoffMin, T cutoffMax);

		public void UpdateModulation(float time)
		{
			if (!enable)
				return;

			switch (modulationMethod)
			{
				case ModulationMethod.Sine:
					Result = GetSine(time);
					break;
				case ModulationMethod.Cosine:
					Result = GetCosine(time);
					break;
				case ModulationMethod.Linear:
					Result = GetLinear(time);
					break;
				case ModulationMethod.PerlinNoise:
					Result = GetPerlinNoise(time);
					break;
				case ModulationMethod.Bounce:
					Result = GetBounce(time);
					break;
				default: break;
			}
		}

		protected abstract T GetSine(float time);
		protected abstract T GetCosine(float time);
		protected abstract T GetLinear(float time);
		protected abstract T GetPerlinNoise(float time);
		protected abstract T GetBounce(float time);
	}
}