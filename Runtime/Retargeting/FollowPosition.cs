using UnityEngine;

namespace Extendo.Retargeting
{
	public abstract class FollowPosition : FollowBehaviour<Vector3>
	{
		[Space] public bool x;
		public         bool y;
		public         bool z;

		protected Vector3 TargetPosition => target.position + offset;

		protected override void SetTransformValue(Vector3 targetValue)
		{
			transform.position = new(
				x ? targetValue.x : transform.position.x,
				y ? targetValue.y : transform.position.y,
				z ? targetValue.z : transform.position.z
			);
		}
	}
}