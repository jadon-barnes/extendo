using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Smooth Damp Follow Rotation")]
	public class SmoothDampFollowRotation : FollowRotation
	{
		public  float      smoothTime = 5f;
		public  float      maxSpeed   = float.PositiveInfinity;
		private Quaternion velocity;

		protected override Quaternion CalculateFollowValue()
		{
			var targetRotation = TargetRotation;

			return new Quaternion(
				Mathf.SmoothDampAngle(transform.rotation.x, targetRotation.x, ref velocity.x, smoothTime, maxSpeed),
				Mathf.SmoothDampAngle(transform.rotation.y, targetRotation.y, ref velocity.y, smoothTime, maxSpeed),
				Mathf.SmoothDampAngle(transform.rotation.z, targetRotation.z, ref velocity.z, smoothTime, maxSpeed),
				Mathf.SmoothDampAngle(transform.rotation.w, targetRotation.w, ref velocity.w, smoothTime, maxSpeed)
			);
		}
	}
}