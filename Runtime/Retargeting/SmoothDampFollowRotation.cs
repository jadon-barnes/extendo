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
			return new Quaternion(
				Mathf.SmoothDampAngle(transform.rotation.x, target.rotation.x, ref velocity.x, smoothTime, maxSpeed),
				Mathf.SmoothDampAngle(transform.rotation.y, target.rotation.y, ref velocity.y, smoothTime, maxSpeed),
				Mathf.SmoothDampAngle(transform.rotation.z, target.rotation.z, ref velocity.z, smoothTime, maxSpeed),
				Mathf.SmoothDampAngle(transform.rotation.w, target.rotation.w, ref velocity.w, smoothTime, maxSpeed)
			);
		}
	}
}