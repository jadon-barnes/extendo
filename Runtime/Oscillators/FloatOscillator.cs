using UnityEngine;

namespace Extendo.Oscillators
{
	public class FloatOscillator : Oscillator<float>
	{
		// Set defaults
		public FloatOscillator()
		{
			speed    = 1f;
			to       = 1f;
			cutoffTo = 1f;
		}

		protected override float GetOscillationValue
		(
			OscillationDelegate method,
			float time,
			float remapMin,
			float remapMax,
			float cutoffMin,
			float cutoffMax
		)
		{
			float timeValue = (time + offset) * speed;
			return method(timeValue, new (remapMin, remapMax), new (cutoffMin, cutoffMax)) * strength;
		}
	}
}