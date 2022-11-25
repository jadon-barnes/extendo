using System.Collections;
using Extendo.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Interaction
{
	[AddComponentMenu("Extendo/Interactable")]
	public class Interactable : MonoBehaviour, IInteractable
	{
		public bool  enableCooldown;
		public float cooldownTime = 1f;
		public bool  toggleValue  = true;
		public bool  InCooldown => this.IsInvoking(OnCooldownComplete);

		[Space]
		public UnityEvent onInteract;
		public UnityEvent<bool> onInteractToggle;
		public UnityEvent       onCooldown;
		public UnityEvent       onCooldownComplete;

		[ContextMenu("Interact")]
		public void OnInteract()
		{
			if (enableCooldown)
			{
				if (InCooldown)
					return;

				onCooldown.Invoke();
				this.Invoke(OnCooldownComplete, cooldownTime);
			}

			toggleValue = !toggleValue;

			onInteract.Invoke();
			onInteractToggle.Invoke(toggleValue);
		}

		private void OnCooldownComplete()
		{
			onCooldownComplete.Invoke();
		}
	}
}