using System.Collections;
using UnityEngine;

namespace Extendo
{
	public abstract class CustomUpdateBehaviour : MonoBehaviour
	{
		public  bool      playOnEnable = true;
		public  bool      runOnFixedUpdate = false;
		private Coroutine customUpdateRoutine;

		protected virtual void OnEnable()
		{
			if (playOnEnable)
				StartUpdate();
		}

		protected virtual void OnDisable()
		{
			StopUpdate();
		}

		public void StartUpdate()
		{
			// Stop if already running routine
			StopUpdate();
			
			// Start new update
			customUpdateRoutine = StartCoroutine(UpdateRoutine());
		}

		public void StopUpdate()
		{
			if (customUpdateRoutine != null)
				StopCoroutine(customUpdateRoutine);
		}

		protected abstract void OnUpdate();

		private IEnumerator UpdateRoutine()
		{
			while (true)
			{
				OnUpdate();

				yield return runOnFixedUpdate ? new WaitForFixedUpdate() : null;
			}
		}
	}
}