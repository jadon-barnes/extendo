using System;
using Extendo.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Math = Extendo.Utilities.Math;

namespace Extendo
{
	[AddComponentMenu("Extendo/Modulate")]
	public class Modulate : CustomUpdateBehaviour
	{
		public enum ModulationMethod
		{
			Sine        = 0,
			Cosine      = 1,
			PerlinNoise = 2,
		}

		[field: SerializeField]
		public float Result { get; private set; }
		public  ModulationMethod  modulationMethod = ModulationMethod.Sine;
		public  bool              resetOnDisable;
		private float             time;
		public  float             speed  = 1f;
		public  int               seed   = 12345;
		public  Vector2           remap  = new Vector2(0, 1);
		public  Vector2           cutoff = new Vector2(0, 1);
		public  UnityEvent<float> onNoiseUpdate;
		private float             Formula => time * speed + seed;

		protected override void OnDisable()
		{
			base.OnDisable();

			if (resetOnDisable)
				time = 0f;
		}

		protected override void OnUpdate()
		{
			UpdateModulation();
		}

		public void UpdateModulation()
		{
			time += Time.deltaTime * speed;

			switch (modulationMethod)
			{
				case ModulationMethod.Sine:
					Result = GetSine();
					break;
				case ModulationMethod.Cosine:
					Result = GetCosine();
					break;
				case ModulationMethod.PerlinNoise:
					Result = GetPerlinNoise();
					break;
				default: break;
			}

			onNoiseUpdate.Invoke(Result);
		}

		private float GetPerlinNoise()
		{
			var noise = Mathf.Clamp01(Mathf.PerlinNoise(Formula, Formula));
			var remapResult = Math.Remap(noise, new (0f, 1f), remap);
			return Mathf.Clamp(remapResult, cutoff.x, cutoff.y);
		}

		private float GetSine()
		{
			var sin = Mathf.Sin(Formula);
			var remapResult = Math.Remap(sin, new (-1f, 1f), remap);
			return Mathf.Clamp(remapResult, cutoff.x, cutoff.y);
		}

		private float GetCosine()
		{
			var cos = Mathf.Cos(Formula);
			var remapResult = Math.Remap(cos, new (-1f, 1f), remap);
			return Mathf.Clamp(remapResult, cutoff.x, cutoff.y);
		}
	}
}