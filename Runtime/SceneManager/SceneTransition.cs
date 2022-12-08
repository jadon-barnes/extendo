using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Extendo.SceneManager
{
	public class SceneTransition : MonoBehaviour
	{
		public SceneReference      sceneReference;
		public bool                async;
		public bool                multipleSameScenes = false;
		public LoadSceneParameters parameters;
		public GameObject[]        moveGameObjects;
		public UnityEvent          onLoadStart;
		public UnityEvent<float>   onAsyncLoadProgress;
		public UnityEvent          onLoadDone;

		public float AsyncLoadProgress { get; private set; }

		[ContextMenu("Transition")]
		public void Transition()
		{
			Transition(sceneReference);
		}

		public void Transition(SceneReference sceneReference)
		{
			if (sceneReference == null)
				return;

			Transition(sceneReference.index);
		}

		public void Transition(int index)
		{
			if (!multipleSameScenes && UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(index).isLoaded)
				return;

			if (async)
				LoadSceneAsync(index);
			else
				LoadScene(index);
		}

		private void LoadScene(int index)
		{
			onLoadStart.Invoke();
			var scene = UnityEngine.SceneManagement.SceneManager.LoadScene(index, parameters);
			onLoadDone.Invoke();

			MoveGameObjects(index);
		}

		private void LoadSceneAsync(int index)
		{
			StartCoroutine(LoadSceneAsyncRoutine(index));
		}

		IEnumerator LoadSceneAsyncRoutine(int index)
		{
			AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index, parameters);

			onLoadStart.Invoke();

			// Wait until the asynchronous scene fully loads
			while (!asyncLoad.isDone)
			{
				AsyncLoadProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
				onAsyncLoadProgress.Invoke(AsyncLoadProgress);
				yield return null;
			}

			onAsyncLoadProgress.Invoke(AsyncLoadProgress);

			onLoadDone.Invoke();

			MoveGameObjects(index);
		}

		private void MoveGameObjects(int index)
		{
			var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(index);

			for (int i = 0; i < moveGameObjects.Length; i++)
			{
				UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(moveGameObjects[i], scene);
			}
		}

		public void Test(float value)
		{
			Debug.Log(value);
		}
	}
}