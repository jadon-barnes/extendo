using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Extendo.Components
{
	[DefaultExecutionOrder(-10000)]
	public class Categorization : MonoBehaviour
	{
		private static Dictionary<Transform, HashSet<Category>> transformDictionary = new ();
		private        Transform[]                              cachedTransforms    = new Transform[0];

		public List<Category> categories = new List<Category>();
		[Tooltip("Excludes the transform and it's children.")]
		public List<Transform> exclude = new List<Transform>();

		public static bool InCategory(Transform transform, params Category[] categories)
		{
			if (transformDictionary.TryGetValue(transform, out var values))
			{
				return values.Overlaps(categories);
			}

			return false;
		}

		public static bool InCategory(GameObject gameObject, params Category[] categories)
		{
			return InCategory(gameObject.transform, categories);
		}

		public static bool InCategory(Transform transform, params string[] categories)
		{
			if (transformDictionary.TryGetValue(transform, out var values))
			{
				foreach (var value in values)
				{
					foreach (var category in categories)
					{
						if (String.Equals(value.name, category, StringComparison.InvariantCultureIgnoreCase))
							return true;
					}
				}
			}

			return false;
		}

		public static bool InCategory(GameObject gameObject, params string[] categories)
		{
			return InCategory(gameObject.transform, categories);
		}

		public void Awake()
		{
			ReassignCategories();
		}

		[ContextMenu("Reassign Categories")]
		public void ReassignCategories()
		{
			foreach (var cachedTransform in cachedTransforms)
			{
				if (IsExcluded(cachedTransform))
					continue;

				RemoveCategories(cachedTransform, categories);
			}

			cachedTransforms = transform.GetComponentsInChildren<Transform>();

			foreach (var cachedTransform in cachedTransforms)
			{
				if (IsExcluded(cachedTransform))
					continue;

				AssignCategories(cachedTransform, categories);
			}
		}


		private void AssignCategories(Transform transform, List<Category> categories)
		{
			// Entry Exists
			if (transformDictionary.ContainsKey(transform))
			{
				// Assign categories
				transformDictionary[transform].UnionWith(categories);
				return;
			}

			// Entry Doesn't Exist
			transformDictionary.TryAdd(transform, new (categories));
		}

		private void RemoveCategories(Transform transform, List<Category> categories)
		{
			// Entry Exists
			if (transformDictionary.ContainsKey(transform))
			{
				// Remove Categories
				transformDictionary[transform].ExceptWith(categories);

				// If no categories assigned, delete from dictionary
				if (transformDictionary[transform].Count == 0)
					transformDictionary.Remove(transform);
			}
		}

		private bool IsExcluded(Transform transform)
		{
			// Loop through all excluded transforms
			foreach (var excludedTransform in exclude)
			{
				// Get children of each excluded transform
				Transform[] children = excludedTransform.GetComponentsInChildren<Transform>();

				foreach (var child in children)
				{
					if (child == transform)
						return true;
				}
			}

			return false;
		}
	}
}