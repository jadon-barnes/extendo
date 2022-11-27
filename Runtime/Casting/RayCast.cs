using UnityEngine;

namespace Extendo.Casting
{
	public class RayCast : Cast
	{
		protected override bool CastDefault(ref RaycastHit hit)
		{
			return Physics.Raycast
			(
				Ray,
				out hit,
				maxDistance,
				layerMask,
				triggerInteraction
			);
		}

		protected override int CastAll(ref RaycastHit[] hits)
		{
			return Physics.RaycastNonAlloc
			(
				Ray,
				hits,
				maxDistance,
				layerMask,
				triggerInteraction
			);
		}

		protected override void DrawShape(float distance)
		{
			Gizmos.DrawRay(Vector3.zero, Vector3.forward * distance);
		}
	}
}