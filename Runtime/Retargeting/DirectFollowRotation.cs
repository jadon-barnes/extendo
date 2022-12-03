using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Direct Follow Rotation")]
	public class DirectFollowRotation : FollowRotation
	{
		protected override Quaternion CalculateFollowValue()
		{
			return TargetValue;
		}
	}
}