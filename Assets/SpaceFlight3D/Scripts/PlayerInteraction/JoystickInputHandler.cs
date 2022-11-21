using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInputHandler : InputHandler
{
    public Joystick joystick;
    
    protected override void Update()
    {
        leftRight = joystick.Horizontal;

        if( joystick.Vertical > 0 )
        {
            mouseHold = true;
        }
        else
        {
            mouseHold = false;
        }

        //Debug.Log("Ho: " + joystick.Horizontal + "; Ve: " + joystick.Vertical);
    }
}
