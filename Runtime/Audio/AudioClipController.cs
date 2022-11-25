using UnityEngine;

namespace Extendo.Audio
{
	public class AudioClipController : MonoBehaviour
	{
		public  AudioSource audioSource;
		public  AudioClip[] clips;
		public  Vector2     volumeVariation = Vector3.one;
		public  Vector2     pitchVariation  = Vector2.one;
		private int         clipIndex       = -1; // Set to -1 at first to avoid skipping the first clip if "PlaySequentially" is enabled
		public  bool        playSequentially;

		[ContextMenu("Play")]
		public void Play()
		{
			if (clips.Length < 1)
				return;

			if (!audioSource)
				return;

			clipIndex = playSequentially ? (clipIndex + 1) % clips.Length : Random.Range(0, clips.Length);

			audioSource.clip = clips[clipIndex];
			audioSource.RandomizePitch(pitchVariation.x, pitchVariation.y);
			audioSource.RandomizeVolume(volumeVariation.x, volumeVariation.y);
			audioSource.Play();
		}
	}
}