using UnityEngine;

namespace Extendo.Modulation.Composition
{
	[AddComponentMenu("Extendo/Modulation/Composition/Float Modulation Composer")]
	public class FloatModulationComposer : ModulationComposer<FloatModulator, float>
	{
		public override float GetSumOfModulations()
		{
			var sum = 0f;

			foreach (FloatModulator modulation in modulations)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}
	}
}