using UnityEngine;

namespace Extendo.Interaction
{
	public static class InteractionExtension
	{
		public static bool TryToInteract<T>(this Transform transform) where T : IInteractable
		{
			T[] interactables = transform.GetComponents<T>();

			if (interactables.Length == 0)
				return false;

			foreach (var interactable in interactables)
			{
				interactable.OnInteract();
			}

			return true;
		}

		public static bool TryToInteract<T>(this GameObject gameObject) where T : IInteractable
		{
			return TryToInteract<T>(gameObject.transform);
		}

		public static bool TryToInteract(this Transform transform)
		{
			return TryToInteract<IInteractable>(transform);
		}

		public static bool TryToInteract(this GameObject gameObject)
		{
			return TryToInteract<IInteractable>(gameObject);
		}
	}
}