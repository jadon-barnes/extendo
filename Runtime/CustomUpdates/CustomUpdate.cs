using System.Collections;
using UnityEngine;

namespace Extendo.CustomUpdates
{
	public abstract class CustomUpdate : MonoBehaviour
	{
		public    bool      playOnEnable     = true;
		public    bool      runOnFixedUpdate = false;
		public    bool      IsRunning       => updateCoroutine != null;
		protected Coroutine updateCoroutine { get; private set; }

		protected virtual void OnEnable()
		{
			if (playOnEnable)
				Start();
		}

		protected virtual void OnDisable()
		{
			Stop();
		}

		public virtual void Start()
		{
			if (IsRunning)
				return;

			updateCoroutine = StartCoroutine(UpdateRoutine());
		}

		public virtual void Stop()
		{
			if (updateCoroutine == null)
				return;

			StopCoroutine(updateCoroutine);

			updateCoroutine = null;
		}

		public abstract void ManualUpdate();

		protected IEnumerator UpdateRoutine()
		{
			while (true)
			{
				ManualUpdate();
				yield return runOnFixedUpdate ? new WaitForFixedUpdate() : null;
			}

			yield break;
		}
	}
}