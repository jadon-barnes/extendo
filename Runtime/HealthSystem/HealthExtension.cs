using UnityEngine;

namespace Extendo.HealthSystem
{
	public static class HealthExtension
	{
		public static bool TryDamage(this Transform transform, int amount)
		{
			if (!transform.TryGetComponent<Health>(out var health))
				return false;

			health.Damage(amount);
			return true;
		}

		public static bool TryDamage(this GameObject gameObject, int amount)
		{
			return TryDamage(gameObject.transform, amount);
		}
	}
}