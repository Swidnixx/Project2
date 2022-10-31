using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public Transform direction;
    public Rigidbody rb;

    public bool launchOnStart;

    private void Start()
    {
        if(launchOnStart)
        {
            Launch();
        }
    }

    public void Launch()
    {
        rb.AddForce((direction.position - transform.position) * rb.mass);
    }
}
