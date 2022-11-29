using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInputHandler : InputHandler
{
    public Joystick joystick;

    bool push;

    protected override void UpdateLeftRight()
    {
        leftRight = joystick.Horizontal;
    }

    protected override void UpdateUpwards()
    {
        float vertical = joystick.Vertical;
        upwards = Mathf.Clamp(vertical, 0, 1);
    }

    protected override void UpdateDownwards()
    {
        float vertical = joystick.Vertical;
        if (vertical < 0)
        {
            downwards = Mathf.Abs(vertical);
        }
        else
        {
            downwards = 0;
        }
    }
}
