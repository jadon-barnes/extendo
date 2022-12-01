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
			return new Quaternion(
				Math.SpringAngle(transform.rotation.x, target.rotation.x, ref velocity.x, springStrength, springDamp),
				Math.SpringAngle(transform.rotation.y, target.rotation.y, ref velocity.y, springStrength, springDamp),
				Math.SpringAngle(transform.rotation.z, target.rotation.z, ref velocity.z, springStrength, springDamp),
				Math.SpringAngle(transform.rotation.w, target.rotation.w, ref velocity.w, springStrength, springDamp)
			);
		}
	}
}