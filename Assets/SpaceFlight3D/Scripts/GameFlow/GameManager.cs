using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(9999)]
public class GameManager : Singleton<GameManager>
{
    public enum GameState{ Loading, MainMenu, Pause, Flying, Animation, LevelFinished }
    public static event Action<GameState> StateChanged;

    public LevelsList levelList;
    string currentLevel;
    string nextLevel;

    GameState state;


    private void Start()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        SetState(GameState.MainMenu);
    }

    public void SetState(GameState state)
    {
        if (this.state == state) return;
        this.state = state;

        StateChanged?.Invoke(state);

        switch (state)
        {
            case GameState.MainMenu:
                break;
            case GameState.Flying:
                break;
        }

    }

    #region Static Helper Methods
    public static void NextLevel()
    {
        if(Instance.nextLevel != "")
        {
            LoadLevel(Instance.nextLevel);
        }
        else
        {
            LevelLoader.Instance.LoadMainMenu();
        }
    }

    internal static void FinishLevel()
    {
        Instance.SetState(GameState.LevelFinished);
    }
    public static void RestartGame()
    {
        ResumeGame();
        //print(SceneManager.GetActiveScene().name);
        LevelLoader.Instance.LoadLevel(SceneManager.GetActiveScene().name);
        Instance.SetState(GameState.Loading);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadLevel(string name)
    {
        LevelLoader.Instance.LoadLevel(name);
        Instance.SetState(GameState.Loading);
        Instance.currentLevel = name;

        Instance.nextLevel = Instance.levelList.GetNext(name);
    }

    public static void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
    }
        #endregion
    
}