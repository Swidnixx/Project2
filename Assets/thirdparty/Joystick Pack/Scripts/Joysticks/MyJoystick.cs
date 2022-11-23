using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyJoystick : Joystick
{
    public Action OnJoystickClicked;
    public Action OnJoystickReleased;

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnJoystickClicked?.Invoke();
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        OnJoystickReleased?.Invoke();
        base.OnPointerUp(eventData);
    }
}
