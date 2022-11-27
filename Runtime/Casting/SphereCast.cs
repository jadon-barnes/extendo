using UnityEngine;

namespace Extendo.Casting
{
	[AddComponentMenu("Extendo/Casting/Sphere Cast")]
	public class SphereCast : Cast
	{
		[Space]
		public float radius = 0.5f;

		protected override bool CastDefault(ref RaycastHit hit)
		{
			return Physics.SphereCast
			(
				Ray,
				radius,
				out hit,
				maxDistance,
				layerMask,
				triggerInteraction
			);
		}

		protected override int CastAll(ref RaycastHit[] hits)
		{
			return Physics.SphereCastNonAlloc
			(
				Ray,
				radius,
				hits,
				maxDistance,
				layerMask,
				triggerInteraction
			);
		}

		protected override void DrawShape(float distance)
		{
			Gizmos.DrawWireSphere(Vector3.forward * distance, radius);
		}
	}
}