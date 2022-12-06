using UnityEngine;

namespace Extendo.Modulation.Curves
{
	[AddComponentMenu("Extendo/Modulation/Curves/Curve Modulator Vector2")]
	public class CurveModulatorVector2 : CurveModulator<Vector2>
	{
		public CurveModulatorVector2()
		{
			scale  = Vector2.one;
			curveX = DefaultCurve;
			curveY = DefaultCurve;
		}

		public AnimationCurve curveX, curveY;

		protected override Vector2 GetValue()
		{
			return new(curveX.Evaluate(Mathf.Repeat(timer.TimeValue + offset.x, 1f)) * scale.x, curveY.Evaluate(Mathf.Repeat(timer.TimeValue + offset.y, 1f)) * scale.y);
		}
	}
}