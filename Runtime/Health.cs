using UnityEngine;
using UnityEngine.Events;

namespace Extendo
{
	[AddComponentMenu("Extendo/Health")]
	public class Health : MonoBehaviour
	{
		[field: SerializeField] public int             CurrentHealth { get; private set; } = 10;
		public                         bool            IsDefeated    => CurrentHealth <= 0;
		public                         bool            IsFullHealth  => CurrentHealth >= maxHealth;
		public                         int             maxHealth = 10;
		public                         UnityEvent<int> onDamage;
		public                         UnityEvent<int> onHeal;
		public                         UnityEvent      onDefeat;
		public                         UnityEvent      onRevive;

		private void OnValidate()
		{
			SetHealth(CurrentHealth); // Keep health within range.
		}

		[ContextMenu("Damage by 35% of Max Health")]
		private void Damage35Percent()
		{
			Damage((int)(maxHealth * 0.35f));
		}

		[ContextMenu("Heal by 35% of Max Health")]
		private void Heal35Percent()
		{
			Heal((int)(maxHealth * 0.35f));
		}

		/// <summary>
		/// Sets health to a specific value set with no event calls.
		/// </summary>
		public void SetHealth(int value)
		{
			CurrentHealth = Mathf.Clamp(value, 0, maxHealth);
		}

		/// <summary>
		/// Damages the health by an amount provided.
		/// </summary>
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

		/// <summary>
		/// Defeat immediately.
		/// </summary>
		[ContextMenu("Defeat")]
		public void Defeat()
		{
			// Check if already defeated
			if (IsDefeated)
				return;

			CurrentHealth = 0;
			onDefeat.Invoke();
		}

		/// <summary>
		/// Heal by the amount set.
		/// </summary>
		public void Heal(int amount)
		{
			// Already at full health
			if (IsFullHealth)
				return;

			// Set Health
			SetHealth(CurrentHealth + amount);

			onHeal.Invoke(amount);
		}

		/// <summary>
		/// Revive to the value set. Won't revive if amount is set below minimum health.
		/// </summary>
		public void Revive(int healthValue)
		{
			// Cancel if not defeated
			if (!IsDefeated)
				return;

			CurrentHealth = healthValue;

			// Check to ensure is alive
			if (!IsDefeated)
				onRevive.Invoke();
		}

		/// <summary>
		/// Revive to full health.
		/// </summary>
		[ContextMenu("Revive")]
		public void Revive()
		{
			Revive(maxHealth);
		}
	}
}