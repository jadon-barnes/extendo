using System;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Utilities
{
	[Serializable]
	public class Timer
	{
		public Timer(float duration = 5f, bool repeat = false, UnityAction onDurationReached = null)
		{
			this.duration          = duration;
			this.repeat            = repeat;
			this.onDurationReached = onDurationReached;
		}

		public float       duration = 5f;
		public bool        repeat   = false;
		public float       time     = 0f;
		public float       TimeValue       => time / duration;
		public bool        DurationReached => time >= duration;
		public int         Hours           => Mathf.FloorToInt(time / 3600f);
		public int         Minutes         => Mathf.FloorToInt(time / 60f);
		public int         Seconds         => Mathf.FloorToInt(time % 60f);
		public float       HoursExact      => time / 3600f;
		public float       MinutesExact    => time / 60f;
		public float       SecondsExact    => time % 60f;
		public UnityAction onDurationReached;

		public void Update()
		{
			if (DurationReached && !repeat)
				return;

			// Add Time
			time = Mathf.Min(time + Time.deltaTime, duration);

			if (!DurationReached)
				return;

			onDurationReached.Invoke();

			if (repeat)
				time = 0;
		}

		public void Reset()
		{
			time = 0f;
		}
	}
}