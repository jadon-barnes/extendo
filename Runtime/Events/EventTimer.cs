using System.Collections;
using Extendo.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Events
{
	[AddComponentMenu("Extendo/Events/Event Timer")]
	public class EventTimer : MonoBehaviour
	{
		public                         bool       startOnEnable = true;
		public                         bool       resetOnEnable = true;
		[field: SerializeField] public Timer      Timer    { get; private set; } = new(5f);
		public                         bool       Counting { get; private set; }
		public                         UnityEvent onDurationReached;

		private void Awake()
		{
			Timer.onDurationReached = onDurationReached.Invoke;
		}

		private void OnEnable()
		{
			if (resetOnEnable)
				Timer.Reset();

			if (startOnEnable)
				Start();
		}

		private void OnDisable()
		{
			Stop();
		}

		private void Update()
		{
			if (Counting)
				Timer.Update();
		}

		[ContextMenu("Start")]
		public void Start()
		{
			Counting = true;
		}

		[ContextMenu("Stop")]
		public void Stop()
		{
			Counting = false;
		}
	}
}