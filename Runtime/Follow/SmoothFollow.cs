using Extendo.Utilities;
using UnityEngine;

namespace Extendo
{
	[AddComponentMenu("Extendo/Follow/Smooth Follow")]
	public class SmoothFollow : FollowBehaviour
	{
		public float smoothTime = 5f;

		protected override Vector3 GetFollowPosition()
		{
			return Math.Damp(transform.position, TargetPosition, smoothTime);
		}
	}
}