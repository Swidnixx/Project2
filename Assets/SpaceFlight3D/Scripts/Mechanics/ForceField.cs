using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public float forceAmount;
    public Vector3 forceDir;
    Rigidbody player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.GetComponent<Rigidbody>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void FixedUpdate()
    {
        if(player)
        {
            Vector3 force = forceDir * forceAmount * player.mass;
            player.AddForce(force);
        }
    }
}
