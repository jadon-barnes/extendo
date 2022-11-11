using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Components
{
	[CreateAssetMenu(fileName = "Event Channel", menuName = "Extendo/Event Channel")]
	public class EventChannel : ScriptableObject
	{
		private UnityEvent onEvent;

		public void Subscribe(UnityAction call) => onEvent.AddListener(call);

		public void Unsubscribe(UnityAction call) => onEvent.RemoveListener(call);

		public void Invoke()
		{
			onEvent.Invoke();
		}

		private void OnEnable()
		{
			onEvent = new UnityEvent();
			Debug.Log("Refreshed Asset");
		}
	}
}