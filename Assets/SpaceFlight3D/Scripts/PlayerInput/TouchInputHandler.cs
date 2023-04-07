using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputHandler : InputHandler
{
    protected override void UpdateLeftRight()
    {
#if UNITY_EDITOR
        if( Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            leftRight = 0;
        }
        else if(Input.GetMouseButton(0))
        {
            leftRight = 1;
        }
        else if(Input.GetMouseButton(1))
        {
            leftRight = -1;
        }
        else
        {
            leftRight = 0;
        }
#else
        if (Input.touchCount == 1)
        {
            float vieportX = Camera.main.ScreenToViewportPoint(Input.touches[0].position).x;
            if (vieportX < 0.5)
            {
                leftRight = 1;
            }
            else
            {
                leftRight = -1;
            }
        }
        else
        {
            leftRight = 0;
        } 
#endif
    }

    protected override void UpdateUpwards()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            upwards = 1;
        }
        else
        {
            upwards = 0;
        }
#else
        if(Input.touchCount > 0)
        {
            upwards = 1;
        }
        else
        {
            upwards = 0;
        }
#endif
    }
}
