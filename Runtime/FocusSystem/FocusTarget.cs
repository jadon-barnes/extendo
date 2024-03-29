using UnityEngine;
using UnityEngine.Events;

namespace Extendo.FocusSystem
{
	[DefaultExecutionOrder(-1)]
	public class FocusTarget : MonoBehaviour
	{
		public UnityEvent        onFocus;
		public UnityEvent        onLostFocus;
		public event UnityAction OnDestroyGameObject;
		public bool              InFocus => FocusDirector.InFocus(this);

		public FocusDirector GetFocusDirector() => FocusDirector.GetDirectorFromTarget(this);
		public T GetFocusDirector<T>() where T : FocusDirector => FocusDirector.GetDirectorFromTarget(this) as T;

		protected virtual void Awake()
		{
			onFocus.AddListener(OnFocus);
			onLostFocus.AddListener(OnLostFocus);
		}

		protected virtual void OnDestroy()
		{
			onFocus.RemoveListener(OnFocus);
			onLostFocus.RemoveListener(OnLostFocus);
			OnDestroyGameObject?.Invoke();
		}

		protected virtual void OnFocus() { }
		protected virtual void OnLostFocus() { }
	}
}