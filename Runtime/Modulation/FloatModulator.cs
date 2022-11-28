using System;

namespace Extendo.Modulation
{
	[Serializable]
	public class FloatModulator : Modulator<float>
	{
		// Set defaults
		public FloatModulator()
		{
			speed    = 1f;
			to       = 1f;
			cutoffTo = 1f;
		}

		protected override float GetValue
		(
			Modulate method,
			float    time,
			float    remapMin,
			float    remapMax,
			float    cutoffMin,
			float    cutoffMax
		)
		{
			float timeValue = (time + offset) * speed;
			return method(timeValue, remapMin, remapMax, cutoffMin, cutoffMax) * strength;
		}
	}
}