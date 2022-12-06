using System;
using UnityEngine;

namespace Extendo.Modulation.Composition
{
	[Serializable]
	public class FloatModulator : Modulator<float>
	{
		// Set defaults
		public FloatModulator()
		{
			to       = 1f;
			cutoffTo = 1f;
		}

		protected override float CutoffFromInfinity => Mathf.NegativeInfinity;
		protected override float CutoffToInfinity   => Mathf.Infinity;

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