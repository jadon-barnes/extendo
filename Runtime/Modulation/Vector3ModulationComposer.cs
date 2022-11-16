using UnityEngine;

namespace Extendo.Modulation
{
	public class Vector3ModulationComposer : ModulationComposer<Vector3Modulation, Vector3>
	{
		public override Vector3 UpdateModulations(float time)
		{
			Vector3 total = Vector3.zero;

			foreach (var modulation in modulations)
			{
				if (!modulation.enable)
					continue;

				modulation.UpdateModulation(time);

				total += modulation.Result;
			}

			return total;
		}
	}
}