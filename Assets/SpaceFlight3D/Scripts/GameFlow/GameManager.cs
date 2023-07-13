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

    public GameState State { get; private set; }


    private void Start()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        SetState(GameState.MainMenu);
    }

    public void SetState(GameState state)
    {
        if (this.State == state) return;
        this.State = state;

        StateChanged?.Invoke(state);


        //Need to use this to unify Game Events to be generated only from here
        //switch (state)
        //{
        //    case GameState.MainMenu:
        //        break;
        //    case GameState.Flying:
        //        break;
        //}

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
        Instance.SetState(GameState.Pause);
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
        Instance.SetState(GameState.Flying);
    }
        #endregion
    
}