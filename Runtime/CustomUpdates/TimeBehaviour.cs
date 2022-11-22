using UnityEngine;

namespace Extendo.CustomUpdates
{
	public abstract class TimeBehaviour : CustomUpdate
	{
		public bool resetOnEnable = true;
		[Space]
		[Tooltip("-1 will run indefinitely.")]
		public float duration = 3f;
		[Tooltip("A value of -1 will repeat indefinitely.")]
		public int repeat;
		public float time;

		public float TimeValue       => time / duration;
		public bool  DurationReached => time >= duration && duration > 0f;
		public bool  IsRepeating     => (repeat > 0 || repeat < 0) && RepeatCount < repeat - 1;
		public int   RepeatCount     { get; private set; }

		protected override void OnEnable()
		{
			if (!IsRunning && resetOnEnable)
				Reset();

			base.OnEnable();
		}

		public void ResetTime() => time = 0f;

		public void Reset()
		{
			ResetTime();
			RepeatCount = 0;
		}

		protected virtual void OnTimeComplete()       {}
		protected virtual void OnTimeRepeatComplete() {}

		public override void ManualUpdate()
		{
			if (DurationReached && !IsRepeating)
			{
				Stop();
				return;
			}

			// Add Time
			time += Time.deltaTime;

			if (!DurationReached)
				return;

			// Clamp time value to max
			time = Mathf.Min(time, duration);

			OnTimeComplete();

			// Repeat if applicable
			if (!IsRepeating)
			{
				Stop();
				OnTimeRepeatComplete();
				return;
			}

			// Add to Repeat count and reset time
			RepeatCount++;
			ResetTime();
		}
	}
}