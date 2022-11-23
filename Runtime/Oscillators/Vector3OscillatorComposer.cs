using UnityEngine;

namespace Extendo.Oscillators
{
	public class Vector3OscillatorComposer : OscillatorComposer<Vector3Oscillator, Vector3>
	{
		public override Vector3 GetSumOfModulations()
		{
			Vector3 sum = Vector3.zero;

			foreach (var modulation in modulations)
			{
				sum += modulation.Evaluate(time);
			}

			return sum * strength;
		}
	}
}