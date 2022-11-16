using System;
using UnityEngine;
using Math = Extendo.Utilities.Math;

namespace Extendo.Modulation
{
	[Serializable]
	public class FloatModulation : Modulation<float>
	{
		public float speed = 1f;
		public float offset;
		public int   seed = 12345;
		[Space]
		public Vector2 remap = new Vector2(0, 1);
		public Vector2 cutoff = new Vector2(0, 1);

		protected override float CalculateModulation
		(
			ModulateDelegate method,
			float time,
			float seed,
			float remapMin,
			float remapMax,
			float cutoffMin,
			float cutoffMax
		)
		{
			float timeValue = (time + offset) * speed;
			return method(timeValue, seed, new (remapMin, remapMax), new (cutoffMin, cutoffMax));
		}

		protected override float GetSine(float time)
		{
			return CalculateModulation
			(
				Math.ModulateSine,
				time,
				seed,
				remap.x,
				remap.y,
				cutoff.x,
				cutoff.y
			);
		}

		protected override float GetCosine(float time)
		{
			return CalculateModulation
			(
				Math.ModulateCosine,
				time,
				seed,
				remap.x,
				remap.y,
				cutoff.x,
				cutoff.y
			);
		}

		protected override float GetLinear(float time)
		{
			return CalculateModulation
			(
				Math.ModulateLinear,
				time,
				seed,
				remap.x,
				remap.y,
				cutoff.x,
				cutoff.y
			);
		}

		protected override float GetPerlinNoise(float time)
		{
			return CalculateModulation
			(
				Math.ModulatePerlinNoise,
				time,
				seed,
				remap.x,
				remap.y,
				cutoff.x,
				cutoff.y
			);
		}

		protected override float GetBounce(float time)
		{
			return CalculateModulation
			(
				Math.ModulateBounce,
				time,
				seed,
				remap.x,
				remap.y,
				cutoff.x,
				cutoff.y
			);
		}
	}
}