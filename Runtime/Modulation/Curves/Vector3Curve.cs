using System;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Modulation.Curves
{
	[Serializable]
	public class Vector3Curve : Curve<Vector3>
	{
		public Vector3Curve()
		{
			scale  = Vector3.one;
			curveX = DefaultCurve;
			curveY = DefaultCurve;
			curveZ = DefaultCurve;
		}

		public AnimationCurve curveX, curveY, curveZ;

		public override Vector3 GetValue(float time)
		{
			return new(
				curveX.Evaluate(Mathf.Repeat(time + offset.x, 1f)) * scale.x,
				curveY.Evaluate(Mathf.Repeat(time + offset.y, 1f)) * scale.y,
				curveZ.Evaluate(Mathf.Repeat(time + offset.z, 1f)) * scale.z
			);
		}
	}
}