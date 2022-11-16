using UnityEngine;

namespace Extendo.Interaction
{
	public static class InteractionExtension
	{
		public static bool TryToInteract(this Transform transform)
		{
			if (!transform.TryGetComponent<Interactable>(out var interactable))
				return false;

			interactable.Interact();
			return true;
		}

		public static bool TryToInteract(this GameObject gameObject)
		{
			return TryToInteract(gameObject.transform);
		}
	}
}