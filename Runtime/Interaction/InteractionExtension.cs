using UnityEngine;

namespace Extendo.Interaction
{
	public static class InteractionExtension
	{
		public static bool TryInteract<T>(this Transform transform) where T : IInteractable
		{
			T[] interactables = transform.GetComponents<T>();

			if (interactables.Length == 0)
				return false;

			foreach (T interactable in interactables)
				interactable.OnInteract();

			return true;
		}

		public static bool TryInteract<T>(this GameObject gameObject) where T : IInteractable
		{
			return TryInteract<T>(gameObject.transform);
		}

		public static bool TryInteract(this Transform transform)
		{
			return TryInteract<IInteractable>(transform);
		}

		public static bool TryInteract(this GameObject gameObject)
		{
			return TryInteract<IInteractable>(gameObject);
		}
	}
}