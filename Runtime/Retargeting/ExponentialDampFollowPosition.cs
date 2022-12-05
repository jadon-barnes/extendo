using Extendo.Utilities;
using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Exponential Damp Follow Position")]
	public class ExponentialDampFollowPosition : FollowBehaviour
	{
		public float smoothTime = 5f;

		protected override Vector3 CalculateFollowPosition(Vector3 from, Vector3 to)
		{
			return Math.ExpDamp(from, to, smoothTime);
		}
	}
}