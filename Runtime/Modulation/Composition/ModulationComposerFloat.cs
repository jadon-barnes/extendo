using UnityEngine;

namespace Extendo.Modulation.Composition
{
	[AddComponentMenu("Extendo/Modulation/Composition/Modulation Composer Float")]
	public class ModulationComposerFloat : ModulationComposer<ModulatorFloat, float>
	{
		public override float GetSumOfModulations()
		{
			var sum = 0f;

			foreach (ModulatorFloat modulation in modulations)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}
	}
}