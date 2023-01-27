using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : ObjectSpawner
{
    public BulletConfig config;

    public override void Spawn()
    {
        foreach(var prefab in toSpawn)
        {
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
            Bullet b = obj.GetComponent<Bullet>();
            config.Configure(b);
        }
    }
}
