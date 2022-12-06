using System;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Modulation.Composition
{
	[AddComponentMenu("Extendo/Modulation/Modulation Composer")]
	public class ModulationComposer : ModulationBehaviour
	{
		public float time;
		public float strength = 1f;

		[Space] public FloatModulator[]   floatModulators   = new[] { new FloatModulator() };
		public         Vector2Modulator[] vector2Modulators = new[] { new Vector2Modulator() };
		public         Vector3Modulator[] vector3Modulators = new[] { new Vector3Modulator() };

		[Space] public UnityEvent<float>   onUpdateFloat;
		public         UnityEvent<Vector2> onUpdateVector2;
		public         UnityEvent<Vector3> onUpdateVector3;

		public float   FloatValue   { get; protected set; }
		public Vector2 Vector2Value { get; protected set; }
		public Vector3 Vector3Value { get; protected set; }

		protected override void ManualUpdate()
		{
			FloatValue   = GetSumOfFloatModulations();
			Vector2Value = GetSumOfVector2Modulations();
			Vector3Value = GetSumOfVector3Modulations();

			time += Time.deltaTime;

			onUpdateFloat.Invoke(FloatValue);
			onUpdateVector2.Invoke(Vector2Value);
			onUpdateVector3.Invoke(Vector3Value);
		}

		public float GetSumOfFloatModulations()
		{
			var sum = 0f;

			foreach (FloatModulator modulation in floatModulators)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}

		public Vector2 GetSumOfVector2Modulations()
		{
			Vector2 sum = Vector2.zero;

			foreach (Vector2Modulator modulation in vector2Modulators)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}

		public Vector3 GetSumOfVector3Modulations()
		{
			Vector3 sum = Vector3.zero;

			foreach (Vector3Modulator modulation in vector3Modulators)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}
	}
}