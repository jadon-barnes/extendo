using UnityEngine;

namespace Extendo.Retargeting
{
	public abstract class FollowPosition : FollowBehaviour
	{
		public  ConstraintSettings constraintSettings = new ConstraintSettings();
		private Vector3            followPosition;

		protected override void CalculateFollow()
		{
			if (!constraintSettings.enableX && !constraintSettings.enableY && !constraintSettings.enableZ)
				return;

			followPosition = CalculateFollowPosition(
				transform.position,
				target.position
				+ (constraintSettings.useLocalOffset && transform.parent
					? transform.parent.TransformPoint(constraintSettings.offset)
					: constraintSettings.offset)
			);

			followPosition.x = constraintSettings.enableX ? followPosition.x : transform.position.x;
			followPosition.y = constraintSettings.enableY ? followPosition.y : transform.position.y;
			followPosition.z = constraintSettings.enableZ ? followPosition.z : transform.position.z;

			transform.position = followPosition;
		}

		protected abstract Vector3 CalculateFollowPosition(Vector3 from, Vector3 to);
	}
}