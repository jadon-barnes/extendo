using System;

namespace Extendo.Oscillation
{
	[Serializable]
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
			Oscillate method,
			float time,
			float remapMin,
			float remapMax,
			float cutoffMin,
			float cutoffMax
		)
		{
			float timeValue = (time + offset) * speed;
			return method(timeValue, remapMin, remapMax, cutoffMin, cutoffMax) * strength;
		}
	}
}