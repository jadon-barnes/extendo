using UnityEngine;

namespace Extendo.Modulation.Composition
{
	[AddComponentMenu("Extendo/Modulation/Composition/Modulation Composer Vector2")]
	public class ModulationComposerVector2 : ModulationComposer<ModulatorVector2, Vector2>
	{
		public override Vector2 GetSumOfModulations()
		{
			Vector2 sum = Vector2.zero;

			foreach (ModulatorVector2 modulation in modulations)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}
	}
}