using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Modulation
{
	public abstract class ModulatorComposer<T> : MonoBehaviour
	{
		public float          strength         = 1f;
		public Modulator<T>[] modulations      = new Modulator<T>[0];
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
				modulation.StartUpdate();
			}
		}

		public void StopAll()
		{
			foreach (var modulation in modulations)
			{
				modulation.StopUpdate();
			}
		}
	}
}