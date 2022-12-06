using UnityEngine;

namespace Extendo.Modulation.Composition
{
	[AddComponentMenu("Extendo/Modulation/Composition/Vector2 Modulation Composer")]
	public class Vector2ModulationComposer : ModulationComposer<Vector2Modulator, Vector2>
	{
		public override Vector2 GetSumOfModulations()
		{
			Vector2 sum = Vector2.zero;

			foreach (Vector2Modulator modulation in modulations)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}
	}
}