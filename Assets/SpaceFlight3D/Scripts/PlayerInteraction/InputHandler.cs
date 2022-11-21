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
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
        {
            mouseHold = true;
        }
        else
        {
            mouseHold = false;
        }

        leftRight = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            mouseDown = true;
        }
        else
        {
            mouseDown = false;
        }
#else
//Thrust
        if (Input.GetMouseButton(0))
        {
            mouseHold = true;
        }
        else
        {
            mouseHold = false;
        }

        // Left Right
        float accelerationX = Input.acceleration.x;
        leftRight = Mathf.Clamp( accelerationX * 2, -1, 1);

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
}
