using System;
using UnityEngine;
using Math = Extendo.Utilities.Math;

namespace Extendo.Modulation
{
	[Serializable]
	public class Vector3Modulation : Modulation<Vector3>
	{
		public Vector3    speed = Vector3.one;
		public Vector3    offset;
		public Vector3Int seed = new (12345, 12346, 12347);
		[Space]
		public Vector3 remapMin;
		public Vector3 remapMax = Vector3.one;
		[Space]
		public Vector3 cutoffMin;
		public Vector3 cutoffMax = Vector3.one;

		protected override Vector3 CalculateModulation(ModulateDelegate method, float time, Vector3 seed, Vector3 remapMin, Vector3 remapMax, Vector3 cutoffMin, Vector3 cutoffMax)
		{
			Vector3 timeValue = Vector3.Scale((Vector3.one * time) + offset, speed);
			;

			return new Vector3
			(
				method(timeValue.x, seed.x, new (remapMin.x, remapMax.x), new (cutoffMin.x, cutoffMax.x)),
				method(timeValue.y, seed.y, new (remapMin.y, remapMax.y), new (cutoffMin.y, cutoffMax.y)),
				method(timeValue.z, seed.z, new (remapMin.z, remapMax.z), new (cutoffMin.z, cutoffMax.z))
			);
		}

		protected override Vector3 GetSine(float time)
		{
			return CalculateModulation(Math.ModulateSine, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected override Vector3 GetCosine(float time)
		{
			return CalculateModulation(Math.ModulateCosine, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected override Vector3 GetLinear(float time)
		{
			return CalculateModulation(Math.ModulateLinear, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected override Vector3 GetPerlinNoise(float time)
		{
			return CalculateModulation(Math.ModulatePerlinNoise, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected override Vector3 GetBounce(float time)
		{
			return CalculateModulation(Math.ModulateBounce, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}
	}
}