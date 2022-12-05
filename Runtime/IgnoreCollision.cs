using System.Collections.Generic;
using UnityEngine;

namespace Extendo
{
	[AddComponentMenu("Extendo/Ignore Collision")]
	public class IgnoreCollision : MonoBehaviour
	{
		private Collider[]  localColliders;
		public  Transform[] transforms;
		public  Collider[]  colliders;

		private void Awake()
		{
			localColliders = GetComponents<Collider>();
		}

		private void IgnoreCollisions(bool ignore)
		{
			for (int i = 0; i < localColliders.Length; i++)
			{
				for (int j = 0; j < colliders.Length; j++)
					if (colliders[j])
						Physics.IgnoreCollision(localColliders[i], colliders[j], ignore);

				for (int j = 0; j < transforms.Length; j++)
				{
					if (!transforms[j])
						continue;

					var transformColliders = transforms[j].GetComponents<Collider>();

					for (int k = 0; k < transformColliders.Length; k++)
					{
						if (transformColliders[k])
							Physics.IgnoreCollision(localColliders[i], transformColliders[k], ignore);
					}
				}
			}
		}

		private void OnEnable()
		{
			IgnoreCollisions(true);
		}

		private void OnDisable()
		{
			IgnoreCollisions(false);
		}
	}
}