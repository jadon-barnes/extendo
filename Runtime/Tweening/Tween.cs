using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Tweening
{
	public abstract class Tween<T>
	{
		protected MonoBehaviour monoBehaviour;
		public  bool          useFixedUpdate;
		public  int           repeat = 0;
		public  int           RepeatCount { get; private set; }
		private Coroutine     tweenCoroutine;
		private float         time;
		public  bool          resetOnStop = true;

		public abstract Tween<T> Move();

		public Tween<T> Ease()
		{
			return this;
		}

		public Tween<T> ResetOnStop(bool value)
		{
			this.resetOnStop = value;
			return this;
		}

		public Tween<T> UseFixedUpdate(bool useFixedUpdate = true)
		{
			this.useFixedUpdate = useFixedUpdate;
			return this;
		}

		public Tween<T> OnComplete(UnityAction unityAction)
		{
			unityAction.Invoke();
			return this;
		}

		public Tween<T> OnCompleteRepeat(UnityAction unityAction)
		{
			unityAction.Invoke();
			return this;
		}

		public Tween<T> Loop(int count)
		{
			repeat = count;
			return this;
		}

		public Tween<T> Stop()
		{
			if (tweenCoroutine != null)
				monoBehaviour.StopCoroutine(tweenCoroutine);

			if (resetOnStop)
				time = 0f;

			return this;
		}

		public Tween<T> Start()
		{
			Stop();
			tweenCoroutine = monoBehaviour.StartCoroutine(TweenRoutine());
			return this;
		}

		private IEnumerator TweenRoutine()
		{
			RepeatCount = repeat;

			while (RepeatCount >= 0 || repeat == -1)
			{
				if (CalculateTween())
				{
					RepeatCount = Mathf.Max(RepeatCount - 1, 0);
				}

				time += Time.deltaTime;

				yield return useFixedUpdate ? new WaitForFixedUpdate() : null;
			}

			tweenCoroutine = null;
			yield return null;
		}

		protected abstract bool CalculateTween();
	}
}