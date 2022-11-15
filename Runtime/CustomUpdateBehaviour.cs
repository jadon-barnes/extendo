using System.Collections;
using UnityEngine;

namespace Extendo
{
	public abstract class CustomUpdateBehaviour : MonoBehaviour
	{
		[field: SerializeField]
		public  UpdateMethod Update { get; private set; } = UpdateMethod.Update;
		private Coroutine    updateRoutine;

		protected virtual void OnEnable()
		{
			if (Update != UpdateMethod.Manual)
				updateRoutine = StartCoroutine(UpdateRoutine());
		}

		protected virtual void OnDisable()
		{
			StopUpdateRoutine();
		}

		protected void StopUpdateRoutine()
		{
			if (updateRoutine != null)
				StopCoroutine(updateRoutine);
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

				yield return null;
			}
		}
	}
}