using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class MenuUI : Singleton<MenuUI>
{


	// Input source component reference and root menu window
	//public PlayerInput playerInput;

	//public Action OnMenuOpen;
	//public Action OnMenuClose;

	MenuWindow current;

	public MenuWindow MainMenuWindow;
	public bool mainMenuActiveOnStart;

	void InitializeUI()
    {
		// turn all windows off
		Array.ForEach<MenuWindow>(
			GetComponentsInChildren<MenuWindow>(),
			window => window.SetActive(false)
		);
        if (mainMenuActiveOnStart)
        {
            // turn on main window
            SwitchCurrentWindow(MainMenuWindow); 
        }
    }

    private void OnEnable()
    {
		//playerInput.actions["Player/MenuOpen"].performed += OpenMainMenuWindow;
        //playerInput.actions["UI/MenuClose"].performed += CancelButtonInUI;

		InitializeUI();
    }

    private void OnDisable()
    {
		//if(playerInput)
  //      {
		//	playerInput.actions["Player/MenuOpen"].performed -= OpenMainMenuWindow;
		//	playerInput.actions["UI/MenuClose"].performed -= CancelButtonInUI;
  //      }
	}

	public void RefreshSelectables()
    {
		current.SetActive(true);
    }

    public void OpenMainMenuWindow(/*InputAction.CallbackContext ctx*/)
	{
		SwitchCurrentWindow(MainMenuWindow);
		//playerInput.SwitchCurrentActionMap("UI");
		//OnMenuOpen?.Invoke();
	}

	// Leave UI section if in main menu. Back to main menu if deeper.
	public void CancelButtonInUI(/*InputAction.CallbackContext ctx*/)
    {
		if(current == MainMenuWindow)
        {
			MainMenuWindow.SetActive(false);
			//playerInput.SwitchCurrentActionMap("Player");
			//OnMenuClose?.Invoke();
        }
		else
        {
			SwitchCurrentWindow(MainMenuWindow);
        }
    }

	public void ContinueToGame()
    {
		CancelButtonInUI( /*new InputAction.CallbackContext() */ );
    }

	public void SwitchCurrentWindow(MenuWindow newWindow)
    {
		if (current != null)
        {
            current.SetActive(false); 
        }
		current = newWindow;
		newWindow.SetActive(true);
    }

	public void ExitGame()
    {
		#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
		#else
		Application.Quit();
		#endif
    }
}
