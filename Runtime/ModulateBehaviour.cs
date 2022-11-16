using Extendo.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo
{
	public abstract class ModulateBehaviour<T> : CustomUpdateBehaviour
	{
		public enum ModulationMethod
		{
			PerlinNoise = 0,
			Linear      = 1,
			Sine        = 2,
			Cosine      = 3,
			Bounce      = 4,
		}

		public                   T                Result { get; private set; }
		public                   ModulationMethod modulationMethod = ModulationMethod.Sine;
		public                   bool             resetTimeOnDisable;
		[HideInInspector] public float            time;
		public                   UnityEvent<T>    onUpdate;

		protected abstract T GetValueFromTime(float time);

		protected delegate float ModulateDelegate(float time, float seed, Vector2 remap, Vector2 cutoff);

		protected abstract T Modulate(ModulateDelegate method, float time, T seed, T remapMin, T remapMax, T cutoffMin, T cutoffMax);

		protected override void OnDisable()
		{
			base.OnDisable();

			if (resetTimeOnDisable)
				time = 0f;
		}

		protected override void OnUpdate()
		{
			time += Time.deltaTime;
			UpdateModulation(time);
		}

		public virtual void UpdateModulation(float time)
		{
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

			onUpdate.Invoke(Result);
		}

		protected abstract T GetSine(float time);
		protected abstract T GetCosine(float time);
		protected abstract T GetLinear(float time);
		protected abstract T GetPerlinNoise(float time);

		protected abstract T GetBounce(float time);
	}
}