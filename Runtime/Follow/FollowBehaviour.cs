using System.Collections;
using UnityEngine;

namespace Extendo
{
	public abstract class FollowBehaviour : MonoBehaviour
	{
		public    bool      useFixedUpdate = false;
		public    Transform target;
		protected Vector3   TargetPosition => target.position;
		public    bool      enablePosition = true;
		public    bool      enableRotation = true;

		private void UpdatePosition()
		{
			if (!enablePosition)
				return;

			transform.position = GetFollowPosition();
		}
		

		protected abstract Vector3 GetFollowPosition();

		private void OnEnable()
		{
			StartCoroutine(UpdateRoutine());
		}

		IEnumerator UpdateRoutine()
		{
			while (enabled)
			{
				UpdatePosition();
				yield return useFixedUpdate ? new WaitForFixedUpdate() : new WaitForEndOfFrame();
			}
		}
	}
}