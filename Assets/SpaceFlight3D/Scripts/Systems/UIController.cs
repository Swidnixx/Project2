using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    public GameObject handMenu;
    public GameObject buttonsPanel;

    protected override void Awake()
    {
        base.Awake();
        GameManager.StateChanged += GameStateChanged;
    }
    private void OnDestroy() => GameManager.StateChanged -= GameStateChanged;

    void GameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Pause:
                EnableHandMenu(true);
                buttonsPanel.SetActive(false);
                break;

            case GameManager.GameState.Flying:
                EnableHandMenu(true);
                buttonsPanel.SetActive(true);
                break; 

            default:
                EnableHandMenu(false);
                buttonsPanel.SetActive(false);
                break;
        }
    }

    void EnableHandMenu(bool active)
    {
        handMenu.SetActive(active);
    }
}
