using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Direct Follow Position")]
	public class DirectFollowPosition : FollowPosition
	{
		protected override Vector3 CalculateFollowValue()
		{
			return target.position;
		}
	}
}