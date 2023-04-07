using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyJoystickInputHandler : JoystickInputHandler
{
    public MyJoystick joystick;

    bool push;

    private void Start()
    {
        joystick.OnJoystickClicked += JoystickDown;
        joystick.OnJoystickReleased += JoystickUp;
    }

    void JoystickDown()
    {
        //Debug.Log("Pushing");
        push = true;
    }

    void JoystickUp()
    {
        //Debug.Log("Releasing");
        push = false;
    }

    protected override void UpdateUpwards()
    {
        if( push )
        {
            upwards = 1;
        }
        else
        {
            upwards = 0;
        }

    }

}
