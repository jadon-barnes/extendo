using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Modulators/Float Modulator Composer")]
	public class FloatModulatorComposer : ModulatorComposer<float>
	{
		public override float GetSumOfModulations()
		{
			float sum = 0f;

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