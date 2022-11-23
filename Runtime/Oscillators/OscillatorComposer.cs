using Extendo.CustomUpdates;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Oscillators
{
	public abstract class OscillatorComposer<TOsc, TOut> : CustomUpdate
	{
		public float            time;
		public float            strength    = 1f;
		public TOsc[]           modulations = new TOsc[1];
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