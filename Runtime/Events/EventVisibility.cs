using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Events
{
	[AddComponentMenu("Extendo/Events/Event Visibility")]
	[RequireComponent(typeof(Renderer))]
	public class EventVisibility : MonoBehaviour
	{
		public UnityEvent<bool> onVisibility;

		private void OnBecameVisible() => onVisibility.Invoke(true);

		private void OnBecameInvisible() => onVisibility.Invoke(false);
	}
}