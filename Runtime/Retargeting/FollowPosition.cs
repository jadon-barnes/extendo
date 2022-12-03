using UnityEngine;

namespace Extendo.Retargeting
{
	public abstract class FollowPosition : FollowBehaviour<Vector3>
	{
		protected Vector3 TargetPosition => target.position + offset;

		protected override void SetTransformValue(Vector3 targetValue)
		{
			transform.position = new(
				useAxis.x ? targetValue.x : transform.position.x,
				useAxis.y ? targetValue.y : transform.position.y,
				useAxis.z ? targetValue.z : transform.position.z
			);
		}
	}
}