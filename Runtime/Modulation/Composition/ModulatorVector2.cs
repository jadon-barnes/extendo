using System;
using UnityEngine;

namespace Extendo.Modulation.Composition
{
	[Serializable]
	public class ModulatorVector2 : Modulator<Vector2>
	{
		public ModulatorVector2()
		{
			to       = Vector2.one;
			cutoffTo = Vector2.one;
		}

		protected override Vector2 CutoffFromInfinity => Vector2.negativeInfinity;
		protected override Vector2 CutoffToInfinity   => Vector2.positiveInfinity;

		protected override Vector2 GetValue
		(
			Modulate method,
			float    time,
			Vector2  remapMin,
			Vector2  remapMax,
			Vector2  cutoffMin,
			Vector2  cutoffMax
		)
		{
			Vector2 timeValue = (Vector2.one * time + timeOffset) * speed;

			return new Vector2(
				       method(timeValue.x, remapMin.x, remapMax.x, cutoffMin.x, cutoffMax.x),
				       method(timeValue.y, remapMin.y, remapMax.y, cutoffMin.y, cutoffMax.y)
			       )
			       * strength;
		}
	}
}