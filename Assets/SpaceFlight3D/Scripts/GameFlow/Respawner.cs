using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Respawner : Singleton<Respawner>
{
    public UnityEvent OnRespawn;

    public Rigidbody player;
    public Vector3 rotation;
    public bool setOnStart;
    public Transform startPos;
    Vector3 spawnPoint;

    RigidbodyConstraints rbConstraints;


    public void FindSpawnPos()
    {
        var go = GameObject.Find("RespawnPos");
        if(go)
        {
            SetSpawn(go.transform);
        }
        else
        {
            Debug.LogWarning("Respawner didn't find SpawnSpot!");
        }
    }

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
        OnRespawn.Invoke();

        player.transform.rotation = Quaternion.Euler(rotation);
        player.transform.position = spawnPoint;

        // reset velocities and systems of player
        player.velocity = Vector3.zero;
        player.angularVelocity = Vector3.zero;


        player.constraints = rbConstraints;
    }
}
