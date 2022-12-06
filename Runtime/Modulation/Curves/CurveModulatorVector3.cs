using UnityEngine;

namespace Extendo.Modulation.Curves
{
	[AddComponentMenu("Extendo/Modulation/Curves/Curve Modulator Vector3")]
	public class CurveModulatorVector3 : CurveModulator<Vector3>
	{
		public CurveModulatorVector3()
		{
			scale  = Vector3.one;
			curveX = DefaultCurve;
			curveY = DefaultCurve;
			curveZ = DefaultCurve;
		}

		public AnimationCurve curveX, curveY, curveZ;

		protected override Vector3 GetValue()
		{
			return new(
				curveX.Evaluate(Mathf.Repeat(timer.TimeValue + offset.x, 1f)) * scale.x,
				curveY.Evaluate(Mathf.Repeat(timer.TimeValue + offset.y, 1f)) * scale.y,
				curveZ.Evaluate(Mathf.Repeat(timer.TimeValue + offset.z, 1f)) * scale.z
			);
		}
	}
}