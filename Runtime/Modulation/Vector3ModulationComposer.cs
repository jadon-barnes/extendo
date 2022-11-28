using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Modulation/Vector3 Modulation Composer")]
	public class Vector3ModulationComposer : ModulationComposer<Vector3Modulator, Vector3>
	{
		public override Vector3 GetSumOfModulations()
		{
			Vector3 sum = Vector3.zero;

			foreach (Vector3Modulator modulation in modulations)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}
	}
}