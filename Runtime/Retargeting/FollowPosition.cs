using UnityEngine;

namespace Extendo.Retargeting
{
	public abstract class FollowPosition : FollowBehaviour<Vector3>
	{
		protected Vector3 TargetPosition => target.position + offset;

		protected override void SetTransformValue(Vector3 targetValue)
		{
			transform.position = new(
				axis.x ? targetValue.x : transform.position.x,
				axis.y ? targetValue.y : transform.position.y,
				axis.z ? targetValue.z : transform.position.z
			);
		}
	}
}