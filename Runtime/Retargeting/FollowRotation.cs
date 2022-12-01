using UnityEngine;

namespace Extendo.Retargeting
{
	public abstract class FollowRotation : FollowBehaviour<Quaternion>
	{
		protected override void SetTransformValue(Quaternion targetValue)
		{
			transform.rotation = targetValue;
		}
	}
}