using System;

namespace Extendo.Retargeting
{
	[Serializable]
	public struct AxisBool
	{
		public AxisBool(bool x, bool y, bool z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public AxisBool(bool toggle)
		{
			x = y = z = toggle;
		}

		public bool x;
		public bool y;
		public bool z;
	}
}