using System.Collections;
using UnityEngine;

namespace Extendo.CustomUpdates
{
	public class TimedBehaviour : MonoBehaviour
	{
		public  bool      playOnEnable     = true;
		public  bool      runOnFixedUpdate = false;
		public  float     duration         = 2f;
		public  float     ElapsedTime { get; private set; }
		public  bool      Done        => AmountDone >= 1f;
		public  float     AmountDone  => ElapsedTime / duration;
		private Coroutine updateCoroutine;

		protected virtual void OnEnable()
		{
			if (playOnEnable)
				Start();
		}

		public void Start()
		{
			Stop();
			ElapsedTime     = 0f;
			updateCoroutine = StartCoroutine(UpdateRoutine());
		}

		public void Stop()
		{
			if (updateCoroutine != null)
				StopCoroutine(updateCoroutine);
		}

		protected virtual void OnUpdate() {}

		private IEnumerator UpdateRoutine()
		{
			while (!Done)
			{
				OnUpdate();
				ElapsedTime += Time.deltaTime;
				yield return runOnFixedUpdate ? new WaitForFixedUpdate() : null;
			}

			yield return null;
		}
	}
}