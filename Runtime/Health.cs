using UnityEngine;
using UnityEngine.Events;

namespace Extendo
{
	[AddComponentMenu("Extendo/Health")]
	public class Health : MonoBehaviour
	{
		[field: SerializeField]
		public int CurrentHealth { get; private set; } = 10;
		public bool            IsDefeated   => CurrentHealth <= 0;
		public bool            IsFullHealth => CurrentHealth >= maxHealth;
		public int             maxHealth = 10;
		public UnityEvent<int> onDamage;
		public UnityEvent<int> onHeal;
		public UnityEvent      onDefeat;
		public UnityEvent      onRevive;

		private void OnValidate() => SetHealth(CurrentHealth);

		[ContextMenu("Damage by 35% of Max Health")]
		private void Damage35Percent() => Damage((int)(maxHealth * 0.35f));

		[ContextMenu("Heal by 35% of Max Health")]
		private void Heal35Percent() => Heal((int)(maxHealth * 0.35f));

		// Sets health to a specific amount set with no event calls
		public void SetHealth(int value)
		{
			CurrentHealth = Mathf.Clamp(value, 0, maxHealth);
		}

		// Damage by an amount set
		public void Damage(int amount)
		{
			// Already defeated
			if (IsDefeated)
				return;

			// Set Health
			SetHealth(CurrentHealth - amount);

			// If Dead
			if (IsDefeated)
			{
				onDefeat.Invoke();
				return;
			}

			// Damage only
			onDamage.Invoke(amount);
		}

		// Defeat immediately
		[ContextMenu("Defeat")]
		public void Defeat()
		{
			// Check if already defeated
			if (IsDefeated)
				return;

			CurrentHealth = 0;
			onDefeat.Invoke();
		}

		// Heal by an amount set
		public void Heal(int amount)
		{
			// Already at full health
			if (IsFullHealth)
				return;

			// Set Health
			SetHealth(CurrentHealth + amount);

			onHeal.Invoke(amount);
		}

		// Revive to amount set. Won't revive if amount is set below minimum health.
		public void Revive(int setHealthAmount)
		{
			// Cancel if not defeated
			if (!IsDefeated)
				return;

			CurrentHealth = setHealthAmount;

			// Check to ensure is alive
			if (!IsDefeated)
				onRevive.Invoke();
		}
		
		// Revive at full health
		[ContextMenu("Revive")]
		public void Revive()
		{
			Revive(maxHealth);
		}

	}
}