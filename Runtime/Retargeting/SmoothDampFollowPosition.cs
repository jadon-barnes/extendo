using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Smooth Damp Follow Position")]
	public class SmoothDampFollowPosition : FollowPosition
	{
		public  float   smoothTime = 5f;
		public  float   maxSpeed   = float.PositiveInfinity;
		private Vector3 velocity;

		protected override Vector3 CalculateFollowPosition(Vector3 from, Vector3 to)
		{
			return Vector3.SmoothDamp(from, to, ref velocity, smoothTime, maxSpeed);
		}
	}
}