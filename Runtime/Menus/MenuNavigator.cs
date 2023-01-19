using UnityEngine;
using UnityEngine.Events;

namespace Extendo.Menus
{
	[AddComponentMenu("Extendo/Menu Navigator")]
	[DisallowMultipleComponent]
	public class MenuNavigator : MonoBehaviour
	{
		/// <summary>
		/// The parent <see cref="MenuNavigator"/> of this <see cref="MenuNavigator"/>.
		/// </summary>
		public MenuNavigator ParentMenuNavigator { get; private set; }

		/// <summary>
		/// The submenu of this <see cref="MenuNavigator"/>.
		/// </summary>
		public MenuNavigator ChildMenuNavigator { get; private set; }

		[Tooltip("Disables the menu when it is not in focus.")]
		public bool disableMenuOnChange;

		[Tooltip("If true, will close the intial/root when 'GoBack()' is called, even if there is no menu to go back to.")]
		public bool closeMenuOnBackIfNoParent = false;

		public UnityEvent OnEnter;
		public UnityEvent OnExit;
		public UnityEvent OnClose;

		private object dataPassedDown;

		private MenuNavigator RootMenuNavigator
		{
			get
			{
				if (ParentMenuNavigator)
					return ParentMenuNavigator.RootMenuNavigator;

				return this;
			}
		}

		private MenuNavigator LastMenuNavigator
		{
			get
			{
				if (ChildMenuNavigator)
					return ChildMenuNavigator.LastMenuNavigator;

				return this;
			}
		}

		private void OnDestroy()
		{
			Close();
		}

		/// <summary>
		/// Pass down data to a <see cref="ChildMenuNavigator"/>.
		/// </summary>
		public void SetDataPassedDown(object data)
		{
			dataPassedDown = data;
		}

		/// <summary>
		/// Attempts to get data passed down from <see cref="ParentMenuNavigator"/>.
		/// </summary>
		/// <typeparam name="T">Data type</typeparam>
		/// <returns>True if getting the data was successful.</returns>
		public bool TryGetDataPassedIn<T>(out T data)
		{
			// No parent, return default
			if (!ParentMenuNavigator)
			{
				data = default;
				return false;
			}

			// Get Data if is type
			if (ParentMenuNavigator.dataPassedDown is T)
			{
				data = (T)ParentMenuNavigator.dataPassedDown;
				return true;
			}

			data = default;
			return false;
		}

		/// <summary>
		/// Spawns the prefab from <see cref="MenuAsset"/> and assigns it as a <see cref="ChildMenuNavigator"/> of this <see cref="MenuNavigator"/>.
		/// </summary>
		public void GoToMenu(MenuAsset menuAsset)
		{
			// Child menu already exists
			if (ChildMenuNavigator)
			{
				Debug.LogWarning($"Child menu already spawned. Close {ChildMenuNavigator.gameObject.name} before going to a new menu.");
				return;
			}

			// Spawns new menu as child of this menu
			ChildMenuNavigator = menuAsset.SpawnMenu();

			// Error creating spawned menu.
			if (!ChildMenuNavigator)
			{
				Debug.LogWarning("Can't find menu.");
				return;
			}

			// Exit this menu
			OnExit.Invoke();

			if (disableMenuOnChange)
				gameObject.SetActive(false);

			ChildMenuNavigator.Enter(this);
		}

		/// <summary>
		/// Jumps directly to this <see cref="MenuNavigator"/>.
		/// </summary>
		[ContextMenu("Go Here")]
		public void GoHere()
		{
			Enter();
			DeleteChildren();
		}

		/// <summary>
		/// Jumps directly to the initial/root <see cref="MenuNavigator"/>.
		/// </summary>
		[ContextMenu("Go Back To Start")]
		public void GoBackToStart()
		{
			MenuNavigator rootMenuNavigator = RootMenuNavigator;

			if (rootMenuNavigator)
				rootMenuNavigator.GoHere();
		}

		/// <summary>
		/// Closes this menu and opens it's parent. Will NOT Close the initial/root <see cref="MenuNavigator"/> if <see cref="closeMenuOnBackIfNoParent"/> is set to false.
		/// </summary>
		[ContextMenu("Go Back")]
		public void GoBack()
		{
			if (ParentMenuNavigator)
				Close();

			if (closeMenuOnBackIfNoParent)
				Close();
		}

		/// <summary>
		/// Closes/Destroys this <see cref="MenuNavigator"/> and enters the <see cref="ParentMenuNavigator"/>, destroying all <see cref="ChildMenuNavigator"/>s from this point downward.
		/// </summary>
		[ContextMenu("Close")]
		public void Close()
		{
			// Invoke any events
			OnClose.Invoke();

			if (ParentMenuNavigator)
				ParentMenuNavigator.Enter();

			DeleteChildren();

			Destroy(gameObject);
		}

		/// <summary>
		/// Sets focus to this menu.
		/// </summary>
		/// <param name="parentMenuNavigator">Assigns Menu as a parent of this menu</param>
		private void Enter(MenuNavigator parentMenuNavigator = null)
		{
			if (parentMenuNavigator)
				ParentMenuNavigator = parentMenuNavigator;

			OnEnter.Invoke();

			gameObject.SetActive(true);
		}

		/// <summary>
		/// Destroys all child menus from this point downward.
		/// </summary>
		private void DeleteChildren()
		{
			// Destroy Children
			if (!ChildMenuNavigator)
				return;

			ChildMenuNavigator.DeleteChildren();
			ChildMenuNavigator.OnClose.Invoke();
			Destroy(ChildMenuNavigator.gameObject);
		}

		[ContextMenu("Disable Menu Group")]
		private void DisableMenuGroup()
		{
			EnableMenuGroup(false);
		}

		[ContextMenu("Enable Menu Group")]
		private void EnableMenuGroup()
		{
			EnableMenuGroup(true);
		}

		/// <summary>
		/// Hides or reveals the entire <see cref="MenuNavigator"/> group. This is useful when you want to minimize or hide a group/collection of menus, retaining their context or state instead of completely destroying it.
		/// </summary>
		public void EnableMenuGroup(bool enable)
		{
			MenuNavigator lastMenuNavigator = LastMenuNavigator;

			if (!lastMenuNavigator)
				return;

			lastMenuNavigator.EnableParentMenus(enable);
			lastMenuNavigator.gameObject.SetActive(enable);
		}

		private void EnableParentMenus(bool enable)
		{
			if (ParentMenuNavigator)
			{
				ParentMenuNavigator.gameObject.SetActive(enable);
				ParentMenuNavigator.EnableParentMenus(enable);
			}
		}
	}
}