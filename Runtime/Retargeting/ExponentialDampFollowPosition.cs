using Extendo.Utilities;
using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Exponential Damp Follow Position")]
	public class ExponentialDampFollowPosition : FollowPosition
	{
		public float smoothTime = 5f;

		protected override Vector3 CalculateFollowValue()
		{
			return Math.ExpDamp(transform.position, target.position, smoothTime);
		}
	}
}