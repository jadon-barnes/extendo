using UnityEngine;

namespace Extendo.Oscillators
{
	public class Vector3OscillatorComposer : OscillatorComposer<Vector3>
	{
		public override Vector3 GetSumOfModulations()
		{
			Vector3 sum = Vector3.zero;

			foreach (var modulation in modulations)
			{
				if (!modulation.enabled)
					continue;

				sum += modulation.Value;
			}

			return sum * strength;
		}
	}
}