using System;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Utilities
{
	/// <summary>
	/// An advanced time class that must be manually updated.
	/// </summary>
	[Serializable]
	public class Timer
	{
		public Timer(float duration = 5f, bool repeat = false, UnityAction onDurationReached = null)
		{
			this.duration          = duration;
			this.repeat            = repeat;
			this.onDurationReached = onDurationReached;
		}

		public float duration = 5f;
		public bool  repeat   = false;

		public float Time              { get; private set; }
		public float TimeReversed      => duration - Time;
		public float TimeValue         => Mathf.Abs(duration) < 0.001f ? 0f : Time / duration;
		public float TimeValueReversed => 1 - TimeValue;
		public bool  DurationReached   => Time >= duration;

		public int   Hours                => Mathf.FloorToInt(Time / 3600f);
		public int   Minutes              => Mathf.FloorToInt(Time / 60f);
		public int   Seconds              => Mathf.FloorToInt(Time % 60f);
		public int   HoursReversed        => Mathf.FloorToInt(duration / 3600f - Time / 3600f);
		public int   MinutesReversed      => Mathf.FloorToInt(60f - Time / 60f);
		public int   SecondsReversed      => Mathf.FloorToInt(60f - Time % 60f);
		public float HoursExact           => Time / 3600f;
		public float MinutesExact         => Time / 60f;
		public float SecondsExact         => Time % 60f;
		public float HoursExactReversed   => duration / 3600f - Time / 3600f;
		public float MinutesExactReversed => 60f - Time / 60f;
		public float SecondsExactReversed => 60f - Time % 60f;

		public UnityAction onDurationReached;

		/// <summary>
		/// Updates the timer by injecting the time manually.
		/// </summary>
		/// <param name="time">Time to be injected.</param>
		public void Update(float time)
		{
			if (DurationReached && !repeat)
				return;

			// Add Time
			Time = Mathf.Min(time, duration);

			if (!DurationReached)
			{
				Time = Mathf.Repeat(time, duration);
				return;
			}

			onDurationReached?.Invoke();

			if (repeat)
				Time = Time = Mathf.Repeat(time, duration);
		}

		/// <summary>
		/// Updates the timer by <see cref="Time.deltaTime"/>
		/// </summary>
		public void Update()
		{
			Update(Time + UnityEngine.Time.deltaTime);
		}

		/// <summary>
		/// Resets the timer back to 0.
		/// </summary>
		public void Reset()
		{
			Time = 0f;
		}
	}
}