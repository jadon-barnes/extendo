using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo
{
	public class Breakable : MonoBehaviour
	{
		public Transform original;
		public Transform broken;

		[Header("On Break")] public bool      linkBrokenToOriginal = true;
		public                      Rigidbody linkVelocity;
		[Header("On Fix")] public   bool      resetBrokenRigidbodies = true;
		public                      bool      resetOriginalVelocity  = true;

		private Rigidbody[]                       brokenRigidbodies;
		private Rigidbody[]                       originalRigidbodies;
		private Dictionary<Rigidbody, Vector3>    brokenRigidbodyLocalPositions = new();
		private Dictionary<Rigidbody, Quaternion> brokenRigidbodyLocalRotations = new();

		[Space] public UnityEvent<bool> onBreak;

		private void Awake()
		{
			TransformNullChecks();

			originalRigidbodies = original.GetComponentsInChildren<Rigidbody>();
			brokenRigidbodies   = broken.GetComponentsInChildren<Rigidbody>();

			if (resetBrokenRigidbodies)
				StoreBrokenRigidbodiesPositions();
		}

		private void TransformNullChecks()
		{
			if (!original)
				Debug.LogError("Original breakable object can't be null!");

			if (!broken)
				Debug.LogError("Broken breakable object can't be null!");
		}

		private void StoreBrokenRigidbodiesPositions()
		{
			for (int i = 0; i < brokenRigidbodies.Length; i++)
			{
				brokenRigidbodyLocalPositions.Add(brokenRigidbodies[i], brokenRigidbodies[i].transform.localPosition);
				brokenRigidbodyLocalRotations.Add(brokenRigidbodies[i], brokenRigidbodies[i].transform.localRotation);
			}
		}

		private void ResetBrokenRigidbodies()
		{
			if (!resetBrokenRigidbodies)
				return;

			for (int i = 0; i < brokenRigidbodies.Length; i++)
			{
				// Reset Velocity
				brokenRigidbodies[i].velocity        = Vector3.zero;
				brokenRigidbodies[i].angularVelocity = Vector3.zero;

				// Set Position
				if (brokenRigidbodyLocalPositions.TryGetValue(brokenRigidbodies[i], out var position))
					brokenRigidbodies[i].transform.localPosition = position;

				if (brokenRigidbodyLocalRotations.TryGetValue(brokenRigidbodies[i], out var rotation))
					brokenRigidbodies[i].transform.localRotation = rotation;
			}
		}

		private void ResetOriginalVelocities()
		{
			if (!resetOriginalVelocity)
				return;

			for (int i = 0; i < originalRigidbodies.Length; i++)
			{
				originalRigidbodies[i].velocity        = Vector3.zero;
				originalRigidbodies[i].angularVelocity = Vector3.zero;
			}
		}

		private void LinkBrokenRigidbodyVelocitiesToOriginal()
		{
			if (!linkVelocity)
				return;

			for (int i = 0; i < brokenRigidbodies.Length; i++)
			{
				brokenRigidbodies[i].velocity        = linkVelocity.velocity;
				brokenRigidbodies[i].angularVelocity = linkVelocity.angularVelocity;
			}
		}

		[ContextMenu("Break")]
		public void Break()
		{
			if (linkBrokenToOriginal)
				broken.SetPositionAndRotation(original.position, original.rotation);

			LinkBrokenRigidbodyVelocitiesToOriginal();

			original.gameObject.SetActive(false);
			broken.gameObject.SetActive(true);

			onBreak.Invoke(true);
		}

		[ContextMenu("Fix")]
		public void Fix()
		{
			ResetOriginalVelocities();

			original.gameObject.SetActive(true);
			broken.gameObject.SetActive(false);

			ResetBrokenRigidbodies();

			onBreak.Invoke(false);
		}
	}
}