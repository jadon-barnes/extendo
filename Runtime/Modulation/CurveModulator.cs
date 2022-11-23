using Extendo.CustomUpdates;
using Extendo.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Modulation
{
	public abstract class CurveModulator<T> : CustomUpdate
	{
		public UnityEvent<T> onUpdate;
		public Timer         timer = new Timer(5f, true);
		public T             offset;
		public T             scale;
		public T             Value { get; private set; }

		protected override void OnEnable()
		{
			timer.Reset();
			base.OnEnable();
		}

		protected AnimationCurve DefaultCurve =>
			new AnimationCurve
			(
				new Keyframe(0f, 0f),
				new Keyframe(0.5f, 1f),
				new Keyframe(1f, 0f)
			);

		public void UpdateValue()
		{
			Value = GetValue();
			onUpdate.Invoke(Value);
			timer.Update();
		}

		protected override void ManualUpdate()
		{
			UpdateValue();
		}

		protected abstract T GetValue();
	}
}