using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelConductor : MonoBehaviour
{
    public GameObject statisticsPanel;
    public GameObject endStatistics;
    public EasyFlyStart flyStarter;
    public ScreenFader screenFader;

    private void Start()
    {
        // Show Level Statistics and Block GamePlay
        statisticsPanel.SetActive(true);
        endStatistics.SetActive(false);
        flyStarter.Block();

        // Reveal Statistics Panel on Start
        if(screenFader)
        {
            screenFader.gameObject.SetActive(true);
            screenFader.FadeImmediately();
            screenFader.Reveal();
        }

        // Wait for player to click play...
    }

    public void CloseStatisticsView()
     {
        statisticsPanel.SetActive(false);

        GameObject startInvoker = new GameObject("StartInvoker");
        startInvoker.transform.parent = transform;

        InputInvoker inputInvoker = startInvoker.AddComponent<InputInvoker>();

        inputInvoker.inputType = InputInvoker.InputType.Tap;
        inputInvoker.OnInput = new UnityEngine.Events.UnityEvent();
        inputInvoker.OnInput.AddListener( StartPlaying );

        inputInvoker.singleUsage = true;

    }

    public void StartPlaying()
    {
        flyStarter.StartFlight();
    }

    public void FinishAndShowStatistics()
    {
        endStatistics.SetActive(true);
    }

    public void EndLevel()
    {
        screenFader.RevealImmediately();
        screenFader.Fade();
    }
}
