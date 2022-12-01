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
			return new Quaternion(
				Math.ExpDampAngle(transform.rotation.x, target.rotation.x, smoothTime),
				Math.ExpDampAngle(transform.rotation.y, target.rotation.y, smoothTime),
				Math.ExpDampAngle(transform.rotation.z, target.rotation.z, smoothTime),
				Math.ExpDampAngle(transform.rotation.w, target.rotation.w, smoothTime)
			);
		}
	}
}