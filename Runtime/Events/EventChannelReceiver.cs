using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Events
{
	[AddComponentMenu("Extendo/Events/Event Channel Receiver")]
	public class EventChannelReceiver : MonoBehaviour
	{
		public EventChannel eventChannel;
		public UnityEvent   onEventInvoked;

		private void OnEnable()
		{
			eventChannel.Subscribe(Invoke);
		}

		private void OnDisable()
		{
			eventChannel.Unsubscribe(Invoke);
		}

		private void Invoke()
		{
			onEventInvoked.Invoke();
		}
	}
}