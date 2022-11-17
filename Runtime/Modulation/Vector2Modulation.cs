using UnityEngine;

namespace Extendo.Modulation
{
	public class Vector2Modulation : Modulation<Vector2>
	{
		public Vector2Modulation()
		{
			speed     = Vector2.one;
			seed      = new (12345, 12345);
			remapMax  = Vector2.one;
			cutoffMax = Vector2.one;
		}

		protected override Vector2 GetModulationValue
		(
			ModulateDelegate method,
			float time,
			Vector2 remapMin,
			Vector2 remapMax,
			Vector2 cutoffMin,
			Vector2 cutoffMax
		)
		{
			Vector2 timeValue = Vector2.Scale((Vector2.one * time) + offset, speed) + seed;

			return new
			(
				method(timeValue.x, new (remapMin.x, remapMax.x), new (cutoffMin.x, cutoffMax.x)),
				method(timeValue.y, new (remapMin.y, remapMax.y), new (cutoffMin.y, cutoffMax.y))
			);
		}
	}
}