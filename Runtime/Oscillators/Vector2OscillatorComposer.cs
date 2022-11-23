using UnityEngine;

namespace Extendo.Oscillators
{
	public class Vector2OscillatorComposer : OscillatorComposer<Vector2Oscillator, Vector2>
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