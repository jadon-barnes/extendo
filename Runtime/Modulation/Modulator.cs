using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Math = Extendo.Utilities.Math;

namespace Extendo.Modulation
{
	public abstract class Modulator<T> : CustomUpdateBehaviour
	{
		public enum ModulationMethod
		{
			PerlinNoise = 0,
			Linear      = 1,
			Sine        = 2,
			Cosine      = 3,
			Bounce      = 4,
		}

		public                   bool             resetTimeOnDisable;
		public                   ModulationMethod modulationMethod = ModulationMethod.Sine;
		[HideInInspector] public float            time;
		public                   float            strength = 1f;
		[Header("Timing")]
		public T speed;
		public T offset;
		[Header("Targets")]
		public T from;
		public T to;
		[Header("Cutoff")]
		public T cutoffFrom;
		public T cutoffTo;
		[Space]
		public UnityEvent<T> onUpdate;
		public T Value { get; private set; }

		protected override void OnDisable()
		{
			base.OnDisable();

			if (resetTimeOnDisable)
				Reset();
		}

		protected override void OnUpdate()
		{
			Value =  Evaluate(time);
			time  += Time.deltaTime;
			onUpdate.Invoke(Value);
		}

		public void Reset()
		{
			time = 0f;
		}

		public T Evaluate(float time)
		{
			switch (modulationMethod)
			{
				case ModulationMethod.Sine:        return GetSine(time);
				case ModulationMethod.Cosine:      return GetCosine(time);
				case ModulationMethod.Linear:      return GetLinear(time);
				case ModulationMethod.PerlinNoise: return GetPerlinNoise(time);
				case ModulationMethod.Bounce:      return GetBounce(time);
				default:                           return default;
			}
		}

		protected delegate float ModulateDelegate(float time, Vector2 remap, Vector2 cutoff);

		protected abstract T GetModulationValue(ModulateDelegate method, float time, T remapMin, T remapMax, T cutoffMin, T cutoffMax);

		protected T GetSine(float time)
		{
			return GetModulationValue(Math.ModulateSine, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetCosine(float time)
		{
			return GetModulationValue(Math.ModulateCosine, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetLinear(float time)
		{
			return GetModulationValue(Math.ModulateLinear, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetPerlinNoise(float time)
		{
			return GetModulationValue(Math.ModulatePerlinNoise, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetBounce(float time)
		{
			return GetModulationValue(Math.ModulateBounce, time, from, to, cutoffFrom, cutoffTo);
		}
	}
}