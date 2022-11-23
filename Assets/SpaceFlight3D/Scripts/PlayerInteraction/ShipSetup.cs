using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSetup : MonoBehaviour
{
    public GameObject player;

    public MyJoystick joystick;
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
        foreach (Component c in currentSetUp)
        {
            Destroy(c);
        }
        joystick.gameObject.SetActive(false);
    }

    private void SetupGyro()
    {
        currentSetUp.Add( player.AddComponent<InputHandler>() );
        //currentSetUp.Add( player.AddComponent<SpaceShipSteer>() );
        //currentSetUp.Add( player.AddComponent<EnginesController>() );
    }

    private void SetupJoystick()
    {


        //currentSetUp.Add(player.AddComponent<SpaceShipStabilizer>());

        joystick.gameObject.SetActive(true);
        //JoystickController jc = player.AddComponent<JoystickController>();
        //jc.joystick = joystick;
        //currentSetUp.Add(jc);

        JoystickInputHandler input = player.AddComponent<JoystickInputHandler>();
        currentSetUp.Add(input);
        input.joystick = joystick;
    }

    private void SetupSplitScreen()
    {
        throw new NotImplementedException();
    }
}
