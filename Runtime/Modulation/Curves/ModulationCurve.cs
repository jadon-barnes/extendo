using Extendo.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Modulation.Curves
{
	[AddComponentMenu("Extendo/Modulation/Modulation Curve")]
	public class ModulationCurve : ModulationBehaviour
	{
		public Timer timer = new(5f, true);

		[Space] public FloatCurve   floatCurve   = new FloatCurve();
		public         Vector2Curve vector2Curve = new Vector2Curve();
		public         Vector3Curve vector3Curve = new Vector3Curve();

		public float   FloatValue   { get; private set; }
		public Vector2 Vector2Value { get; private set; }
		public Vector3 Vector3Value { get; private set; }

		[Space] public UnityEvent<float>   onUpdateFloat;
		public         UnityEvent<Vector2> onUpdateVector2;
		public         UnityEvent<Vector3> onUpdateVector3;

		protected override void OnEnable()
		{
			timer.Reset();
			base.OnEnable();
		}

		public void UpdateValue()
		{
			FloatValue   = floatCurve.GetValue(timer.TimeValue);
			Vector2Value = vector2Curve.GetValue(timer.TimeValue);
			Vector3Value = vector3Curve.GetValue(timer.TimeValue);

			onUpdateFloat.Invoke(FloatValue);
			onUpdateVector2.Invoke(Vector2Value);
			onUpdateVector3.Invoke(Vector3Value);

			timer.Update();
		}

		protected override void ManualUpdate()
		{
			UpdateValue();
		}
	}
}