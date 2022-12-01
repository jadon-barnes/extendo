using Extendo.Utilities;
using UnityEngine;

namespace Extendo
{
	[AddComponentMenu("Extendo/Follow/Spring Follow")]
	public class SpringFollow : FollowBehaviour
	{
		public  float   springStrength = 200f;
		public  float   springDamp     = 5f;
		private Vector3 velocity;
		private Vector3 velocityAngles;

		protected override Vector3 GetFollowPosition()
		{
			return Math.Spring(transform.position, TargetPosition, ref velocity, springStrength, springDamp);
		}
	}
}