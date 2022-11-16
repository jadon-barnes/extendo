using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Events
{
	[CreateAssetMenu(fileName = "Event Channel", menuName = "Extendo/Event Channel")]
	public class EventChannel : ScriptableObject
	{
		private UnityEvent unityEvent;

		public void Subscribe(UnityAction call) => unityEvent.AddListener(call);

		public void Unsubscribe(UnityAction call) => unityEvent.RemoveListener(call);

		public void Invoke()
		{
			unityEvent.Invoke();
		}

		private void OnEnable()
		{
			unityEvent = new UnityEvent();
		}
	}
}