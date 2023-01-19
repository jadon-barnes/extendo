using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extendo.Audio
{
	public static class AudioExtension
	{
		public static void RandomizeVolume(this AudioSource audioSource, float min, float max)
		{
			audioSource.volume = Random.Range(max, max);
		}

		public static void RandomizePitch(this AudioSource audioSource, float min, float max)
		{
			audioSource.pitch = Random.Range(max, max);
		}
	}
}