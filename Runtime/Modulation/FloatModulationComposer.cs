using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Modulation/Float Modulation Composer")]
	public class FloatModulationComposer : ModulationComposer<FloatModulator, float>
	{
		public override float GetSumOfModulations()
		{
			float sum = 0f;

			foreach (var modulation in modulations)
			{
				sum += modulation.Evaluate(time);
			}

			return sum * strength;
		}
	}
}