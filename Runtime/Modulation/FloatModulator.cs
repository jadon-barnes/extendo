using System;
using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Modulators/Float Modulator")]
	public class FloatModulator : Modulator<float>
	{
		// Set defaults
		public FloatModulator()
		{
			speed    = 1f;
			to       = 1f;
			cutoffTo = 1f;
		}

		protected override float GetModulationValue
		(
			ModulateDelegate method,
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