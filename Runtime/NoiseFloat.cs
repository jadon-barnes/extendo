using Extendo.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Extendo
{
	[AddComponentMenu("Extendo/Noise Float")]
	public class NoiseFloat : CustomUpdateBehaviour
	{
		public  bool              resetOnDisable;
		private float             time;
		public  float             speed       = 1f;
		public  int               seed        = 12345;
		public  Vector2           input       = new Vector2(0, 1);
		public  Vector2           output      = new Vector2(0, 1);
		public  Vector2           clampResult = new Vector2(0, 1);
		public  UnityEvent<float> onNoiseUpdate;
		public  float             Noise         { get; private set; }
		public  float             NoiseRemapped { get; private set; }
		public  float             NoiseResult   { get; private set; }

		protected override void OnDisable()
		{
			base.OnDisable();

			if (resetOnDisable)
				time = 0f;
		}

		protected override void OnUpdate()
		{
			UpdateNoise();
		}

		private void UpdateNoise()
		{
			time          += Time.deltaTime * speed;
			Noise         =  Mathf.PerlinNoise(time, seed);
			NoiseRemapped =  Math.Remap(Noise, input, output);

			NoiseResult = Mathf.Clamp(NoiseRemapped, clampResult.x, clampResult.y);

			onNoiseUpdate.Invoke(NoiseResult);
		}
	}
}