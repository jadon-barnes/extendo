using UnityEngine;

namespace Extendo.Retargeting
{
	[AddComponentMenu("Extendo/Retargeting/Direct Follow Position")]
	public class DirectFollowPosition : FollowBehaviour
	{
		protected override Vector3 CalculateFollowPosition(Vector3 from, Vector3 to)
		{
			return to;
		}
	}
}