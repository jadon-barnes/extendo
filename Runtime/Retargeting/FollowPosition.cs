using UnityEngine;

namespace Extendo.Retargeting
{
	public abstract class FollowPosition : FollowBehaviour<Vector3>
	{
		public bool x = true;
		public bool y = true;
		public bool z = true;

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