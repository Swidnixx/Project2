using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using UnityEngine.Events;

namespace SpaceFlight3D.UI
{
    public class MenuUI : MonoBehaviour
    {

        public UnityEvent OnMenuOpen;
        public UnityEvent OnMenuClose;

        MenuWindow current;

        public MenuWindow MainMenuWindow;
        public bool mainMenuActiveOnStart;

        public CanvasGroup canvasGroup;

        private void Start() => GameManager.StateChanged += GameStateChanged;
        private void OnDestroy() => GameManager.StateChanged -= GameStateChanged;

        void GameStateChanged(GameManager.GameState state)
        {
            switch(state)
            {
                case GameManager.GameState.MainMenu:
                    EnableMenuUI();
                    break;

                default:
                    DisableMenuUI();
                    break;
            }
        }

        void DisableMenuUI()
        {
            if(canvasGroup)
            {
                canvasGroup.interactable = false;
            }
        }
        void EnableMenuUI()
        {
            if(canvasGroup)
            {
                canvasGroup.interactable = true;
            }
        }

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
                //SwitchCurrentWindow(MainMenuWindow);
                OpenMenu();
            }
        }

        private void OnEnable()
        {
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

        public void OpenMenu(/*InputAction.CallbackContext ctx*/)
        {
            SwitchCurrentWindow(MainMenuWindow);
            OnMenuOpen?.Invoke();
        }

        // Leave UI section if in main menu. Back to main menu if deeper.
        public void CancelButtonInUI(/*InputAction.CallbackContext ctx*/)
        {
            if (current == MainMenuWindow)
            {
                MainMenuWindow.SetActive(false);
                OnMenuClose?.Invoke();
            }
            else
            {
                SwitchCurrentWindow(MainMenuWindow);
            }
        }

        public void ContinueToGame()
        {
            CancelButtonInUI( );
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

}