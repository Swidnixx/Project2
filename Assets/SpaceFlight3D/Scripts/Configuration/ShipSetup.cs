using SpaceFlight3D.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSetup : Singleton<ShipSetup>
{
    public GameObject player;
    Rigidbody playerRb;
    SpaceShipEngine mainEngine;

    public Joystick joystick;
    public ButtonHoldable buttonLeft;
    public ButtonHoldable buttonRight;
    public Slider handle;
    public ButtonHoldable thrustButton;

    public Dropdown selectionDropdown;
    public Modes initialMode;

    internal void Disable()
    {
        playerRb.isKinematic = true;
    }
    internal void Enable()
    {
        playerRb.isKinematic = false;
    }

    // List of components that are unique for current setup
    List<Component> currentSetUp;



    public PhysicsSettings options;

    public enum Modes
    {
        GyroMode, JoystickMode, SplitScreenMode, HandleMode, ButtonsMode
    }

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
        mainEngine = player.GetComponent<SpaceShipEngine>();

        currentSetUp = new List<Component>();
        Setup((int)initialMode);

        selectionDropdown.onValueChanged.AddListener(Setup);

        options.Init(playerRb, mainEngine);
    }

    public void Setup(int mode)
    {
        DestroyPreviousSetup();
        // selectionDropdown.value = mode;
        selectionDropdown.SetValueWithoutNotify(mode);

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


        InputHandler.Instance.Reset();
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

    [Serializable]
    public class PhysicsSettings
    {
        Rigidbody rb;
        SpaceShipEngine engine;

        public Slider mainForceSlider;
        public Text mainForceText;
        public Slider dragSlider;
        public Text dragText;

        public void Init(Rigidbody rb, SpaceShipEngine engine)
        {
            this.rb = rb;
            this.engine = engine;

            SetMainForce(engine.MaxPower);
            mainForceSlider.value = engine.MaxPower;
            mainForceSlider.onValueChanged.AddListener(SetMainForce);

            SetDrag(rb.drag);
            dragSlider.value = rb.drag;
            dragSlider.onValueChanged.AddListener(SetDrag);
        }

        void SetMainForce( float force)
        {
            engine.MaxPower = force;
            mainForceText.text = engine.MaxPower.ToString("n2");
        }

        void SetDrag(float drag)
        {
            rb.drag = drag;
            dragText.text = drag.ToString("n2");
        }
    }
}
