using Extendo.Utilities;
using UnityEngine;

namespace Extendo
{
	[AddComponentMenu("Extendo/Modulate Vector3")]
	public class ModulateVector3 : ModulateBehaviour<Vector3>
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

		protected override Vector3 GetValueFromTime(float time) => Vector3.Scale((Vector3.one * time) + offset, speed);

		protected override Vector3 Modulate(ModulateDelegate method, float time, Vector3 seed, Vector3 remapMin, Vector3 remapMax, Vector3 cutoffMin, Vector3 cutoffMax)
		{
			Vector3 timeValue = GetValueFromTime(time);

			return new Vector3
			(
				method(timeValue.x, seed.x, new (remapMin.x, remapMax.x), new (cutoffMin.x, cutoffMax.x)),
				method(timeValue.y, seed.y, new (remapMin.y, remapMax.y), new (cutoffMin.y, cutoffMax.y)),
				method(timeValue.z, seed.z, new (remapMin.z, remapMax.z), new (cutoffMin.z, cutoffMax.z))
			);
		}

		protected override Vector3 GetSine(float time)
		{
			return Modulate(Math.ModulateSine, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected override Vector3 GetCosine(float time)
		{
			return Modulate(Math.ModulateCosine, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected override Vector3 GetLinear(float time)
		{
			return Modulate(Math.ModulateLinear, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected override Vector3 GetPerlinNoise(float time)
		{
			return Modulate(Math.ModulatePerlinNoise, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}

		protected override Vector3 GetBounce(float time)
		{
			return Modulate(Math.ModulateBounce, time, seed, remapMin, remapMax, cutoffMin, cutoffMax);
		}
	}
}