using System;
using UnityEngine;
using Math = Extendo.Utilities.Math;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Follow Position")]
	public class FollowPosition : FollowBehaviour
	{
		public  InterpolationMethod interpolationMethod = InterpolationMethod.None;
		public  ConstraintSettings  constraintSettings  = new ConstraintSettings();
		private Vector3             followPosition;

		public ExponentialDampSettings exponentialDampSettings;
		public SmoothDampSettings      smoothDampSettings;
		public SpringSettings          springSettings;

		private Vector3 velocity;

		protected override void CalculateFollow()
		{
			if (!constraintSettings.enableX && !constraintSettings.enableY && !constraintSettings.enableZ)
				return;

			followPosition = CalculateFollowPosition(
				transform.position,
				target.position
				+ (constraintSettings.useLocal && transform.parent
					? transform.parent.TransformPoint(constraintSettings.offset)
					: constraintSettings.offset)
			);

			followPosition.x = constraintSettings.enableX ? followPosition.x : transform.position.x;
			followPosition.y = constraintSettings.enableY ? followPosition.y : transform.position.y;
			followPosition.z = constraintSettings.enableZ ? followPosition.z : transform.position.z;

			transform.position = followPosition;
		}

		protected Vector3 CalculateFollowPosition(Vector3 from, Vector3 to)
		{
			switch (interpolationMethod)
			{
				case InterpolationMethod.ExponentialDamp:
					return Math.ExpDamp(from, to, exponentialDampSettings.smoothTime);
				case InterpolationMethod.SmoothDamp:
					return Vector3.SmoothDamp(
						from,
						to,
						ref velocity,
						smoothDampSettings.smoothTime,
						smoothDampSettings.maxSpeed
					);
				case InterpolationMethod.Spring:
					return Math.Spring(
						from,
						to,
						ref velocity,
						springSettings.springStrength,
						springSettings.springDamp
					);
				default: return to;
			}
		}
	}
}