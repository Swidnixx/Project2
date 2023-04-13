using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[DefaultExecutionOrder(-1)]
public class ShipRotationSetup : Singleton<ShipRotationSetup>
{
    public GameObject player;
    public enum RotationType { Auto, Manual }

    public Dropdown selectionDropdown;
    public RotationType initialType;

    public RotationOptionsSetuper options;

    // List of components that are unique for current setup
    List<Component> currentSetUp;
    SteerSettings settings = new SteerSettings();
    public SteeringRefactored steerScript { get; private set; }



    protected override void Awake()
    {
        base.Awake();

        steerScript = player.GetComponent<SteeringRefactored>();
        if (steerScript)
        {
            settings.SetupSelf(steerScript);
            Destroy(steerScript);
        }

        currentSetUp = new List<Component>();
        Setup((int)initialType);

        selectionDropdown.onValueChanged.AddListener(Setup);
    }

    private void Start()
    {
        selectionDropdown.options = Enum.GetValues(typeof(RotationType)).Cast<RotationType>().Select( t => new Dropdown.OptionData(t.ToString())).ToList();
    }
    internal void Disable()
    {
        steerScript.enabled = false;
    }
    public void Enable()
    {
        steerScript.enabled = true;
    }

    private void DestroyPreviousSetup()
    {
        foreach (Component c in currentSetUp)
        {
            Destroy(c);
        }
        currentSetUp.Clear();
    }
    public void Setup(int mode)
    {
        DestroyPreviousSetup();
        // selectionDropdown.value = mode;
        selectionDropdown.SetValueWithoutNotify(mode);

        switch ((RotationType)mode)
        {
            case RotationType.Auto:
                SetupAuto();
                break;

            case RotationType.Manual:
                SetupManual();
                break;
        }

        options.Init(steerScript);
    }

    private void SetupAuto()
    {
        steerScript = player.AddComponent<SteeringRefactored>();
        settings.SetupScript(steerScript);
        currentSetUp.Add(steerScript);
    }
    void SetupManual()
    {
        steerScript = player.AddComponent<SteeringManual>();
        settings.SetupScript(steerScript);
        currentSetUp.Add(steerScript);
    }

    [Serializable]
    class SteerSettings
    {
        // Kinematic Ship rotation
        float rotateSpeed = 1;
        float rotateCombackMultiplier = 1;
        float maxAngle = 45;
        // Dynamic sideways forces
        float force = 2;
        bool applyForceLocally;

        // Rotation Axis settings
        Vector3 axis = Vector3.forward;
        bool flipLeftRight;

        public void SetupScript(SteeringRefactored script)
        {
            script.rotateSpeed = rotateSpeed;
            script.comeBackMultiplier = rotateCombackMultiplier;
            script.maxAngle = maxAngle;
            script.force = force;
            script.applyForceLocally = applyForceLocally;
            script.axis = axis;
            script.flipLeftRight = flipLeftRight;
        }

        public void SetupSelf(SteeringRefactored script)
        {
            rotateSpeed = script.rotateSpeed;
            rotateCombackMultiplier = script.comeBackMultiplier;
            maxAngle = script.maxAngle;
            force = script.force;
            applyForceLocally = script.applyForceLocally;
            axis = script.axis;
            flipLeftRight = script.flipLeftRight;
        }
    }

    [Serializable]
    public class RotationOptionsSetuper
    {
        SteeringRefactored steerScript;

        public Slider angleSlider;
        public Text angleText;
        public Slider rotationSpeed;
        public Text speedText;
        public Toggle flipRotation;
        public Slider lateralForceSlider;
        public Text lateralForceText;
        public Toggle lateralForceLocally;

        public Slider combackMultiplier;
        public Text comebackText;

        public void Init(SteeringRefactored steerScript)
        {
            this.steerScript = steerScript;

            angleSlider.value = steerScript.maxAngle;
            angleText.text = steerScript.maxAngle.ToString();

            rotationSpeed.value = steerScript.rotateSpeed;
            speedText.text = steerScript.rotateSpeed.ToString();

            combackMultiplier.value = steerScript.comeBackMultiplier;
            comebackText.text = steerScript.comeBackMultiplier.ToString();
            combackMultiplier.onValueChanged.AddListener(SetComebackSpeed);

            flipRotation.isOn = steerScript.flipLeftRight;

            angleSlider.onValueChanged.AddListener(SetMaxAngle);
            rotationSpeed.onValueChanged.AddListener(SetRotationSpeed);
            flipRotation.onValueChanged.AddListener(SetRotationFlip);

            lateralForceSlider.value = steerScript.force;
            lateralForceText.text = steerScript.force.ToString();
            lateralForceLocally.isOn = steerScript.applyForceLocally;

            lateralForceSlider.onValueChanged.AddListener(SetLateralForce);
            lateralForceLocally.onValueChanged.AddListener(SetLateralForceLocally);
        }

        public void SetMaxAngle(float angle)
        {
            steerScript.maxAngle = angle;
            angleText.text = angle.ToString("n2");
        }
        public void SetRotationSpeed(float speed)
        {
            steerScript.rotateSpeed = speed;
            speedText.text = speed.ToString("n2");
        }
        public void SetComebackSpeed(float speed)
        {
            steerScript.comeBackMultiplier = speed;
            comebackText.text = speed.ToString("n2");
        }

        public void SetRotationFlip(bool flip)
        {
            steerScript.flipLeftRight = flip;
        }

        void SetLateralForce(float force)
        {
            steerScript.force = force;
            lateralForceText.text = force.ToString("n2");
        }

        void SetLateralForceLocally(bool locally)
        {
            steerScript.applyForceLocally = locally;
        }
    }
}
