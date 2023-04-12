using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : Singleton<ScoreKeeper>
{
    public GameObject panel;

    public Text timeText;
    public Text smallDiamondsText;
    public Text bigDiamondsText;

    int seconds;
    int smallDiamondsCollected;
    int bigDiamodnsCollected;

    Diamond[] allDiamonds;
    int smallDiamondsToCollect;
    int bigDiamondsToCollect;

    string levelName;

    protected override void Awake()
    {
        base.Awake();
        LevelLoader.SceneLoaded += OnLevelLoaded;
        GameManager.StateChanged += OnGameStateChanged;
    }
    private void OnDestroy()
    {
        LevelLoader.SceneLoaded -= OnLevelLoaded;
        GameManager.StateChanged -= OnGameStateChanged;
    }


    void OnLevelLoaded(string levelName)
    {
        panel.SetActive(true);

        timeText.text = "0";
        smallDiamondsText.text = "0";
        bigDiamondsText.text = "0";
        this.levelName = levelName;

        allDiamonds = FindObjectsOfType<Diamond>();

        smallDiamondsToCollect = CountDiamonds(Diamond.DiamondType.Small);
        bigDiamondsToCollect = CountDiamonds(Diamond.DiamondType.Big);

        seconds = 0;
        smallDiamondsCollected = 0;
        bigDiamodnsCollected = 0;
    }

    void OnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.LevelFinished:
                OnLevelFinished();
                break;

            case GameManager.GameState.Flying:
                //unpaused
                StartCoroutine(Stopper());
                break;

            default:
                //paused
                StopAllCoroutines();
                break;
        }
    }

    IEnumerator Stopper()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            seconds++;
            timeText.text = seconds.ToString();
        }
    }

    public void CollectDiamond(Diamond.DiamondType type)
    {
        switch (type)
        {
            case Diamond.DiamondType.Big:
                bigDiamodnsCollected++;
                bigDiamondsText.text = bigDiamodnsCollected.ToString();
                break;
            case Diamond.DiamondType.Small:
                smallDiamondsCollected++;
                smallDiamondsText.text = smallDiamondsCollected.ToString();
                break;
        }

    }

    void OnLevelFinished()
    {
        panel.SetActive(false); 

        StopAllCoroutines();

        var stats = new LevelStatisticsWindow.LevelStatistics(
            levelName,
            seconds, 
            smallDiamondsCollected, smallDiamondsToCollect, 
            bigDiamodnsCollected, bigDiamondsToCollect);
        LevelStatisticsWindow.Instance.DisplayLevelStatistics(stats);
    }


    private int CountDiamonds(Diamond.DiamondType type)
    {
        int sum = 0;
        Array.ForEach(allDiamonds, d => sum += d.type == type ? 1 : 0);
        return sum;
    }
}

