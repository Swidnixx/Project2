using SpaceFlight3D.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSetup : MonoBehaviour
{
    public GameObject player;

    public Joystick joystick;
    public ButtonHoldable buttonLeft;
    public ButtonHoldable buttonRight;
    public Slider handle;
    public ButtonHoldable thrustButton;

    public Dropdown selectionDropdown;
    public Modes initialMode;

    // List of components that are unique for current setup
    List<Component> currentSetUp;

    public enum Modes
    {
        GyroMode, JoystickMode, SplitScreenMode, HandleMode, ButtonsMode
    }

    private void Start()
    {
        currentSetUp = new List<Component>();
        Setup((int)initialMode);
    }

    public void Setup(int mode)
    {
        DestroyPreviousSetup();
        selectionDropdown.value = mode;

        switch ((Modes)mode)
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

            case Modes.HandleMode:
                SetupButtonsAndHandle();
                break;

            case Modes.ButtonsMode:
                SetupButtons();
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
        buttonLeft.gameObject.SetActive(false);
        buttonRight.gameObject.SetActive(false);
        handle.gameObject.SetActive(false);
        thrustButton.gameObject.SetActive(false);

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

    private void SetupButtonsAndHandle()
    {
        buttonLeft.gameObject.SetActive(true);
        buttonRight.gameObject.SetActive(true);
        handle.gameObject.SetActive(true);

        ButtonsInputHandler ih = player.AddComponent<ButtonsInputHandler>();
        ih.Setup(buttonLeft, buttonRight, handle);

        ih.Switch(ih);
        currentSetUp.Add(ih);
    }

    private void SetupButtons()
    {
        buttonLeft.gameObject.SetActive(true);
        buttonRight.gameObject.SetActive(true);
        thrustButton.gameObject.SetActive(true);

        ButtonsInputHandler ih = player.AddComponent<ButtonsInputHandler>();
        ih.Setup(buttonLeft, buttonRight, thrustButton);

        ih.Switch(ih);
        currentSetUp.Add(ih);
    }
}
