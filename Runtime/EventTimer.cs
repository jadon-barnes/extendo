using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FunWithRagdolls.Utilities
{
	public class EventTimer : MonoBehaviour
	{
		public enum Update
		{
			Manual      = 0,
			Update      = 1,
			FixedUpdate = 2,
		}

		public  float      time;
		public  float      duration = 5f;
		public  bool       repeat;
		public  bool       resetOnDisabled;
		public  int        RepeatCount { get; private set; }
		public  float      Value       => time / duration;
		public  bool       Done        => time >= duration;
		public  Update     updateMethod = Update.Update;
		public  UnityEvent onDone;
		private Coroutine  updateRoutine;

		private void OnEnable()
		{
			if (updateMethod != Update.Manual)
				updateRoutine = StartCoroutine(UpdateRoutine());
		}

		private void OnDisable()
		{
			if (updateRoutine != null)
				StopCoroutine(updateRoutine);

			if (resetOnDisabled)
				time = 0f;
		}

		private IEnumerator UpdateRoutine()
		{
			// Count down
			while (!Done || repeat)
			{
				UpdateTimer();

				switch (updateMethod)
				{
					case Update.FixedUpdate:
						yield return new WaitForFixedUpdate();
						break;
					default:
						yield return null;
						break;
				}
			}

			yield break;
		}

		public void UpdateTimer()
		{
			if (Done && !repeat)
				return;

			time += UnityEngine.Time.deltaTime;

			OnDone();
		}

		private void OnDone()
		{
			if (!Done)
				return;

			// Clamp time value to max
			time = Mathf.Min(time, duration);

			// Invoke Events
			onDone.Invoke();

			// Repeat if applicable
			if (repeat)
			{
				RepeatCount++;
				time = 0f;
			}
		}
	}
}