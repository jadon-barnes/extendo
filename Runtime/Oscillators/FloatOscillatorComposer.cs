using UnityEngine;

namespace Extendo.Oscillators
{
	[AddComponentMenu("Extendo/Modulators/Float Modulator Composer")]
	public class FloatOscillatorComposer : OscillatorComposer<float>
	{
		public override float GetSumOfModulations()
		{
			float sum = 0f;

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