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
		public  bool      closeMenuOnBackIfNoParent = true;

		public UnityEvent OnEnter;
		public UnityEvent OnExit;
		public UnityEvent OnClose;

		private object dataPassedDown;

		private Menu RootMenu
		{
			get
			{
				if (parentMenu)
					return parentMenu.RootMenu;

				return this;
			}
		}

		private Menu LastMenu
		{
			get
			{
				if (childMenu)
					return childMenu.LastMenu;

				return this;
			}
		}

		private void OnDestroy()
		{
			Close();
		}

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

		[ContextMenu("New Test Menu")]
		private void NewTestMenu()
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

		[ContextMenu("Go Here")]
		public void GoHere()
		{
			Enter();
			DeleteChildren();
		}

		[ContextMenu("Go Back To Start")]
		public void GoBackToStart()
		{
			var rootMenu = RootMenu;

			if (rootMenu)
				rootMenu.GoHere();
		}

		[ContextMenu("Go Back")]
		public void GoBack()
		{
			if (parentMenu)
			{
				Close();
			}

			if (closeMenuOnBackIfNoParent)
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
			// Invoke any events
			OnClose.Invoke();

			if (parentMenu)
				parentMenu.Enter();

			DeleteChildren();

			Destroy(gameObject);
		}

		private void DeleteChildren()
		{
			// Destroy Children
			if (!childMenu)
				return;

			childMenu.DeleteChildren();
			childMenu.OnClose.Invoke();
			Destroy(childMenu.gameObject);
		}

		[ContextMenu("Disable Menu Group")]
		private void DisableMenuGroup() => EnableMenuGroup(false);

		[ContextMenu("Enable Menu Group")]
		private void EnableMenuGroup() => EnableMenuGroup(true);

		public void EnableMenuGroup(bool enable)
		{
			var lastMenu = LastMenu;

			if (!lastMenu)
				return;

			lastMenu.EnableParentMenus(enable);
			lastMenu.gameObject.SetActive(enable);
		}

		// TODO: Fix minimize and maximize
		private void EnableParentMenus(bool enable)
		{
			if (parentMenu)
			{
				parentMenu.gameObject.SetActive(enable && !parentMenu.disableMenuOnChange);
				parentMenu.EnableParentMenus(enable);
			}
		}
	}
}