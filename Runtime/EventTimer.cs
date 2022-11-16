using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo
{
	[AddComponentMenu("Extendo/Event Timer")]
	public class EventTimer : CustomUpdateBehaviour
	{
		public float      time;
		public float      duration = 5f;
		public bool       repeat;
		public bool       resetOnDisabled;
		public int        RepeatCount { get; private set; }
		public float      Value       => time / duration;
		public bool       Done        => time >= duration;
		public UnityEvent onDone;

		protected override void OnDisable()
		{
			base.OnDisable();
			
			if (resetOnDisabled)
				time = 0f;
		}

		protected override void OnUpdate()
		{
			UpdateTimer();

			if (Done)
			{
				StopUpdate();

				if (repeat)
					StartUpdate();
			}
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