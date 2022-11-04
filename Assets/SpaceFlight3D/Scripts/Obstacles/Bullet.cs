using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dir = Vector3.right;
    public float speed = 1;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //transform.position += dir * Time.deltaTime * speed;
        rb.MovePosition(transform.position + dir * Time.deltaTime * speed);
    }
}
