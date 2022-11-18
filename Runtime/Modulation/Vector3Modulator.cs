using System;
using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Modulators/Vector3 Modulator")]
	public class Vector3Modulator : Modulator<Vector3>
	{
		public Vector3Modulator()
		{
			speed    = Vector3.one;
			to       = Vector3.one;
			cutoffTo = Vector3.one;
		}

		protected override Vector3 GetModulationValue
		(
			ModulateDelegate method,
			float time,
			Vector3 remapMin,
			Vector3 remapMax,
			Vector3 cutoffMin,
			Vector3 cutoffMax
		)
		{
			Vector3 timeValue = Vector3.Scale((Vector3.one * time) + offset, speed);

			return new Vector3
				(
					method(timeValue.x, new (remapMin.x, remapMax.x), new (cutoffMin.x, cutoffMax.x)),
					method(timeValue.y, new (remapMin.y, remapMax.y), new (cutoffMin.y, cutoffMax.y)),
					method(timeValue.z, new (remapMin.z, remapMax.z), new (cutoffMin.z, cutoffMax.z))
				)
				* strength;
		}
	}
}