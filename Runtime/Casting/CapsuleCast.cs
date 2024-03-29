using UnityEngine;

namespace Extendo.Casting
{
	[AddComponentMenu("Extendo/Casting/Capsule Cast")]
	public class CapsuleCast : Cast
	{
		[Space] public float radius = 0.5f;
		public         float height = 1f;

		protected void OnValidate()
		{
			height = Mathf.Max(0, height);
		}

		private Vector3 CapsuleSphereOffset => transform.up * height * 0.5f;
		private Vector3 TopSphereCapsule    => Position + CapsuleSphereOffset;
		private Vector3 BottomSphereCapsule => Position - CapsuleSphereOffset;

		public override bool DoCast(out RaycastHit hit)
		{
			return Physics.CapsuleCast(
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

		protected override void DrawShape(float distance)
		{
			Vector3 sphereTop = Vector3.up * height * 0.5f + Vector3.forward * distance;
			Vector3 sphereBottom = Vector3.down * height * 0.5f + Vector3.forward * distance;
			Gizmos.DrawWireSphere(sphereTop, radius);
			Gizmos.DrawWireSphere(sphereBottom, radius);

			Gizmos.DrawLine(sphereTop + Vector3.forward * radius, sphereBottom + Vector3.forward * radius);

			Gizmos.DrawLine(sphereTop - Vector3.forward * radius, sphereBottom - Vector3.forward * radius);

			Gizmos.DrawLine(sphereTop + Vector3.right * radius, sphereBottom + Vector3.right * radius);

			Gizmos.DrawLine(sphereTop - Vector3.right * radius, sphereBottom - Vector3.right * radius);
		}
	}
}