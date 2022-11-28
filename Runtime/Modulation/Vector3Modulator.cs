using System;
using UnityEngine;

namespace Extendo.Modulation
{
	[Serializable]
	public class Vector3Modulator : Modulator<Vector3>
	{
		public Vector3Modulator()
		{
			speed    = Vector3.one;
			to       = Vector3.one;
			cutoffTo = Vector3.one;
		}

		protected override Vector3 GetValue
		(
			Modulate method,
			float    time,
			Vector3  remapMin,
			Vector3  remapMax,
			Vector3  cutoffMin,
			Vector3  cutoffMax
		)
		{
			Vector3 timeValue = Vector3.Scale(Vector3.one * time + offset, speed);

			return new Vector3(method(timeValue.x, remapMin.x, remapMax.x, cutoffMin.x, cutoffMax.x), method(timeValue.y, remapMin.y, remapMax.y, cutoffMin.y, cutoffMax.y), method(timeValue.z, remapMin.z, remapMax.z, cutoffMin.z, cutoffMax.z)) * strength;
		}
	}
}