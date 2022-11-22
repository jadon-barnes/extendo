using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Oscillators
{
	public abstract class OscillatorComposer<T> : MonoBehaviour
	{
		public float          strength         = 1f;
		public Oscillator<T>[] modulations      = new Oscillator<T>[0];
		public T              Value { get; protected set; }
		public UnityEvent<T>  onUpdate;

		private void LateUpdate()
		{
			Value = GetSumOfModulations();
			onUpdate.Invoke(Value);
		}

		public abstract T GetSumOfModulations();

		public void StartAll()
		{
			foreach (var modulation in modulations)
			{
				modulation.Start();
			}
		}

		public void StopAll()
		{
			foreach (var modulation in modulations)
			{
				modulation.Stop();
			}
		}
	}
}