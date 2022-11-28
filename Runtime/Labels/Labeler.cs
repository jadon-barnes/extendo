using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extendo.Labels
{
	[AddComponentMenu("Extendo/Labeler")]
	[DefaultExecutionOrder(-10000)]
	public class Labeler : MonoBehaviour
	{
		private static Dictionary<Transform, HashSet<Label>> transformDictionary = new();
		private        Transform[]                           cachedTransforms    = new Transform[0];

		public List<Label> labels = new();
		[Tooltip("Excludes the transform and it's children.")]
		public List<Transform> exclude = new();

		public static bool HasLabel(Transform transform, params Label[] labels)
		{
			if (transformDictionary.TryGetValue(transform, out HashSet<Label> values))
				return values.Overlaps(labels);

			return false;
		}

		public static bool HasLabel(GameObject gameObject, params Label[] labels)
		{
			return HasLabel(gameObject.transform, labels);
		}

		public static bool HasLabel(Transform transform, params string[] labels)
		{
			if (transformDictionary.TryGetValue(transform, out HashSet<Label> values))
			{
				foreach (Label value in values)
				foreach (string category in labels)
					if (string.Equals(value.name, category, StringComparison.InvariantCultureIgnoreCase))
						return true;
			}

			return false;
		}

		public static bool HasLabel(GameObject gameObject, params string[] labels)
		{
			return HasLabel(gameObject.transform, labels);
		}

		public void Awake()
		{
			ReassignLabels();
		}

		[ContextMenu("Reassign Labels")]
		public void ReassignLabels()
		{
			foreach (Transform cachedTransform in cachedTransforms)
			{
				if (IsExcluded(cachedTransform))
					continue;

				RemoveCategories(cachedTransform, labels);
			}

			cachedTransforms = transform.GetComponentsInChildren<Transform>();

			foreach (Transform cachedTransform in cachedTransforms)
			{
				if (IsExcluded(cachedTransform))
					continue;

				AssignLabels(cachedTransform, labels);
			}
		}

		private void AssignLabels(Transform transform, List<Label> labels)
		{
			// Entry Exists
			if (transformDictionary.ContainsKey(transform))
			{
				// Assign categories
				transformDictionary[transform].UnionWith(labels);
				return;
			}

			// Entry Doesn't Exist
			transformDictionary.TryAdd(transform, new(labels));
		}

		private void RemoveCategories(Transform transform, List<Label> labels)
		{
			// Entry Exists
			if (transformDictionary.ContainsKey(transform))
			{
				// Remove Categories
				transformDictionary[transform].ExceptWith(labels);

				// If no categories assigned, delete from dictionary
				if (transformDictionary[transform].Count == 0)
					transformDictionary.Remove(transform);
			}
		}

		private bool IsExcluded(Transform transform)
		{
			// Loop through all excluded transforms
			foreach (Transform excludedTransform in exclude)
			{
				// Get children of each excluded transform
				Transform[] children = excludedTransform.GetComponentsInChildren<Transform>();

				foreach (Transform child in children)
					if (child == transform)
						return true;
			}

			return false;
		}
	}
}