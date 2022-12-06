using System;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Modulation.Curves
{
	[Serializable]
	public class Vector2Curve : Curve<Vector2>
	{
		public Vector2Curve()
		{
			scale  = Vector2.one;
			curveX = DefaultCurve;
			curveY = DefaultCurve;
		}

		public AnimationCurve curveX, curveY;

		public override Vector2 GetValue(float time)
		{
			return new(
				curveX.Evaluate(Mathf.Repeat(time + offset.x, 1f)) * scale.x,
				curveY.Evaluate(Mathf.Repeat(time + offset.y, 1f)) * scale.y
			);
		}
	}
}