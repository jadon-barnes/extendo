using System.Collections;
using UnityEngine;

namespace Extendo.CustomUpdates
{
	public abstract class CustomUpdate : MonoBehaviour
	{
		public bool playOnEnable     = true;
		public bool runOnFixedUpdate = false;

		protected virtual void OnEnable()
		{
			if (playOnEnable)
				StartCoroutine(UpdateRoutine());
		}

		protected abstract void ManualUpdate();

		protected IEnumerator UpdateRoutine()
		{
			while (enabled)
			{
				ManualUpdate();
				yield return runOnFixedUpdate ? new WaitForFixedUpdate() : null;
			}

			yield break;
		}
	}
}