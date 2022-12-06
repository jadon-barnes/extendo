using System;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Modulation.Curves
{
	[Serializable]
	public class FloatCurve : Curve<float>
	{
		public FloatCurve()
		{
			scale = 1f;
			curve = DefaultCurve;
		}

		public AnimationCurve curve;

		public override float GetValue(float time)
		{
			return curve.Evaluate(Mathf.Repeat(time + offset, 1f)) * scale;
		}
	}
}