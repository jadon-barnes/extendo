using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Modulation/Float Modulation Composer")]
	public class FloatModulationComposer : ModulationComposer<FloatModulation, float>
	{
		public override float GetSumOfModulations(float time)
		{
			float sum = 0f;

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