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

		public    T                Result { get; private set; }
		public    ModulationMethod modulationMethod = ModulationMethod.Sine;
		public    bool             resetOnDisable;
		protected float            time;

		public UnityEvent<T> onUpdate;

		protected override void OnDisable()
		{
			base.OnDisable();

			if (resetOnDisable)
				time = 0f;
		}

		protected override void OnUpdate()
		{
			UpdateModulation();
		}

		public virtual void UpdateModulation()
		{
			time += Time.deltaTime;

			switch (modulationMethod)
			{
				case ModulationMethod.Sine:
					Result = GetSine();
					break;
				case ModulationMethod.Cosine:
					Result = GetCosine();
					break;
				case ModulationMethod.Linear:
					Result = GetLinear();
					break;
				case ModulationMethod.PerlinNoise:
					Result = GetPerlinNoise();
					break;
				default: break;
			}
			
			onUpdate.Invoke(Result);
		}

		protected abstract T GetSine();
		protected abstract T GetCosine();
		protected abstract T GetLinear();
		protected abstract T GetPerlinNoise();
	}
}