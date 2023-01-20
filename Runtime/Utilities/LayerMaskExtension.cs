using UnityEngine;

namespace Extendo.Utilities
{
	public static class LayerMaskExtension
	{
		public static bool Contains(this LayerMask layerMask, int layer)
		{
			return (layerMask.value & (1 << layer)) != 0;
		}
	}
}