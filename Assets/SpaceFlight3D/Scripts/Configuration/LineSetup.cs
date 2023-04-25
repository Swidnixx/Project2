using RopeMechanim;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineSetup : MonoBehaviour
{
    public RopeMechanim.RopeMechanim rope;
    public JointConfig jointConfig;

    public Slider ropeLengthSlider;
    public Text ropeLengthText;
    public Slider jointsDistanceSlider;
    public Text jointsDistanceText;

    public Slider jointsAngularDragSlider;
    public Text jointsAngularText;
    public Slider jointsDragSlider;
    public Text jointsDragText;
    public Slider jointsMassSlider;
    public Text jointsMassText;
    public Slider jointsPushDownForceSlider;
    public Text jointsPushDownText;
    public Slider jointsLinearLimitSlider;
    public Text jointsLinearText;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        //Rope length and anchors distance
        SetRopeLength(rope.ropeLength);
        ropeLengthSlider.value = rope.ropeLength;
        ropeLengthSlider.onValueChanged.AddListener(SetRopeLength);

        SetJointsDistance(rope.distanceBetweenJoints);
        jointsDistanceSlider.value = rope.distanceBetweenJoints;
        jointsDistanceSlider.onValueChanged.AddListener(SetJointsDistance);

        //Joints physical settings
        SetJointsAngularDrag(jointConfig.angularDrag);
        jointsAngularDragSlider.value = jointConfig.angularDrag;
        jointsAngularDragSlider.onValueChanged.AddListener(SetJointsAngularDrag);

        SetJointsLinearDrag(jointConfig.drag);
        jointsDragSlider.value = jointConfig.drag;
        jointsDragSlider.onValueChanged.AddListener(SetJointsLinearDrag);

        SetJointsMass(jointConfig.mass);
        jointsMassSlider.value = jointConfig.mass;
        jointsMassSlider.onValueChanged.AddListener(SetJointsMass);

        SetJointsPushDownForce(jointConfig.pushDownForce);
        jointsPushDownForceSlider.value = jointConfig.pushDownForce;
        jointsPushDownForceSlider.onValueChanged.AddListener(SetJointsPushDownForce);

        var cf = jointConfig as ConfigurableSetup;
        if (cf)
        {
            SetJointsLinearLimit(cf.linearLimit);
            jointsLinearLimitSlider.value = cf.linearLimit;
            jointsLinearLimitSlider.onValueChanged.AddListener(SetJointsLinearLimit);
        }
    }
    //Joints Physical properties
    void SetJointsAngularDrag(float drag)
    {
        jointConfig.angularDrag = drag;
        jointsAngularText.text = drag.ToString("n2");
    }
    void SetJointsLinearDrag(float drag)
    {
        jointConfig.drag = drag;
        jointsDragText.text = drag.ToString("n2");
    }
    void SetJointsMass(float mass)
    {
        jointConfig.mass = mass;
        jointsMassText.text = mass.ToString("n2");
    }
    void SetJointsPushDownForce(float force)
    {
        jointConfig.pushDownForce = force;
        jointsPushDownText.text = force.ToString("n2");
    }
    void SetJointsLinearLimit(float limit)
    {
        (jointConfig as ConfigurableSetup).linearLimit = limit;
        jointsLinearText.text = limit.ToString("n2");
    }

    //Rope length and anchor distances
    void SetRopeLength(float length)
    {
        rope.ropeLength = length;
        ropeLengthText.text = length.ToString("n2");
    }

    void SetJointsDistance(float dist)
    {
        rope.distanceBetweenJoints = dist;
        jointsDistanceText.text = dist.ToString("n2");
    }


}
