using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Smooth Damp Follow Position")]
	public class SmoothDampFollowPosition : FollowPosition
	{
		public  float   smoothTime = 5f;
		public  float   maxSpeed   = float.PositiveInfinity;
		private Vector3 velocity;

		protected override Vector3 CalculateFollowValue()
		{
			return Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime, maxSpeed);
		}
	}
}