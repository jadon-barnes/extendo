using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Modulation/Vector2 Modulation Composer")]
	public class Vector2ModulationComposer : ModulationComposer<Vector2Modulation, Vector2>
	{
		public override Vector2 GetSumOfModulations(float time)
		{
			Vector2 sum = Vector2.zero;

			foreach (var modulation in modulations)
			{
				if (!modulation.enable)
					continue;

				modulation.Evaluate(time);

				sum += modulation.Result;
			}

			return sum;
		}
	}
}