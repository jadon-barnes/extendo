using Extendo.Utilities;
using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Spring Follow Position")]
	public class SpringFollowPosition : FollowPosition
	{
		public  float   springStrength = 200f;
		public  float   springDamp     = 5f;
		private Vector3 velocity;

		protected override Vector3 CalculateFollowValue()
		{
			return Math.Spring(transform.position, target.position, ref velocity, springStrength, springDamp);
		}
	}
}