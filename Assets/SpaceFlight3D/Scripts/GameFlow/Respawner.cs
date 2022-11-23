using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Rigidbody player;
    public Vector3 rotation;
    Vector3 spawnPoint;

    public void SetSpawn(Transform position)
    {
        spawnPoint = position.position;
    }

    public void Respawn()
    {
        // reset velocities and systems of player
        player.velocity = Vector3.zero;
        player.angularVelocity = Vector3.zero;

        player.rotation = Quaternion.Euler(rotation);
        player.position = spawnPoint;
    }
}
