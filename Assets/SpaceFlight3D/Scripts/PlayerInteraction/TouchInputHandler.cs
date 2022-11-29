using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputHandler : InputHandler
{
    protected override void UpdateLeftRight()
    {
        if(Input.touchCount == 1)
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
    }

    protected override void UpdateUpwards()
    {
        if(Input.touchCount > 0)
        {
            upwards = 1;
        }
        else
        {
            upwards = 0;
        }
    }
}
