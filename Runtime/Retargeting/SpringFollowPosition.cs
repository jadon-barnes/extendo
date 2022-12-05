using Extendo.Utilities;
using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Spring Follow Position")]
	public class SpringFollowPosition : FollowBehaviour
	{
		public  float   springStrength = 200f;
		public  float   springDamp     = 5f;
		private Vector3 velocity;

		protected override Vector3 CalculateFollowPosition(Vector3 from, Vector3 to)
		{
			return Math.Spring(from, to, ref velocity, springStrength, springDamp);
		}
	}
}