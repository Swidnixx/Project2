using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Rigidbody player;
    public Vector3 rotation;
    public bool setOnStart;
    public Transform startPos;
    Vector3 spawnPoint;

    RigidbodyConstraints rbConstraints;

    private void Start()
    {
        rbConstraints = player.constraints;

        if(setOnStart)
        {
            SetSpawn(startPos);
            Respawn();
        }
    }

    public void SetSpawn(Transform position)
    {
        spawnPoint = position.position;
    }

    public void Respawn()
    {

        player.transform.rotation = Quaternion.Euler(rotation);
        player.transform.position = spawnPoint;

        // reset velocities and systems of player
        player.velocity = Vector3.zero;
        player.angularVelocity = Vector3.zero;


        player.constraints = rbConstraints;
    }
}
