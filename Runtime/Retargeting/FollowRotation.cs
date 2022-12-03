using System;
using UnityEngine;
using Math = Extendo.Utilities.Math;

namespace Extendo.Retargeting
{
	public abstract class FollowRotation : FollowBehaviour<Quaternion>
	{
		public         bool lookAt = false;
		[Space] public bool x;
		public         bool y;
		public         bool z;

		private Vector3 LookAtDirection => Math.Direction(transform.position, target.position);
		private Vector3 RelativeForward => transform.parent ? transform.parent.forward : Vector3.forward;
		private Vector3 RelativeUp      => transform.parent ? transform.parent.up : Vector3.up;

		Vector3 RotationTarget
		{
			get
			{
				var result = lookAt ? LookAtDirection : target.transform.forward;
				result.x = y ? RelativeForward.x : result.x;
				result.y = x ? RelativeForward.y : result.y;

				return result;
			}
		}

		protected Quaternion TargetValue =>
			Quaternion.LookRotation(RotationTarget, lookAt || z ? RelativeUp : target.transform.up)
			* Quaternion.Euler(offset);

		protected override void SetTransformValue(Quaternion targetValue)
		{
			transform.rotation = targetValue;
		}
	}
}