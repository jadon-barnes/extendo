using Extendo.CustomUpdates;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Modulation
{
	public abstract class ModulationComposer<TModulation, TOut> : TimeBehaviour where TModulation : new()
	{
		protected ModulationComposer()
		{
			duration = -1f;
		}

		public float            strength    = 1f;
		public TModulation[]    modulations = new[] { new TModulation() };
		public TOut             Result { get; protected set; }
		public UnityEvent<TOut> onUpdate;

		public override void ManualUpdate()
		{
			Result = GetSumOfModulations(time);
			onUpdate.Invoke(Result);
			base.ManualUpdate();
		}

		public abstract TOut GetSumOfModulations(float time);
	}
}