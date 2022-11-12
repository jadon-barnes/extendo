using UnityEngine;

namespace Extendo
{
	public static class CategoryExtension
	{
		public static bool InCategory(this Transform transform, params Category[] categories)
		{
			return Categorization.InCategory(transform, categories);
		}

		public static bool InCategory(this Transform transform, params string[] categories)
		{
			return Categorization.InCategory(transform, categories);
		}

		public static bool InCategory(this GameObject gameObject, params Category[] categories)
		{
			return Categorization.InCategory(gameObject.transform, categories);
		}

		public static bool InCategory(this GameObject gameObject, params string[] categories)
		{
			return Categorization.InCategory(gameObject.transform, categories);
		}
	}
}