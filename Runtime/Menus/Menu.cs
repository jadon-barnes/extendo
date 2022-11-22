using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Menus
{
	[DisallowMultipleComponent]
	public class Menu : MonoBehaviour
	{
		/// <summary>
		/// The parent <see cref="Menu"/> of this <see cref="Menu"/>.
		/// </summary>
		public Menu ParentMenu { get; private set; }
		/// <summary>
		/// The submenu of this <see cref="Menu"/>.
		/// </summary>
		public Menu ChildMenu { get; private set; }
		[Tooltip("Disables the menu when it is not in focus.")]
		public bool disableMenuOnChange;
		[Tooltip("This will close the intial/root when 'GoBack()' is called, even if there is no menu to go back to.")]
		public bool closeMenuOnBackIfNoParent = false;

		public UnityEvent OnEnter;
		public UnityEvent OnExit;
		public UnityEvent OnClose;

		private object dataPassedDown;

		private Menu RootMenu
		{
			get
			{
				if (ParentMenu)
					return ParentMenu.RootMenu;

				return this;
			}
		}

		private Menu LastMenu
		{
			get
			{
				if (ChildMenu)
					return ChildMenu.LastMenu;

				return this;
			}
		}

		private void OnDestroy()
		{
			Close();
		}

		/// <summary>
		/// Pass down data to a <see cref="ChildMenu"/>.
		/// </summary>
		public void SetDataPassedDown(object data)
		{
			dataPassedDown = data;
		}

		/// <summary>
		/// Attempts to get data passed down from <see cref="ParentMenu"/>.
		/// </summary>
		/// <typeparam name="T">Data type</typeparam>
		/// <returns>True if getting the data was successful.</returns>
		public bool TryGetDataPassedIn<T>(out T data)
		{
			// No parent, return default
			if (!ParentMenu)
			{
				data = default;
				return false;
			}

			// Get Data if is type
			if (ParentMenu.dataPassedDown is T)
			{
				data = (T)ParentMenu.dataPassedDown;
				return true;
			}

			data = default;
			return false;
		}

		/// <summary>
		/// Spawns the prefab from <see cref="MenuAsset"/> and assigns it as a <see cref="ChildMenu"/> of this <see cref="Menu"/>.
		/// </summary>
		public void GoToMenu(MenuAsset menuAsset)
		{
			if (ChildMenu)
			{
				Debug.LogWarning($"Child menu already spawned. Close {ChildMenu.gameObject.name} before going to a new menu.");
				return;
			}

			ChildMenu = menuAsset.SpawnMenu();

			if (!ChildMenu)
			{
				Debug.LogWarning("Can't find menu.");
				return;
			}

			Exit();
			ChildMenu.Enter(this);
		}

		/// <summary>
		/// Jumps directly to this <see cref="Menu"/>.
		/// </summary>
		[ContextMenu("Go Here")]
		public void GoHere()
		{
			Enter();
			DeleteChildren();
		}

		/// <summary>
		/// Jumps directly to the initial/root <see cref="Menu"/>.
		/// </summary>
		[ContextMenu("Go Back To Start")]
		public void GoBackToStart()
		{
			var rootMenu = RootMenu;

			if (rootMenu)
				rootMenu.GoHere();
		}

		/// <summary>
		/// Closes this menu and opens it's parent. Will NOT Close the initial/root <see cref="Menu"/> if <see cref="closeMenuOnBackIfNoParent"/> is set to false.
		/// </summary>
		[ContextMenu("Go Back")]
		public void GoBack()
		{
			if (ParentMenu)
			{
				Close();
			}

			if (closeMenuOnBackIfNoParent)
				Close();
		}

		/// <summary>
		/// Closes/Destroys this <see cref="Menu"/> and enters the <see cref="ParentMenu"/>, destroying all <see cref="ChildMenu"/>s from this point downward.
		/// </summary>
		[ContextMenu("Close")]
		public void Close()
		{
			// Invoke any events
			OnClose.Invoke();

			if (ParentMenu)
				ParentMenu.Enter();

			DeleteChildren();

			Destroy(gameObject);
		}

		private void Enter(Menu parentMenu = null)
		{
			if (parentMenu)
				this.ParentMenu = parentMenu;

			OnEnter.Invoke();

			gameObject.SetActive(true);
		}

		private void Exit()
		{
			OnExit.Invoke();

			if (disableMenuOnChange)
				gameObject.SetActive(false);
		}

		private void DeleteChildren()
		{
			// Destroy Children
			if (!ChildMenu)
				return;

			ChildMenu.DeleteChildren();
			ChildMenu.OnClose.Invoke();
			Destroy(ChildMenu.gameObject);
		}

		[ContextMenu("Disable Menu Group")]
		private void DisableMenuGroup() => EnableMenuGroup(false);

		[ContextMenu("Enable Menu Group")]
		private void EnableMenuGroup() => EnableMenuGroup(true);

		/// <summary>
		/// Hides or reveals the entire <see cref="Menu"/> group. This is useful when you want to minimize or hide a group/collection of menus, retaining their context or state instead of completely destroying it.
		/// </summary>
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
			if (ParentMenu)
			{
				ParentMenu.gameObject.SetActive(enable && !ParentMenu.disableMenuOnChange);
				ParentMenu.EnableParentMenus(enable);
			}
		}
	}
}