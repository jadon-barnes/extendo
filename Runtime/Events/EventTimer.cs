using Extendo.CustomUpdates;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Events
{
	[AddComponentMenu("Extendo/Events/Event Timer")]
	public class EventTimer : TimeBehaviour
	{
		public UnityEvent onComplete;
		public UnityEvent onRepeatComplete;

		protected override void OnTimeComplete()
		{
			onComplete.Invoke();
		}

		protected override void OnTimeRepeatComplete()
		{
			onRepeatComplete.Invoke();
		}
	}
}