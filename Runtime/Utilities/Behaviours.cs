using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Utilities
{
	public static class Behaviours
	{
		public static void Invoke(this MonoBehaviour monoBehaviour, UnityAction action, float time)
		{
			monoBehaviour.Invoke(action.Method.Name, time);
		}

		public static void InvokeRepeating(this MonoBehaviour monoBehaviour, UnityAction action, float time, float repeatRate)
		{
			monoBehaviour.InvokeRepeating(action.Method.Name, time, repeatRate);
		}

		public static bool IsInvoking(this MonoBehaviour monoBehaviour, UnityAction action)
		{
			return monoBehaviour.IsInvoking(action.Method.Name);
		}

		public static void CancelInvoke(this MonoBehaviour monoBehaviour, UnityAction action)
		{
			monoBehaviour.CancelInvoke(action.Method.Name);
		}
	}
}