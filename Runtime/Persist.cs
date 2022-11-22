using UnityEngine;

namespace Extendo
{
	[AddComponentMenu("Extendo/Persist")]
	public class Persist : MonoBehaviour
	{
		private void Awake() => DontDestroyOnLoad(gameObject);
	}
}