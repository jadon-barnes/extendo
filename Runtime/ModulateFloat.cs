using System;
using Extendo.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Math = Extendo.Utilities.Math;

namespace Extendo
{
	[AddComponentMenu("Extendo/Modulate Float")]
	public class ModulateFloat : ModulateBehaviour<float>
	{
		public float speed = 1f;
		public float offset;
		public int   seed = 12345;
		[Space]
		public Vector2 remap = new Vector2(0, 1);
		public Vector2 cutoff = new Vector2(0, 1);

		protected override float GetValueFromTime(float time) => (time + offset) * speed;

		protected override float Modulate(ModulateDelegate method, float time, float seed, float remapMin, float remapMax, float cutoffMin, float cutoffMax)
		{
			return method(GetValueFromTime(time), seed, new (remapMin, remapMax), new (cutoffMin, cutoffMax));
		}

		protected override float GetSine(float time)
		{
			return Modulate(Math.ModulateSine, time, seed, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		protected override float GetCosine(float time)
		{
			return Modulate(Math.ModulateCosine, time, seed, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		protected override float GetLinear(float time)
		{
			return Modulate(Math.ModulateLinear, time, seed, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		protected override float GetPerlinNoise(float time)
		{
			return Modulate(Math.ModulatePerlinNoise, time, seed, remap.x, remap.y, cutoff.x, cutoff.y);
		}

		protected override float GetBounce(float time)
		{
			return Modulate(Math.ModulateBounce, time, seed, remap.x, remap.y, cutoff.x, cutoff.y);
		}
	}
}