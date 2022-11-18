using Extendo.CustomUpdates;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Modulation
{
	public abstract class ModulationComposer<TModulation, TOut> : CustomUpdateBehaviour where TModulation : new()
	{
		public                   bool             resetTimeOnDisable;
		[HideInInspector] public float            time;
		public                   float            strength    = 1f;
		public                   TModulation[]    modulations = new[] { new TModulation() };
		public                   TOut             Result { get; protected set; }
		public                   UnityEvent<TOut> onUpdate;

		protected override void OnDisable()
		{
			base.OnDisable();

			if (resetTimeOnDisable)
				Reset();
		}

		protected override void OnUpdate()
		{
			UpdateSumOfModulations();
		}

		public void Reset()
		{
			time = 0f;
		}

		public void UpdateSumOfModulations()
		{
			time   += Time.deltaTime;
			Result =  GetSumOfModulations(time);
			onUpdate.Invoke(Result);
		}

		public abstract TOut GetSumOfModulations(float time);
	}
}