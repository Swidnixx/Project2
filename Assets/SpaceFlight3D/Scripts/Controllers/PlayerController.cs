using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    enum PlayerState
    {
        Grounded,
        InAir
    }

    PlayerState state = PlayerState.Grounded;
    Animator animator;

    public Transform[] visibleObjects;



    protected override void Awake()
    {
        base.Awake();
        GameManager.StateChanged += ChangeState;
        LevelLoader.SceneLoaded += LevelLoaded;
    }
    private void OnDestroy() => GameManager.StateChanged -= ChangeState;
    private void OnEnable() => GameManager.StateChanged += ChangeState;
    private void OnDisable() => GameManager.StateChanged -= ChangeState;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void LevelLoaded(string name)
    {
        SpawnPlayer();
    }

    public void Hide()
    {
        foreach (var item in visibleObjects)
        {
            item.gameObject.SetActive(false); 
        }
        DisableMovement();
        SteeringRefactored steerScript = GetComponent<SteeringRefactored>();
        if (steerScript)
        {
            steerScript.Reset();
        }
    }

    public void Show()
    {
        foreach (var item in visibleObjects)
        {
            item.gameObject.SetActive(true); 
        }
        EnableMovement();
    }

    private void DisableMovement()
    {
        ShipSetup.Instance.Disable();
        ShipRotationSetup.Instance.Disable();
    }

    public void EnableMovement()
    {
        ShipSetup.Instance.Enable();
        ShipRotationSetup.Instance.Enable();
    }


    void ChangeState(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.MainMenu:
                SpawnPlayer();
                DisableMovement();
                break;
            default:
                DisableMovement();
                break;
            case GameManager.GameState.Flying:
                EnableMovement();
                break;
        }
    }
    internal void SpawnPlayer()
    {
        Respawner.Instance.FindSpawnPos();
        Respawner.Instance.Respawn();
    }



    private void OnCollisionExit(Collision collision)
    {
        if (!animator) return;
        //Debug.Log("Collision exit");
        state = PlayerState.InAir;
        animator.SetTrigger("inAir");
        Invoke("SetAir", 0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!animator) return;
        //Debug.Log("Collision enter");
        state = PlayerState.Grounded;
        animator.SetTrigger("grounded");
    }
    public void SetAir()
    {
        animator.SetTrigger("inAir");
    }
    #region Helper Methods

    #endregion
}
