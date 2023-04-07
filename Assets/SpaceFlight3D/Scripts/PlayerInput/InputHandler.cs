using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : SingletonSwitchable<InputHandler>
{
    public bool MouseDown { get { return mouseDown; } }
    public float Upwards { get { return upwards; } }
    public float LeftRight { get { return leftRight; } }
    public float Downwards {  get { return downwards; } }

    protected bool mouseDown;
    protected float upwards;
    protected float leftRight;
    protected float downwards;

    private void Start()
    {
        Input.gyro.enabled = true;
    }

    protected virtual void Update()
    {
        if (Instance == null)
        {
            Debug.Log("Input handler null");
            return;
        }


        //Debug.Log("Input Handler instance: " + Instance);
       // Debug.Log("InputHandler:" + Instance.gameObject.name + ", " + Instance.GetType());
        UpdateMouseDown();
        UpdateUpwards();
        UpdateLeftRight();
        UpdateDownwards();
    }

    protected virtual void UpdateUpwards()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
        {
            upwards = 1;
        }
        else
        {
            upwards = 0;
        }
#else
        if (Input.GetMouseButton(0))
        {
            upwards = 1;
        }
        else
        {
            upwards = 0;
        }
#endif

    }
    protected virtual void UpdateMouseDown()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mouseDown = true;
        }
        else
        {
            mouseDown = false;
        }
#else
        if(Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
        else
        {
            mouseDown = false;
        }
#endif
    }
    protected virtual void UpdateLeftRight()
    {
#if UNITY_EDITOR
        leftRight = Input.GetAxis("Horizontal");
#else
        float accelerationX = Input.acceleration.x;
        leftRight = Mathf.Clamp( accelerationX * 2, -1, 1);
#endif 
    }
    protected virtual void UpdateDownwards()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        if ( verticalAxis < 0)
        {
            downwards = Mathf.Abs(verticalAxis);
        }
        else
        {
            downwards = 0;
        }
    }
}
