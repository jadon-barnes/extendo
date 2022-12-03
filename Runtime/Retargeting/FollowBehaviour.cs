using System.Collections;
using UnityEngine;

namespace Extendo.Retargeting
{
	public abstract class FollowBehaviour<T> : MonoBehaviour
	{
		public bool      useFixedUpdate = false;
		public Transform target;
		public Vector3   offset;

		private void UpdateFollowValue()
		{
			if (!target)
				return;

			SetTransformValue(CalculateFollowValue());
		}

		protected abstract void SetTransformValue(T targetValue);

		protected abstract T CalculateFollowValue();

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