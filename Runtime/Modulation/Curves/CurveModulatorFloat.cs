using UnityEngine;

namespace Extendo.Modulation.Curves
{
	[AddComponentMenu("Extendo/Modulation/Curves/Float Curve Modulator")]
	public class CurveModulatorFloat : CurveModulator<float>
	{
		public CurveModulatorFloat()
		{
			scale = 1f;
			curve = DefaultCurve;
		}

		public AnimationCurve curve;

		protected override float GetValue()
		{
			return curve.Evaluate(Mathf.Repeat(timer.TimeValue + offset, 1f)) * scale;
		}
	}
}