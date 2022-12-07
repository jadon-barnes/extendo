using UnityEngine;

namespace Extendo.Casting
{
	[AddComponentMenu("Extendo/Casting/Box Cast")]
	public class BoxCast : Cast
	{
		[Space] public Vector3 size = Vector3.one;

		public override bool DoCast(out RaycastHit hit)
		{
			return Physics.BoxCast(
				Position,
				size * 0.5f,
				Direction,
				out hit,
				transform.rotation,
				maxDistance,
				layerMask,
				triggerInteraction
			);
		}

		protected override void DrawShape(float distance)
		{
			Gizmos.DrawWireCube(Vector3.forward * distance, size);
		}
	}
}