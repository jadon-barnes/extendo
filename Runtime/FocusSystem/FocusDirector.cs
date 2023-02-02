using System.Collections.Generic;
using UnityEngine;

namespace Extendo.FocusSystem
{
	public class FocusDirector : MonoBehaviour
	{
		private static readonly List<FocusDirector> focusDirectors = new();
		public                  FocusTarget         defaultFocusTarget;
		public                  FocusTarget         FocusTarget { get; private set; }

		public static bool InFocus(FocusTarget focusTarget)
		{
			for (int i = 0; i < focusDirectors.Count; i++)
				if (focusDirectors[i].FocusTarget == focusTarget)
					return true;

			return false;
		}

		public static FocusDirector GetDirectorFromTarget(FocusTarget focusTarget)
		{
			for (int i = 0; i < focusDirectors.Count; i++)
				if (focusDirectors[i].FocusTarget == focusTarget)
					return focusDirectors[i];

			return null;
		}

		protected virtual void Awake()
		{
			focusDirectors.Add(this);
			Switch(defaultFocusTarget);
		}

		protected virtual void OnDestroy()
		{
			focusDirectors.Remove(this);
			RemoveFocus();
		}

		public void Switch(FocusTarget focusTarget)
		{
			if (!focusTarget)
				return;

			// Already in focus
			if (this.FocusTarget == focusTarget)
				return;

			// Detach existing focus
			RemoveFocus();

			// Reset attached director to default
			var linkedDirector = focusTarget.GetFocusDirector();
			if (linkedDirector)
				linkedDirector.ResetToDefault();

			// Set Focus
			this.FocusTarget = focusTarget;

			// Events
			this.FocusTarget.OnDestroyGameObject += ResetToDefault;
			this.FocusTarget.onFocus.Invoke();
		}

		public void SwitchIfAvailable(FocusTarget focusTarget)
		{
			if (!focusTarget)
				return;

			if (InFocus(focusTarget))
				return;

			Switch(focusTarget);
		}

		private void RemoveFocus()
		{
			if (!FocusTarget)
				return;

			// Events
			FocusTarget.OnDestroyGameObject -= ResetToDefault;
			FocusTarget.onLostFocus.Invoke();

			// Remove Focus
			FocusTarget = null;
		}

		public void ResetToDefault()
		{
			Switch(defaultFocusTarget);
		}
	}
}