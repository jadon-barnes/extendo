using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Direct Follow Rotation")]
	public class DirectFollowRotation : FollowRotation
	{
		public bool lockX, lockY, lockZ;

		private Vector3 LocalForward => transform.parent ? transform.parent.forward : Vector3.forward;
		private Vector3 LocalUp      => transform.parent ? transform.parent.up : Vector3.up;

		protected override Quaternion CalculateFollowValue()
		{
			var targetRotation = target.transform.forward;
			targetRotation.x = lockY ? LocalForward.x : targetRotation.x;
			targetRotation.y = lockX ? LocalForward.y : targetRotation.y;

			return Quaternion.LookRotation(targetRotation, lockZ ? LocalUp : target.transform.up);
		}
	}
}