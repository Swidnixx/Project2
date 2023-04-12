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

    protected override void Awake()
    {
        base.Awake();
        GameManager.StateChanged += ChangeState;
        LevelLoader.SceneLoaded += LevelLoaded;
    }
    private void OnDestroy() => GameManager.StateChanged -= ChangeState;
    private void OnEnable() => GameManager.StateChanged += ChangeState;
    private void OnDisable() => GameManager.StateChanged -= ChangeState;
    
    void LevelLoaded(string name)
    {
        SpawnPlayer();
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


    void Start()
    {
        animator = GetComponent<Animator>();
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
