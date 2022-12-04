using UnityEngine;
using Extendo.Utilities;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Spring Follow Rotation")]
	public class SpringFollowRotation : FollowRotation
	{
		public  float      springStrength = 200f;
		public  float      springDamp     = 5f;
		private Quaternion velocity;

		protected override Quaternion CalculateFollowValue()
		{
			var targetRotation = TargetRotation;

			return new Quaternion(
				Math.Spring(transform.rotation.x, targetRotation.x, ref velocity.x, springStrength, springDamp),
				Math.Spring(transform.rotation.y, targetRotation.y, ref velocity.y, springStrength, springDamp),
				Math.Spring(transform.rotation.z, targetRotation.z, ref velocity.z, springStrength, springDamp),
				Math.Spring(transform.rotation.w, targetRotation.w, ref velocity.w, springStrength, springDamp)
			);
		}
	}
}