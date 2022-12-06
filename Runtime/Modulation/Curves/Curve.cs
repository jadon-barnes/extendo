using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Modulation.Curves
{
	public abstract class Curve<T>
	{
		public T offset;
		public T scale;

		protected AnimationCurve DefaultCurve =>
			new(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));

		public abstract T GetValue(float time);
	}
}