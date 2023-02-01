using System.Collections.Generic;
using UnityEngine;

namespace Extendo.FocusSystem
{
	public class FocusDirector : MonoBehaviour
	{
		private static readonly List<FocusDirector> focusDirectors = new();
		public                  FocusTarget         initialFocusTarget;
		public                  FocusTarget         FocusTarget { get; private set; }

		private void Awake()
		{
			focusDirectors.Add(this);
			ChangeFocus(initialFocusTarget);
		}

		private void OnDestroy()
		{
			focusDirectors.Remove(this);
			RemoveFocus();
		}

		public void ChangeFocus(FocusTarget focusTarget)
		{
			if (!focusTarget)
				return;

			// Already in focus
			if (focusTarget == this.FocusTarget)
				return;

			RemoveFocus();

			// Set Focus
			this.FocusTarget = focusTarget;
			this.FocusTarget.onFocus.Invoke();
		}

		public void ChangeFocusIfAvailable(FocusTarget focusTarget)
		{
			if (!focusTarget)
				return;

			if (focusTarget.InFocus)
				return;

			ChangeFocus(focusTarget);
		}

		private void RemoveFocus()
		{
			if (!FocusTarget)
				return;

			FocusTarget.onLostFocus.Invoke();
			FocusTarget = null;
		}

		public static bool InFocus(FocusTarget focusTarget)
		{
			for (int i = 0; i < focusDirectors.Count; i++)
				if (focusDirectors[i].FocusTarget == focusTarget)
					return true;

			return false;
		}

		public static FocusDirector GetDirectorFrom(FocusTarget focusTarget)
		{
			for (int i = 0; i < focusDirectors.Count; i++)
				if (focusDirectors[i].FocusTarget == focusTarget)
					return focusDirectors[i];

			return null;
		}
	}
}