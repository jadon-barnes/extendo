using UnityEngine;
using Math = Extendo.Utilities.Math;

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

		public T                Result { get; private set; }
		public bool             enable           = true;
		public ModulationMethod modulationMethod = ModulationMethod.Sine;
		[Header("Timing")]
		public T speed;
		public T offset;
		public T seed;
		[Header("Remap")]
		public T remapMin;
		public T remapMax;
		[Header("Cutoff")]
		public T cutoffMin;
		public T cutoffMax;

		protected delegate float ModulateDelegate(float time, float seed, Vector2 remap, Vector2 cutoff);

		protected abstract T GetModulationValue(ModulateDelegate method, float time, T seed, T remapMin, T remapMax, T cutoffMin, T cutoffMax);

		public void Evaluate(float time)
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
				default:
					Result = default;
					break;
			}
		}

		protected T GetSine(float time)
		{
			return GetModulationValue(Math.ModulateSine, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected T GetCosine(float time)
		{
			return GetModulationValue(Math.ModulateCosine, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected T GetLinear(float time)
		{
			return GetModulationValue(Math.ModulateLinear, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected T GetPerlinNoise(float time)
		{
			return GetModulationValue(Math.ModulatePerlinNoise, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected T GetBounce(float time)
		{
			return GetModulationValue(Math.ModulateBounce, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}
	}
}