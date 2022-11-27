using UnityEngine;

namespace Extendo.Casting
{
	public class SphereCast : Cast
	{
		[Space]
		public float radius = 0.5f;

		protected override int CastDefault(ref RaycastHit hit)
		{
			return Physics.SphereCast
			(
				Ray,
				radius,
				out hit,
				maxDistance,
				layerMask,
				triggerInteraction
			)
				? 1
				: 0;
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