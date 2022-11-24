using UnityEngine;

namespace Extendo.Audio
{
	public static class AudioExtension
	{
		public static void PlayRandom(this AudioSource audioSource, float volumeMin = 1f, float volumeMax = 1f, float pitchMin = 1f, float pitchMax = 1f)
		{
			audioSource.volume = Random.Range(volumeMin, volumeMax);
			audioSource.pitch  = Random.Range(pitchMin, pitchMax);
			audioSource.Play();
		}

		public static void PlayRandom(this AudioSource audioSource, AudioClip[] audioClips, float volumeMin = 1f, float volumeMax = 1f, float pitchMin = 1f, float pitchMax = 1f)
		{
			audioSource.clip   = audioClips[Random.Range(0, audioClips.Length)];
			audioSource.volume = Random.Range(volumeMin, volumeMax);
			audioSource.pitch  = Random.Range(pitchMin, pitchMax);
			audioSource.Play();
		}
	}
}