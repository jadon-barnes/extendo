using System.Collections;
using UnityEngine;

namespace Extendo.Retargeting
{
	public abstract class FollowBehaviour : MonoBehaviour
	{
		public bool               useFixedUpdate = false;
		public Transform          target;
		public ConstraintSettings constraintSettings = new ConstraintSettings();

		private Vector3 followPosition;

		private void UpdateFollowValue()
		{
			if (!target)
				return;

			if (!constraintSettings.enableX && !constraintSettings.enableY && !constraintSettings.enableZ)
				return;

			followPosition = CalculateFollowPosition(transform.position, target.position + constraintSettings.offset);

			followPosition.x = constraintSettings.enableX ? followPosition.x : transform.position.x;
			followPosition.y = constraintSettings.enableY ? followPosition.y : transform.position.y;
			followPosition.z = constraintSettings.enableZ ? followPosition.z : transform.position.z;

			transform.position = followPosition;
		}

		protected abstract Vector3 CalculateFollowPosition(Vector3 from, Vector3 to);

		private void OnEnable()
		{
			StartCoroutine(UpdateRoutine());
		}

		IEnumerator UpdateRoutine()
		{
			while (enabled)
			{
				UpdateFollowValue();

				yield return useFixedUpdate ? new WaitForFixedUpdate() : new WaitForEndOfFrame();
			}
		}
	}
}