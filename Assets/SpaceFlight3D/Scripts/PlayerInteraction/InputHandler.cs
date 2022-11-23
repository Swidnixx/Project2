using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : Singleton<InputHandler>
{
    public bool MouseDown { get { return mouseDown; } }
    public bool MouseHold { get { return mouseHold; } }
    public float LeftRight { get { return leftRight; } }

    protected bool mouseDown;
    protected bool mouseHold;
    protected float leftRight;

    protected virtual void Update()
    {
        //Debug.Log("Input Handler instance: " + Instance);
        UpdateMouseDown();
        UpdateMouseHold();
        UpdateLeftRight();
    }

    protected virtual void UpdateMouseHold()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
        {
            mouseHold = true;
        }
        else
        {
            mouseHold = false;
        }
#else
        if (Input.GetMouseButton(0))
        {
            mouseHold = true;
        }
        else
        {
            mouseHold = false;
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
}
