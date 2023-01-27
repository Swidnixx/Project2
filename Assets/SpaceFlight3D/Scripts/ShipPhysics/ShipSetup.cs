using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSetup : MonoBehaviour
{
    public GameObject player;

    public Joystick joystick;
    public Modes initialMode;

    List<Component> currentSetUp;

    public enum Modes
    {
        GyroMode, JoystickMode, SplitScreenMode
    }

    private void Start()
    {
        currentSetUp = new List<Component>();
        NewSetup(initialMode);
    }

    public void Setup(int mode)
    {
        NewSetup((Modes)mode);
    }

    void NewSetup(Modes mode)
    {
        DestroyPreviousSetup();
        switch(mode)
        {
            case Modes.GyroMode:
                SetupGyro();
                break;

            case Modes.JoystickMode:
                SetupJoystick();
                break;

            case Modes.SplitScreenMode:
                SetupSplitScreen();
                break;
        }
    }

    private void DestroyPreviousSetup()
    {
        //if (InputHandler.Instance)
        //{
        //   InputHandler.Instance.Reset(); 
        //}
        foreach (Component c in currentSetUp)
        {
            Destroy(c);
        }
        currentSetUp.Clear();
        joystick.gameObject.SetActive(false);
    }

    private void SetupGyro()
    {
        InputHandler ih = player.AddComponent<InputHandler>();
        ih.Switch(ih);
        currentSetUp.Add( ih );
    }

    private void SetupJoystick()
    {
        joystick.gameObject.SetActive(true);

        JoystickInputHandler input = player.AddComponent<JoystickInputHandler>();
        input.joystick = joystick;
        input.Switch(input);
        currentSetUp.Add(input);
    }

    private void SetupSplitScreen()
    {
        InputHandler ih = player.AddComponent<TouchInputHandler>();
        ih.Switch(ih);
        currentSetUp.Add(ih);
    }
}
