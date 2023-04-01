using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipRotationSetup : MonoBehaviour
{
    public GameObject player;
    public enum RotationType { Auto, Manual }

    public Dropdown selectionDropdown;
    public RotationType initialType;

    // List of components that are unique for current setup
    List<Component> currentSetUp;
    SteerSettings settings = new SteerSettings();

    private void Start()
    {
        SteeringRefactored steerScript = player.GetComponent<SteeringRefactored>();
        if (steerScript)
        {
            settings.SetupSelf(steerScript);
            Destroy(steerScript);
        } 

        currentSetUp = new List<Component>();
        Setup((int)initialType);
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
        selectionDropdown.value = mode;

        switch ((RotationType)mode)
        {
            case RotationType.Auto:
                SetupAuto();
                break;

            case RotationType.Manual:
                SetupManual();
                break;
        }
    }

    private void SetupAuto()
    {
        var script = player.AddComponent<SteeringRefactored>();
        settings.SetupScript(script);
        currentSetUp.Add(script);
    }
    void SetupManual()
    {
        var script = player.AddComponent<SteeringManual>();
        settings.SetupScript(script);
        currentSetUp.Add(script);
    }

    [Serializable]
    class SteerSettings
    {
        // Kinematic Ship rotation
        float rotateSpeed = 1;
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
            script.maxAngle = maxAngle;
            script.force = force;
            script.applyForceLocally = applyForceLocally;
            script.axis = axis;
            script.flipLeftRight = flipLeftRight;
        }

        public void SetupSelf(SteeringRefactored script)
        {
            rotateSpeed = script.rotateSpeed;
            maxAngle = script.maxAngle;
            force = script.force;
            applyForceLocally = script.applyForceLocally;
            axis = script.axis;
            flipLeftRight = script.flipLeftRight;
        }
    }
}
