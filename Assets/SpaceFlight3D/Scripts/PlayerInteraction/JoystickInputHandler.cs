using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInputHandler : InputHandler
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

    protected override void UpdateLeftRight()
    {
//#if UNITY_EDITOR
        //base.UpdateLeftRight();
//#else
        leftRight = joystick.Horizontal;
//#endif
    }

    protected override void UpdateMouseHold()
    {
//#if UNITY_EDITOR
//        base.UpdateMouseHold();
//#else
        if( push )
        {
            mouseHold = true;
        }
        else
        {
            mouseHold = false;
        }
//#endif
    }

}
