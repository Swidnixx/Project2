using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] toSpawn;
    public bool spawnRepeating;
    public float startTime = 0;
    public float spawnTime = 1;

    private void Start()
    {
        if(spawnRepeating)
        {
            StartCoroutine(CycleSpawn(startTime, spawnTime));
        }
    }

    public void Spawn()
    {
        toSpawn.ToList().ForEach(prefab => Instantiate(prefab, transform.position, Quaternion.identity));
    }

    public void SpawnRepeatingly(float waitTime, float time)
    {
        spawnRepeating = true;
        StartCoroutine(CycleSpawn(waitTime, time));
    }

    IEnumerator CycleSpawn(float waitTime, float time)
    {
        yield return new WaitForSeconds(waitTime);

        while(spawnRepeating)
        {
            Spawn();
            yield return new WaitForSeconds(time);
        }
    }
}
