using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extendo
{
	[AddComponentMenu("Extendo/Note")]
	public class Note : MonoBehaviour
	{
		[TextArea(3, 8)] public string     text;
		public                  List<Task> tasks = new();
		public                  string     url;

		[Serializable]
		public class Task
		{
			[TextArea(2, 6)] public string name;
			public                  bool   done;
		}

		[ContextMenu("Go to URL")]
		private void GoToURL()
		{
			if (string.IsNullOrEmpty(url))
			{
				Debug.LogWarning("No URL available.");
				return;
			}

			Application.OpenURL(url);
		}

		[ContextMenu("Remove Completed Tasks")]
		private void RemoveCompletedTasks()
		{
			tasks.RemoveAll(task => task.done);
		}
	}
}