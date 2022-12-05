using System.Collections;
using UnityEngine;

namespace Extendo.Retargeting
{
	public abstract class FollowBehaviour : MonoBehaviour
	{
		public bool      useFixedUpdate = false;
		public Transform target;

		protected abstract void CalculateFollow();

		private void UpdateFollow()
		{
			if (!target)
				return;

			CalculateFollow();
		}

		private void OnEnable()
		{
			StartCoroutine(UpdateRoutine());
		}

		IEnumerator UpdateRoutine()
		{
			while (enabled)
			{
				UpdateFollow();

				yield return useFixedUpdate ? new WaitForFixedUpdate() : new WaitForEndOfFrame();
			}
		}
	}
}