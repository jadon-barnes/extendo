using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Interaction
{
	[AddComponentMenu("Extendo/Interactable")]
	public class Interactable : MonoBehaviour
	{
		public UnityEvent    onInteract;
		private IInteractable[] interactables = new IInteractable[0];

		private void Awake()
		{
			interactables = GetComponents<IInteractable>();
		}

		[ContextMenu("Interact")]
		public void Interact()
		{
			onInteract.Invoke();
			
			foreach (var interactable in interactables)
			{
				interactable.OnInteract();
			}
		}
	}
}