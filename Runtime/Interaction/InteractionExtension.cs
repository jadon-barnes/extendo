using UnityEngine;

namespace Extendo.Interaction
{
	public static class InteractionExtension
	{
		public static bool TryInteract<T>(this GameObject gameObject) where T : IInteractable
		{
			T[] interactables = gameObject.GetComponents<T>();

			if (interactables.Length == 0)
				return false;

			foreach (T interactable in interactables)
				interactable.Interact();

			return true;
		}

		public static bool TryInteract<T>(this Transform transform) where T : IInteractable
		{
			return TryInteract<T>(transform.gameObject);
		}

		public static bool TryInteract(this GameObject gameObject)
		{
			return TryInteract<IInteractable>(gameObject);
		}

		public static bool TryInteract(this Transform transform)
		{
			return TryInteract<IInteractable>(transform);
		}
	}
}