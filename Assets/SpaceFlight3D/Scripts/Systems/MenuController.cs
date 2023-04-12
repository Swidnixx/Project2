using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : Singleton<MenuController>
{
    public GameObject handMenu;

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
            case GameManager.GameState.Flying:
                EnableHandMenu(true);
                break; 

            default:
                EnableHandMenu(false);
                break;
        }
    }

    void EnableHandMenu(bool active)
    {
        handMenu.SetActive(active);
    }
}
