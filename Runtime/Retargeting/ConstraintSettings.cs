using System;
using UnityEngine;

namespace Extendo.Retargeting
{
	[Serializable]
	public class ConstraintSettings
	{
		public bool    useLocal = true;
		public Vector3 offset;
		public bool    enableX = true;
		public bool    enableY = true;
		public bool    enableZ = true;
	}
}