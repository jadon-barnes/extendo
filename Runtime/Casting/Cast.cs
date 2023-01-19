using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Color = UnityEngine.Color;

namespace Extendo.Casting
{
	public abstract class Cast : MonoBehaviour
	{
		protected Vector3 Position  => transform.position;
		protected Vector3 Direction => transform.forward;
		protected Ray     Ray       => new(Position, Direction);

		public UnityEvent<RaycastHit> onHit = new();
		public UnityEvent<Vector3>    hitPosition;

		public bool runOnEnable = true;
		public bool useFixedUpdate;

		[Tooltip("A value of 0 will update the component every FixedUpdate() or Update().")]
		public float updateDelay = 0f;

		public             bool  drawGizmos = true;
		protected readonly Color colorHit   = Color.red;
		protected readonly Color colorMiss  = Color.green;
		protected          float MaxVisualRayLength => float.IsInfinity(maxDistance) ? 9999999f : maxDistance;

		[Space] public QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal;
		public         LayerMask               layerMask          = ~0;
		public         float                   maxDistance        = Mathf.Infinity;

		private RaycastHit hit;
		public  RaycastHit Hit                 => hit;
		public  bool       HitSomething        => Hit.collider;
		public  Vector3    HitDistancePosition => transform.position + transform.forward * (hit.collider ? hit.distance : MaxVisualRayLength);

		private void OnEnable()
		{
			if (runOnEnable)
				StartCoroutine(CastRoutine());
		}

		public void ManualUpdate()
		{
			DoCast(out hit);

			hitPosition.Invoke(HitDistancePosition);

			if (HitSomething)
				onHit.Invoke(Hit);
		}

		public abstract bool DoCast(out RaycastHit hit);

		private IEnumerator CastRoutine()
		{
			while (enabled)
			{
				ManualUpdate();

				if (Mathf.Abs(updateDelay) > 0.001f)
					yield return new WaitForSeconds(updateDelay);
				else
					yield return useFixedUpdate ? new WaitForFixedUpdate() : null;
			}
		}

		private void OnDrawGizmos()
		{
			if (!enabled)
				return;

			if (!drawGizmos)
				return;

			// Set position and rotation relative to transform
			Gizmos.matrix = transform.localToWorldMatrix;

			// Render miss first.
			Gizmos.color = colorMiss;
			// Line
			Gizmos.DrawRay(Vector3.zero, Vector3.forward * MaxVisualRayLength);
			// Shape
			DrawShape(MaxVisualRayLength);

			// Render Hits
			if (HitSomething)
			{
				Gizmos.color = colorHit;
				DrawShape(Hit.distance);
			}
		}

		protected abstract void DrawShape(float distance);
	}
}