using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Modulators/Vector3 Modulator Composer")]
	public class Vector3ModulatorComposer : ModulatorComposer<Vector3>
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