using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Menus
{
	[DisallowMultipleComponent]
	public class Menu : MonoBehaviour
	{
		public  MenuAsset sampleMenu;
		private Menu      parentMenu;
		private Menu      childMenu;
		public  bool      disableMenuOnChange;

		public UnityEvent OnEnter;
		public UnityEvent OnExit;
		public UnityEvent OnClose;

		private object dataPassedDown;

		public void SetDataPassedDown(object data)
		{
			dataPassedDown = data;
		}

		public bool TryGetDataPassedIn<T>(out T data)
		{
			if (!parentMenu)
			{
				data = default;
				return false;
			}

			if (parentMenu.dataPassedDown is T)
			{
				data = (T)parentMenu.dataPassedDown;
				return true;
			}

			data = default;
			return false;
		}

		[ContextMenu("Test")]
		public void GoToMenuTest()
		{
			GoToMenu(sampleMenu);
		}

		public void GoToMenu(MenuAsset menuAsset)
		{
			if (childMenu)
			{
				Debug.LogWarning($"Child menu already spawned. Close {childMenu.gameObject.name} before going to a new menu.");
				return;
			}

			childMenu = menuAsset.SpawnMenu();

			if (!childMenu)
			{
				Debug.LogWarning("Can't find menu.");
				return;
			}

			Exit();
			childMenu.Enter(this);
		}

		[ContextMenu("Go Back To Start")]
		public void GoBackToStart()
		{
			if (parentMenu)
			{
				parentMenu.GoBackToStart();
				Close();
			}
		}

		[ContextMenu("Go Back")]
		public void GoBack()
		{
			if (parentMenu)
			{
				parentMenu.Enter();
			}

			Close();
		}

		private void Enter(Menu parentMenu = null)
		{
			if (parentMenu)
				this.parentMenu = parentMenu;

			OnEnter.Invoke();
			gameObject.SetActive(true);
		}

		[ContextMenu("Exit")]
		private void Exit()
		{
			OnExit.Invoke();

			if (disableMenuOnChange)
				gameObject.SetActive(false);
		}

		[ContextMenu("Close")]
		public void Close()
		{
			// Close all child menus
			if (childMenu)
			{
				childMenu.Close();
			}

			OnClose.Invoke();

			if (parentMenu)
			{
				parentMenu.Enter();
			}

			Destroy(gameObject);
		}

		[ContextMenu("Minimize")]
		private void Minimize()
		{
			Minimize(true);
		}

		[ContextMenu("Maximize")]
		private void Maximize()
		{
			Minimize(false);
		}

		public void Minimize(bool minimize)
		{
			EnableParentMenus(!minimize);
			EnableChildMenus(!minimize);

			if (!minimize && childMenu)
				return;

			gameObject.SetActive(!minimize);
		}

		// TODO: Fix minimize and maximize
		private void EnableParentMenus(bool enable)
		{
			if (parentMenu)
			{
				if (enable && parentMenu.childMenu)
					return;

				parentMenu.gameObject.SetActive(enable);
				parentMenu.EnableParentMenus(enable);
			}
		}

		private void EnableChildMenus(bool enable)
		{
			if (childMenu)
			{
				if (enable && childMenu.childMenu)
					return;

				childMenu.gameObject.SetActive(enable);
				childMenu.EnableChildMenus(enable);
			}
		}
	}
}