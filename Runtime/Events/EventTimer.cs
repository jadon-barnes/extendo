using Extendo.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Events
{
	[AddComponentMenu("Extendo/Events/Event Timer")]
	public class EventTimer : MonoBehaviour
	{
		public bool resetOnEnable = true;
		[field: SerializeField]
		public Timer Timer { get; private set; } = new Timer(5f);
		public UnityEvent onDurationReached;

		private void Awake()
		{
			Timer.onDurationReached = onDurationReached.Invoke;
		}

		private void OnEnable()
		{
			if (resetOnEnable)
			{
				Timer.Reset();
			}
		}

		private void Update()
		{
			Timer.Update();
		}
	}
}