using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public Transform direction;
    public Rigidbody rb;
    public bool launchOnStart;

    public float speed = 1;
    private void Start()
    {
        if(launchOnStart)
        {
            Launch();
        }
    }

    public void Launch()
    {
        Debug.Log("Launched " + (direction.position - transform.position).normalized);
        rb.AddForce((direction.position - transform.position).normalized * rb.mass * speed, ForceMode.Impulse);
    }
}
