using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : Singleton<LevelLoader>
{
    public CanvasGroup loadingScreen;
    public ScreenFader screenFader;

    string targetSceneName;
    AsyncOperation asyncOp;

    public static event Action<string> SceneLoaded;
    public static event Action SceneRevealed;

    public string[] mainMenuScenes = { "MainMenu", "MainMenu2", "MainMenu3" };

    internal void LoadMainMenu()
    {
        LoadLevel(mainMenuScenes[mainMenuScenes.Length - 1]);
    }

    public void LoadLevel(string name)
    {
        targetSceneName = name;
        screenFader.PrepareFader(true);
        screenFader.SetFadeCallback(OnSceneFaded);
        screenFader.Fade();

        //disable all interactivity
    }

    void OnSceneFaded()
    {
        asyncOp = SceneManager.LoadSceneAsync(targetSceneName);
        asyncOp.completed += OnSceneLoaded;

        ShowLoadingScreen();
    }

    void OnSceneLoaded(AsyncOperation aop)
    {
        HideLoadingScreen();

        screenFader.PrepareFader(false);
        screenFader.SetFadeCallback(OnSceneRevealed);
        screenFader.Reveal();

        SceneLoaded?.Invoke(targetSceneName);
    }

    void OnSceneRevealed()
    {
        //Needs to be moved to gameManager as callback
        foreach(string s in mainMenuScenes)
        {
            if(targetSceneName == s)
            {
                GameManager.Instance.SetState(GameManager.GameState.MainMenu);
                return; //break;
            }
        }
        GameManager.Instance.SetState(GameManager.GameState.Pause);
        //

        SceneRevealed?.Invoke();

        //enable all interactivity
    }


    void ShowLoadingScreen()
    {
        //loadingScreen.enabled = true;
        loadingScreen.alpha = 1;
    }
    
    void HideLoadingScreen()
    {
        //loadingScreen.enabled = false;
        loadingScreen.alpha = 0;
    }
}
