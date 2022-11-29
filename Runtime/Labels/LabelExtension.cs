using UnityEngine;

namespace Extendo.Labels
{
	public static class LabelExtension
	{
		public static bool HasLabel(this Transform transform, params Label[] labels)
		{
			return Labeler.HasLabel(transform, labels);
		}

		public static bool HasLabel(this Transform transform, params string[] labels)
		{
			return Labeler.HasLabel(transform, labels);
		}

		public static bool HasLabel(this GameObject gameObject, params Label[] labels)
		{
			return Labeler.HasLabel(gameObject.transform, labels);
		}

		public static bool HasLabel(this GameObject gameObject, params string[] labels)
		{
			return Labeler.HasLabel(gameObject.transform, labels);
		}
	}
}