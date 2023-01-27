using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Bullet", menuName = "Obstacles/Bullet Configuration")]
public class BulletConfig : ScriptableObject
{
    public Vector3 dir = Vector3.down;
    public float speed = 5;
    public Vector3 rotationAxis;

    public void Configure(Bullet b)
    {
        b.dir = this.dir;
        b.speed = this.speed;

        Rotatoer r = b.GetComponentInChildren<Rotatoer>();
        r.axis = Vector3.Scale( Random.onUnitSphere, rotationAxis );
    }
}
