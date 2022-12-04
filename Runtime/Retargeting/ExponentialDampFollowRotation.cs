using Extendo.Utilities;
using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Exponential Damp Follow Rotation")]
	public class ExponentialDampFollowRotation : FollowRotation
	{
		public float smoothTime = 5f;

		protected override Quaternion CalculateFollowValue()
		{
			var targetRotation = TargetRotation;

			return new Quaternion(
				Math.ExpDampAngle(transform.rotation.x, targetRotation.x, smoothTime),
				Math.ExpDampAngle(transform.rotation.y, targetRotation.y, smoothTime),
				Math.ExpDampAngle(transform.rotation.z, targetRotation.z, smoothTime),
				Math.ExpDampAngle(transform.rotation.w, targetRotation.w, smoothTime)
			);
		}
	}
}