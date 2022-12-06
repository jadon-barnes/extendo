using UnityEngine;

namespace Extendo.Modulation.Composition
{
	[AddComponentMenu("Extendo/Modulation/Composition/Modulation Composer Vector3")]
	public class ModulationComposerVector3 : ModulationComposer<ModulatorVector3, Vector3>
	{
		public override Vector3 GetSumOfModulations()
		{
			Vector3 sum = Vector3.zero;

			foreach (ModulatorVector3 modulation in modulations)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}
	}
}