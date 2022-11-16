using UnityEngine;

namespace Extendo.Interaction
{
	public class SampleInteractable : MonoBehaviour, IInteractable
	{
		private void Start()
		{
			transform.TryToInteract();
		}

		public void OnInteract()
		{
			Debug.Log("Hello world!");
		}
	}
}