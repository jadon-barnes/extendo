using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Color = UnityEngine.Color;

namespace Extendo.Casting
{
	public abstract class Cast : MonoBehaviour
	{
		protected Vector3 Position  => transform.position;
		protected Vector3 Direction => transform.forward;
		protected Ray     Ray       => new Ray(Position, Direction);

		public UnityEvent<RaycastHit>   onHit  = new UnityEvent<RaycastHit>();
		public UnityEvent<RaycastHit[]> onHits = new UnityEvent<RaycastHit[]>();

		public bool  useFixedUpdate;
		public float updateDelay = 0f;

		public             bool  drawGizmos = true;
		protected readonly Color colorHit   = Color.red;
		protected readonly Color colorMiss  = Color.green;
		protected          float GizmoRayLength => (float.IsInfinity(maxDistance) ? 9999999f : maxDistance);

		[Space]
		public bool castMultiple = false;
		public QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal;
		public LayerMask               layerMask          = ~0;
		public int                     maxCollisions      = 10;
		public float                   maxDistance        = Mathf.Infinity;

		protected RaycastHit[]                  hits;
		public    int                           HitCount     { get; protected set; }
		public    bool                          HitSomething => HitCount > 0;
		public    Dictionary<int, RaycastHit[]> hitArrays = new ();

		protected virtual void OnValidate()
		{
			maxCollisions = Mathf.Max(1, maxCollisions);
		}

		private void Awake()
		{
			hits = new RaycastHit[maxCollisions];
		}

		private void OnEnable()
		{
			StartCoroutine(CastRoutine());
		}

		private void OnDisable()
		{
			StopCoroutine(CastRoutine());
			HitCount = 0;
		}

		private void Calculate()
		{
			if (castMultiple)
				CastMultiple();
			else
				CastSingle();
		}

		private void CastSingle()
		{
			HitCount = CastDefault(ref hits[0]);

			if (HitSomething)
				onHit.Invoke(hits[0]);
		}

		private void CastMultiple()
		{
			HitCount = CastAll(ref hits);

			if (!HitSomething)
				return;

			// Add array size to dictionary if doesn't exist.
			if (!hitArrays.ContainsKey(HitCount))
				hitArrays.Add(HitCount, new RaycastHit[HitCount]);

			// To avoid GC Alloc, arrays (the size of the hitcount) are created and stored in a dictionary to be reused.
			for (int i = 0; i < HitCount; i++)
				hitArrays[HitCount][i] = hits[i];

			onHits.Invoke(hitArrays[HitCount]);
		}

		protected abstract int CastDefault(ref RaycastHit hit);

		protected abstract int CastAll(ref RaycastHit[] hits);

		IEnumerator CastRoutine()
		{
			while (true)
			{
				Calculate();

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

			Gizmos.matrix = transform.localToWorldMatrix;
			
			// Render miss first.
			Gizmos.color = colorMiss;
			// Line
			Gizmos.DrawRay(Vector3.zero, Vector3.forward * GizmoRayLength);
			// Shape
			DrawShape(GizmoRayLength);

			Gizmos.color = colorHit;

			// Render Hits
			if (castMultiple)
			{
				for (int i = 0; i < HitCount; i++)
					DrawShape(hits[i].distance);
			}
			else
			{
				if (HitSomething)
					DrawShape(hits[0].distance);
			}
		}

		protected abstract void DrawShape(float distance);
	}
}