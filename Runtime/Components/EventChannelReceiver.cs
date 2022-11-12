using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Components
{
	public class EventChannelReceiver : MonoBehaviour
	{
		[field: SerializeField]
		public EventChannel EventChannel { get; private set; }
		public UnityEvent onEventInvoked;

		private void OnEnable()
		{
			EventChannel.Subscribe(Invoke);
		}

		private void OnDisable()
		{
			EventChannel.Unsubscribe(Invoke);
		}

		private void Invoke()
		{
			onEventInvoked.Invoke();
		}
	}
}