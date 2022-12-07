using UnityEngine;
using UnityEngine.Events;

namespace Extendo.SceneManager
{
	public class SceneTransition : MonoBehaviour
	{
		public SceneReference                            sceneReference;
		public UnityEngine.SceneManagement.LoadSceneMode loadSceneMode;
		public bool                                      async;
		public GameObject[]                              moveGameObjects;
		public UnityEvent                                onSceneLoadStart;
		public UnityEvent                                onSceneLoadComplete;
	}
}