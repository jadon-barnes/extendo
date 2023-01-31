using UnityEngine;
using UnityEngine.Events;

namespace Extendo.FocusSystem
{
	public class FocusTarget : MonoBehaviour
	{
		public UnityEvent onFocus;
		public UnityEvent onLostFocus;
		public bool       InFocus => FocusDirector.InFocus(this);

		public FocusDirector GetFocusDirector()
		{
			return FocusDirector.GetDirectorFrom(this);
		}
	}
}