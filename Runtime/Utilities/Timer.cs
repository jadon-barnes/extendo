using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Utilities
{
	public class Timer
	{
		public Timer(float duration, bool repeat = false, UnityAction onComplete = null)
		{
			this.duration   = duration;
			this.repeat     = repeat;
			this.onComplete = onComplete;
		}

		public  float       duration = 5f;
		public  bool        repeat   = false;
		public  float       time     = 0f;
		public  float       TimeValue       => time / duration;
		public  bool        DurationReached => time >= duration;
		private UnityAction onComplete;

		public void Update()
		{
			if (DurationReached && !repeat)
				return;

			// Add Time
			time = Mathf.Min(time + Time.deltaTime, duration);

			if (!DurationReached)
				return;

			onComplete.Invoke();

			if (repeat)
				time = 0;
		}

		public void Reset()
		{
			time = 0f;
		}
	}
}