using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Interaction
{
	[AddComponentMenu("Extendo/Interactable")]
	public class Interactable : MonoBehaviour, IInteractable
	{
		public bool  enableCooldown;
		public float cooldownTime = 1f;
		public bool  InCooldown => cooldownRoutine != null;
		public bool  toggleValue = true;

		[Space]
		public UnityEvent onInteract;
		public UnityEvent<bool> onInteractToggle;
		public UnityEvent       onCooldown;
		public UnityEvent       onCooldownComplete;

		private Coroutine cooldownRoutine;

		private void OnDisable()
		{
			ResetCooldown();
		}

		[ContextMenu("Interact")]
		public void OnInteract()
		{
			if (enableCooldown)
			{
				if (InCooldown)
					return;

				StartCooldown();
			}

			toggleValue = !toggleValue;

			onInteract.Invoke();
			onInteractToggle.Invoke(toggleValue);
		}

		private void ResetCooldown()
		{
			if (cooldownRoutine != null)
				StopCoroutine(cooldownRoutine);

			cooldownRoutine = null;
		}

		private void StartCooldown()
		{
			cooldownRoutine = StartCoroutine(CooldownRoutine());
		}

		IEnumerator CooldownRoutine()
		{
			onCooldown.Invoke();
			yield return new WaitForSeconds(cooldownTime);
			cooldownRoutine = null;
			onCooldownComplete.Invoke();
		}
	}
}