using Extendo.CustomUpdates;
using UnityEngine;
using UnityEngine.Events;
using Math = Extendo.Utilities.Math;

namespace Extendo.Oscillators
{
	public abstract class Oscillator<T> : TimeBehaviour
	{
		public enum OscillationMethod
		{
			PerlinNoise = 0,
			Linear      = 1,
			Sine        = 2,
			Cosine      = 3,
			Bounce      = 4,
		}

		[Space]
		public OscillationMethod oscillationMethod = OscillationMethod.Sine;
		public float            strength         = 1f;
		[Space]
		public T speed;
		public T offset;
		[Space]
		public T from;
		public T to;
		[Space]
		public T cutoffFrom;
		public T cutoffTo;
		[Space]
		public UnityEvent<T> onUpdate;
		public T Value { get; private set; }


		public override void ManualUpdate()
		{
			Value = Evaluate(time);
			onUpdate.Invoke(Value);
			base.ManualUpdate();
		}

		public T Evaluate(float time)
		{
			switch (oscillationMethod)
			{
				case OscillationMethod.Sine:        return GetSine(time);
				case OscillationMethod.Cosine:      return GetCosine(time);
				case OscillationMethod.Linear:      return GetLinear(time);
				case OscillationMethod.PerlinNoise: return GetPerlinNoise(time);
				case OscillationMethod.Bounce:      return GetBounce(time);
				default:                           return default;
			}
		}

		protected delegate float OscillationDelegate(float time, Vector2 remap, Vector2 cutoff);

		protected abstract T GetOscillationValue(OscillationDelegate method, float time, T remapMin, T remapMax, T cutoffMin, T cutoffMax);

		protected T GetSine(float time)
		{
			return GetOscillationValue(Math.OscillateSine, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetCosine(float time)
		{
			return GetOscillationValue(Math.OscillateCosine, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetLinear(float time)
		{
			return GetOscillationValue(Math.OscillateLinear, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetPerlinNoise(float time)
		{
			return GetOscillationValue(Math.OscillatePerlinNoise, time, from, to, cutoffFrom, cutoffTo);
		}

		protected T GetBounce(float time)
		{
			return GetOscillationValue(Math.OscillateBounce, time, from, to, cutoffFrom, cutoffTo);
		}
	}
}