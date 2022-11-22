using UnityEngine;

namespace Extendo.Oscillators
{
	public class Vector2OscillatorComposer : OscillatorComposer<Vector2>
	{
		public override Vector2 GetSumOfModulations()
		{
			Vector2 sum = Vector2.zero;

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