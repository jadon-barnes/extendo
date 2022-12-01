using UnityEngine;

namespace Extendo
{
	[AddComponentMenu("Extendo/Follow/Direct Follow")]
	public class DirectFollow : FollowBehaviour
	{
		protected override Vector3 GetFollowPosition()
		{
			return TargetPosition;
		}
	}
}