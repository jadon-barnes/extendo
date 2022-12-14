using UnityEngine;

namespace Extendo.Menus
{
	[CreateAssetMenu(fileName = "Menu", menuName = "Extendo/Menu Asset", order = 0)]
	public class MenuAsset : ScriptableObject
	{
		public GameObject menuPrefab;

		public MenuNavigator SpawnMenu()
		{
			if (!menuPrefab)
				return null;

			return Instantiate(menuPrefab).GetComponentInChildren<MenuNavigator>();
		}
	}
}