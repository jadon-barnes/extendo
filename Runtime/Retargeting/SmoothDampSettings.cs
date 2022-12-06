using System;

namespace Extendo.Retargeting
{
	[Serializable]
	public class SmoothDampSettings
	{
		public float smoothTime = 0.5f;
		public float maxSpeed   = float.PositiveInfinity;
	}
}