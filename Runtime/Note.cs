using UnityEngine;

namespace Extendo
{
	[AddComponentMenu("Extendo/Note")]
	public class Note : MonoBehaviour
	{
		[TextArea(minLines:3, maxLines:8)]
		public string text;
		[InspectorName("Test")]
		public string url;
	}
}