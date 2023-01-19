using UnityEngine;

namespace Extendo.Audio
{
	[RequireComponent(typeof(AudioSource))]
	[AddComponentMenu("Extendo/Multi Clip Player")]
	public class MultiClipPlayer : MonoBehaviour
	{
		private AudioSource audioSource;
		public  AudioClip[] clips;
		public  Vector2     volumeVariation = Vector3.one;
		public  Vector2     pitchVariation  = Vector2.one;

		private int clipIndex = 0;

		public bool playSequentially;

		private void Awake()
		{
			audioSource = GetComponent<AudioSource>();
		}

		[ContextMenu("Play")]
		public void Play()
		{
			if (clips.Length < 1)
				return;

			if (!audioSource)
				return;

			// Get clip from Index
			clipIndex = playSequentially ? clipIndex % clips.Length : Random.Range(0, clips.Length);

			if (playSequentially)
				clipIndex++;

			audioSource.clip = clips[clipIndex];
			audioSource.RandomizePitch(pitchVariation.x, pitchVariation.y);
			audioSource.RandomizeVolume(volumeVariation.x, volumeVariation.y);
			audioSource.Play();
		}
	}
}