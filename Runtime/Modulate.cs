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
			PerlinNoise = 0,
			Linear      = 1,
			Sine        = 2,
			Cosine      = 3,
		}

		public  float            Result { get; private set; }
		public  ModulationMethod modulationMethod = ModulationMethod.Sine;
		public  bool             resetOnDisable;
		private float            time;
		[Space]
		public float speed = 1f;
		public float offset = 0f;
		public int   seed   = 12345;
		[Space]
		public Vector2 remap = new Vector2(0, 1);
		public Vector2 cutoff = new Vector2(0, 1);
		[Space]
		public UnityEvent<float> onUpdate;
		private float TimeFormula => (time + offset) * speed;

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
			
			time += Time.deltaTime;

			switch (modulationMethod)
			{
				case ModulationMethod.Sine:
					Result = Math.ModulateSine(TimeFormula, seed, remap, cutoff);
					break;
				case ModulationMethod.Cosine:
					Result = Math.ModulateCosine(TimeFormula, seed, remap, cutoff);
					break;
				case ModulationMethod.PerlinNoise:
					Result = Math.ModulatePerlinNoise(TimeFormula, seed, remap, cutoff);
					break;
				case ModulationMethod.Linear:
					Result = Math.ModulateLinear(TimeFormula, seed, remap, cutoff);
					break;
				default: break;
			}

			onUpdate.Invoke(Result);
		}
	}
}