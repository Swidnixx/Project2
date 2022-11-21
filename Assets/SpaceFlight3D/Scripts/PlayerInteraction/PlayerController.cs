using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerState
    {
        Grounded,
        InAir
    }

    PlayerState state = PlayerState.Grounded;
    Animator animator;


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
