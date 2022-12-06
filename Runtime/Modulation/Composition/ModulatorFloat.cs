using System;
using UnityEngine;

namespace Extendo.Modulation.Composition
{
	[Serializable]
	public class ModulatorFloat : Modulator<float>
	{
		// Set defaults
		public ModulatorFloat()
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
			float timeValue = (time + timeOffset) * speed;
			return method(timeValue, remapMin, remapMax, cutoffMin, cutoffMax) * strength;
		}
	}
}