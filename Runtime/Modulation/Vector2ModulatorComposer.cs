using UnityEngine;

namespace Extendo.Modulation
{
	public class Vector2ModulatorComposer : ModulatorComposer<Vector2Modulator, Vector2>
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