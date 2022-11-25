using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Modulation/Vector2 Modulation Composer")]
	public class Vector2ModulationComposer : ModulationComposer<Vector2Modulator, Vector2>
	{
		public override Vector2 GetSumOfModulations()
		{
			Vector2 sum = Vector2.zero;

			foreach (var modulation in modulations)
			{
				sum += modulation.Evaluate(time);
			}

			return sum * strength;
		}
	}
}