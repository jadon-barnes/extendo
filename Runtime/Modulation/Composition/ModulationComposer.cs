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

		[Space] public ModulatorFloat[]   floatModulations   = new[] { new ModulatorFloat() };
		public         ModulatorVector2[] vector2Modulations = new[] { new ModulatorVector2() };
		public         ModulatorVector3[] vector3Modulations = new[] { new ModulatorVector3() };

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

			foreach (ModulatorFloat modulation in floatModulations)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}

		public Vector2 GetSumOfVector2Modulations()
		{
			Vector2 sum = Vector2.zero;

			foreach (ModulatorVector2 modulation in vector2Modulations)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}

		public Vector3 GetSumOfVector3Modulations()
		{
			Vector3 sum = Vector3.zero;

			foreach (ModulatorVector3 modulation in vector3Modulations)
				sum += modulation.Evaluate(time);

			return sum * strength;
		}
	}
}