using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Modulation/Vector3 Modulation Composer")]
	public class Vector3ModulationComposer : ModulationComposer<Vector3Modulation, Vector3>
	{
		public override Vector3 GetSumOfModulations(float time)
		{
			Vector3 sum = Vector3.zero;

			foreach (var modulation in modulations)
			{
				if (!modulation.enable)
					continue;

				modulation.Evaluate(time);

				sum += modulation.Value;
			}

			return sum * strength;
		}
	}
}