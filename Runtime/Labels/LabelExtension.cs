using UnityEngine;

namespace Extendo.Labels
{
	public static class LabelExtension
	{
		public static bool HasLabel(this Transform transform, params Label[] categories)
		{
			return Labeler.HasLabel(transform, categories);
		}

		public static bool HasLabel(this Transform transform, params string[] categories)
		{
			return Labeler.HasLabel(transform, categories);
		}

		public static bool HasLabel(this GameObject gameObject, params Label[] categories)
		{
			return Labeler.HasLabel(gameObject.transform, categories);
		}

		public static bool HasLabel(this GameObject gameObject, params string[] categories)
		{
			return Labeler.HasLabel(gameObject.transform, categories);
		}
	}
}