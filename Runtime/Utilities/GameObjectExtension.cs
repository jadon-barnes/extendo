using System.Collections.Generic;
using UnityEngine;

namespace Extendo.Utilities
{
	public static class GameObjectExtension
	{
		public static T GetVisibleComponent<T>(this Transform transform, float maxDistance, float fieldOfView = 360f) where T : MonoBehaviour
		{
			var colliders = Physics.OverlapSphere(transform.position, maxDistance);

			float closestDistance = Mathf.Infinity;
			T closest = null;

			for (int i = 0; i < colliders.Length; i++)
			{
				// Get component
				if (!colliders[i].TryGetComponent<T>(out var component))
					continue;

				// In view
				if (!Math.InFieldOfView(transform.position, transform.forward, component.transform.position, fieldOfView))
					continue;

				// Get distance
				var distance = Vector3.Distance(component.transform.position, transform.position);

				// Check for closest
				if (distance < closestDistance)
				{
					closestDistance = distance;
					closest = component;
				}
			}

			return closest;
		}

		public static T GetVisibleComponent<T>(this GameObject gameObject, float maxDistance, float fieldOfView = 360f) where T : MonoBehaviour
		{
			return GetVisibleComponent<T>(gameObject.transform, maxDistance, fieldOfView);
		}

		public static T[] GetVisibleComponents<T>(this Transform transform, float maxDistance, float fieldOfView = 360f) where T : MonoBehaviour
		{
			var colliders = Physics.OverlapSphere(transform.position, maxDistance);

			List<T> found = new();

			for (int i = 0; i < colliders.Length; i++)
			{
				// Get component
				if (!colliders[i].TryGetComponent<T>(out var component))
					continue;

				// In view
				if (!Math.InFieldOfView(transform.position, transform.forward, component.transform.position, fieldOfView))
					continue;

				found.Add(component);
			}

			return found.ToArray();
		}

		public static T[] GetVisibleComponents<T>(this GameObject gameObject, float maxDistance, float fieldOfView = 360f) where T : MonoBehaviour
		{
			return GetVisibleComponents<T>(gameObject.transform, maxDistance, fieldOfView);
		}
	}
}