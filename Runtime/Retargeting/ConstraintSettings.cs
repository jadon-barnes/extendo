using System;
using UnityEngine;

namespace Extendo.Retargeting
{
	[Serializable]
	public class ConstraintSettings
	{
		public bool    useLocalOffset = true;
		public Vector3 offset;
		public bool    enableX = true;
		public bool    enableY = true;
		public bool    enableZ = true;

		public Vector3 GetOffset(Transform transform)
		{
			return useLocalOffset ? transform.TransformPoint(offset) : offset;
		}
	}
}