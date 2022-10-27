using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dir = Vector3.right;
    public float speed = 1;

    private void Update()
    {
        transform.position += dir * Time.deltaTime * speed;
    }
}
