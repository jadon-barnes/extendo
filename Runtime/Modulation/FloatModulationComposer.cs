using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Float Modulation Composer")]
	public class FloatModulationComposer : ModulationComposer<FloatModulation, float>
	{
		public override float UpdateModulations(float time)
		{
			float total = 0f;

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