using UnityEngine;

namespace Extendo.Casting
{
	[AddComponentMenu("Extendo/Casting/Ray Cast")]
	public class RayCast : Cast
	{
		public override bool DoCast(out RaycastHit hit)
		{
			return Physics.Raycast(Ray, out hit, maxDistance, layerMask, triggerInteraction);
		}

		protected override void DrawShape(float distance)
		{
			Gizmos.DrawRay(Vector3.zero, Vector3.forward * distance);
		}
	}
}