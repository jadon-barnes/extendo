using System.Collections;
using UnityEngine;

namespace Extendo
{
	public abstract class CustomUpdateBehaviour : MonoBehaviour
	{
		[field: SerializeField]
		public  UpdateMethod Update { get; private set; } = UpdateMethod.Update;
		private Coroutine    customUpdateRoutine;

		protected virtual void OnEnable()
		{
			StartCustomUpdate();
		}

		protected virtual void OnDisable()
		{
			StopCustomUpdate();
		}

		protected void StartCustomUpdate()
		{
			if (Update != UpdateMethod.Manual)
				customUpdateRoutine = StartCoroutine(UpdateRoutine());
		}

		protected void StopCustomUpdate()
		{
			if (customUpdateRoutine != null)
				StopCoroutine(customUpdateRoutine);
		}

		protected abstract void OnUpdate();

		private IEnumerator UpdateRoutine()
		{
			while (true)
			{
				OnUpdate();

				switch (Update)
				{
					case UpdateMethod.FixedUpdate:
						yield return new WaitForFixedUpdate();
						break;
					default:
						yield return null;
						break;
				}
			}
		}
	}
}