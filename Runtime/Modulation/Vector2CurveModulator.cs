using UnityEngine;

namespace Extendo.Modulation
{
	[AddComponentMenu("Extendo/Modulation/Vector2 Curve Modulator")]
	public class Vector2CurveModulator : CurveModulator<Vector2>
	{
		public Vector2CurveModulator()
		{
			scale  = Vector2.one;
			curveX = DefaultCurve;
			curveY = DefaultCurve;
		}

		public AnimationCurve curveX, curveY;

		protected override Vector2 GetValue()
		{
			return new Vector2
			(
				curveX.Evaluate(Mathf.Repeat(timer.TimeValue + offset.x, 1f)) * scale.x,
				curveY.Evaluate(Mathf.Repeat(timer.TimeValue + offset.y, 1f)) * scale.y
			);
		}
	}
}