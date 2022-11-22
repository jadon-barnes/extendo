using Extendo.CustomUpdates;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Events
{
	[AddComponentMenu("Extendo/Events/Event Timer")]
	public class EventTimer : MonoBehaviour
	{
		public bool       resetOnEnable = true;
		public float      duration      = 3f;
		public bool       repeat;
		public float      time;
		public int        RepeatCountFromStart { get; private set; }
		public float      TimeValue            => time / duration;
		public bool       DurationReached      => time >= duration;
		public UnityEvent onComplete;

		private void OnEnable()
		{
			if (resetOnEnable)
			{
				Reset();
			}
		}

		private void Update()
		{
			if (DurationReached && !repeat)
				return;

			// Add Time
			time = Mathf.Min(time + Time.deltaTime, duration);

			if (DurationReached)
			{
				onComplete.Invoke();

				// Repeat if applicable
				if (repeat)
				{
					RepeatCountFromStart++;
					time = 0;
				}
			}
		}

		[ContextMenu("Reset")]
		public void Reset()
		{
			time                 = 0f;
			RepeatCountFromStart = 0;
		}
	}
}