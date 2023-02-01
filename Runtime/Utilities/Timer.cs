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
			this.duration = duration;
			this.repeat = repeat;
			this.onDurationReached = onDurationReached;
		}

		public float duration = 5f;
		public bool  repeat   = false;

		public float Time                   { get; private set; }
		public float TimeReversed           => duration - Time;
		public float TimeNormalized         => Mathf.Abs(duration) < 0.001f ? 0f : Time / duration;
		public float TimeNormalizedReversed => 1 - TimeNormalized;
		public bool  DurationReached        => Time >= duration;

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

			// Stop here if duration isn't reached.
			if (!DurationReached)
			{
				Time = Mathf.Repeat(time, duration);
				return;
			}

			// Execute if duration reached

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