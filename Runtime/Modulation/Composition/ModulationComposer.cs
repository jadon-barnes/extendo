using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Modulation.Composition
{
	public abstract class ModulationComposer<TMod, TOut> : ModulationBehaviour
	{
		public float            time;
		public float            strength    = 1f;
		public TMod[]           modulations = new TMod[1];
		public TOut             Value { get; protected set; }
		public UnityEvent<TOut> onUpdate;

		public abstract TOut GetSumOfModulations();

		protected override void ManualUpdate()
		{
			Value =  GetSumOfModulations();
			time  += Time.deltaTime;
			onUpdate.Invoke(Value);
		}
	}
}