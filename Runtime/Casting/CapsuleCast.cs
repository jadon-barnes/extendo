using UnityEngine;

namespace Extendo.Casting
{
	[AddComponentMenu("Extendo/Casting/Capsule Cast")]
	public class CapsuleCast : Cast
	{
		[Space]
		public float radius = 0.5f;
		public float height = 1f;

		protected override void OnValidate()
		{
			base.OnValidate();
			height = Mathf.Max(0, height);
		}

		private Vector3 CapsuleSphereOffset => transform.up * height * 0.5f;
		private Vector3 TopSphereCapsule    => Position + CapsuleSphereOffset;
		private Vector3 BottomSphereCapsule => Position - CapsuleSphereOffset;

		protected override bool CastDefault(ref RaycastHit hit)
		{
			return Physics.CapsuleCast
			(
				TopSphereCapsule,
				BottomSphereCapsule,
				radius,
				Direction,
				out hit,
				maxDistance,
				layerMask,
				triggerInteraction
			);
		}

		protected override int CastAll(ref RaycastHit[] hits)
		{
			return Physics.CapsuleCastNonAlloc
			(
				TopSphereCapsule,
				BottomSphereCapsule,
				radius,
				Direction,
				hits,
				maxDistance,
				layerMask,
				triggerInteraction
			);
		}

		protected override void DrawShape(float distance)
		{
			var sphereTop = Vector3.up * height * 0.5f + (Vector3.forward * distance);
			var sphereBottom = Vector3.down * height * 0.5f + (Vector3.forward * distance);
			Gizmos.DrawWireSphere(sphereTop, radius);
			Gizmos.DrawWireSphere(sphereBottom, radius);

			Gizmos.DrawLine
			(
				sphereTop + Vector3.forward * radius,
				sphereBottom + Vector3.forward * radius
			);

			Gizmos.DrawLine
			(
				sphereTop - Vector3.forward * radius,
				sphereBottom - Vector3.forward * radius
			);

			Gizmos.DrawLine
			(
				sphereTop + Vector3.right * radius,
				sphereBottom + Vector3.right * radius
			);

			Gizmos.DrawLine
			(
				sphereTop - Vector3.right * radius,
				sphereBottom - Vector3.right * radius
			);
		}
	}
}