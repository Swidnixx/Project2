using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvenFlight : MonoBehaviour
{
    public Transform enter;
    public Transform exit;

    Rigidbody player;

    public UnityEvent OnEnter;

    private void OnTriggerEnter(Collider other)
    {
        if(player == null)
        {
            if(other.CompareTag("Player"))
            {
                player = other.GetComponent<Rigidbody>();
                InitMechanic();
            }
        }
    }

    private void InitMechanic()
    {
        OnEnter?.Invoke();

        player.constraints = RigidbodyConstraints.FreezePositionY;
        //player.isKinematic = true;
        StartCoroutine(Utils.KinematicMovement.MoveToLineary(player, enter.position, 2));
    }

    private void Update()
    {
        if (player == null) return;
        
        if( Vector3.Distance(player.position, exit.position) < 2)
        {
            FinishMechanic();
        }
    }

    void FinishMechanic()
    {
        player.constraints = RigidbodyConstraints.None;
    }
}
