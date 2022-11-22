using UnityEngine;

namespace Extendo
{
	public class Persist : MonoBehaviour
	{
		private void Awake() => DontDestroyOnLoad(gameObject);
	}
}