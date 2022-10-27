using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBounce : MonoBehaviour
{
    public float initialSpeed = 5;
    Rigidbody rb;
    bool click;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //click = Input.GetMouseButtonDown(0);
        if (Input.GetMouseButtonDown(0))
        {
            LaunchUp();
        }
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Mouse button 1 clicked");
            LaunchDown();
        }
    }

    private void LaunchDown()
    {
        rb.velocity = -Vector3.up * initialSpeed;
    }

    private void FixedUpdate()
    {

    }

    private void LaunchUp()
    {
        rb.velocity = Vector3.up * initialSpeed;
    }

}
