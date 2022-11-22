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
		public bool        DurationReached => time >= duration;
		public UnityAction onDurationReached;

		public float TimeValue         => time / duration;
		public float TimeValueReversed => 1 - time / duration;
		public float TimeReversed      => duration - time;

		public int   Hours                => Mathf.FloorToInt(time / 3600f);
		public int   Minutes              => Mathf.FloorToInt(time / 60f);
		public int   Seconds              => Mathf.FloorToInt(time % 60f);
		public int   HoursReversed        => Mathf.FloorToInt(duration / 3600f - time / 3600f);
		public int   MinutesReversed      => Mathf.FloorToInt(60f - time / 60f);
		public int   SecondsReversed      => Mathf.FloorToInt(60f - time % 60f);
		public float HoursExact           => time / 3600f;
		public float MinutesExact         => time / 60f;
		public float SecondsExact         => time % 60f;
		public float HoursExactReversed   => duration / 3600f - time / 3600f;
		public float MinutesExactReversed => 60f - time / 60f;
		public float SecondsExactReversed => 60f - time % 60f;

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