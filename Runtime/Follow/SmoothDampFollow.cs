using UnityEngine;

namespace Extendo
{
	[AddComponentMenu("Extendo/Follow/Smooth Damp Follow")]
	public class SmoothDampFollow : FollowBehaviour
	{
		public  float   smoothTime = 5f;
		public  float   maxSpeed   = float.PositiveInfinity;
		private Vector3 velocity;

		protected override Vector3 GetFollowPosition()
		{
			return Vector3.SmoothDamp
			(
				transform.position,
				TargetPosition,
				ref velocity,
				smoothTime,
				maxSpeed,
				Time.deltaTime
			);
		}
	}
}