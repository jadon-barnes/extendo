using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Extendo.SceneManager
{
	public class SceneTransition : MonoBehaviour
	{
		public SceneReference sceneReference;
		public bool           async;
		// public bool                enableAsyncSceneAutomatically = true;
		public bool                allowMultipleSameScenes = false;
		public LoadSceneParameters parameters;
		public GameObject[]        moveGameObjects;
		public UnityEvent          onLoadStart;
		public UnityEvent          onLoadDone;

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
			if (!allowMultipleSameScenes
			    && UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(index).isLoaded)
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

			MoveGameObjects(scene);
		}

		private void LoadSceneAsync(int index)
		{
			StartCoroutine(LoadSceneAsyncRoutine(index));
		}

		IEnumerator LoadSceneAsyncRoutine(int index)
		{
			AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index, parameters);
			asyncLoad.allowSceneActivation = true;

			onLoadStart.Invoke();

			// Wait until the asynchronous scene fully loads
			while (!asyncLoad.isDone) { yield return null; }

			onLoadDone.Invoke();

			var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(index);
			MoveGameObjects(scene);
		}

		private void MoveGameObjects(Scene scene)
		{
			for (int i = 0; i < moveGameObjects.Length; i++)
			{
				UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(moveGameObjects[i], scene);
			}
		}
	}
}