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
		}

		public T                Result { get; private set; }
		public ModulationMethod modulationMethod = ModulationMethod.Sine;
		public bool             resetTimeOnDisable;
		[HideInInspector]
		public float time;

		public UnityEvent<T> onUpdate;

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
				default: break;
			}

			onUpdate.Invoke(Result);
		}

		protected abstract T GetSine(float time);
		protected abstract T GetCosine(float time);
		protected abstract T GetLinear(float time);
		protected abstract T GetPerlinNoise(float time);
	}
}