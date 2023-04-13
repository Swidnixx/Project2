using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringRefactored : MonoBehaviour
{
    // Kinematic Ship rotation
    public float maxAngle = 45;
    public float rotateSpeed = 1;
    public float comeBackMultiplier = 1;
    // Dynamic sideways forces
    public float force = 2;
    public bool applyForceLocally;

    // Rotation Axis settings
    public Vector3 axis = Vector3.forward;
    public bool flipLeftRight;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Kinematic Ship Rotating
    private void Update()
    {
        if (InputHandler.Instance == null) return;
        //Debug.Log(InputHandler.Instance);
        float leftRight = InputHandler.Instance.LeftRight;

        Quaternion targetRotation = SetTargetRotation(leftRight);
        transform.rotation = targetRotation;
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        //Debug.Log("Cur rot: " + transform.rotation.eulerAngles + " Target rot: " + targetRotation.eulerAngles + " step: " + Time.deltaTime * rotateSpeed);
    }

    float rotation;


    protected virtual Quaternion SetTargetRotation(float leftRight)
    {
        if(leftRight != 0)
        {
            rotation += leftRight * (flipLeftRight ? 1 : -1) * Time.deltaTime * rotateSpeed * 100;
            rotation = Mathf.Clamp(rotation, -maxAngle, maxAngle);
        }
        else if (Mathf.Abs(rotation) > 0.05f * rotateSpeed)
        {
            rotation += (flipLeftRight ? 1 : -1) * Time.deltaTime * rotateSpeed * Mathf.Sign(rotation) * 100 * comeBackMultiplier;
            if(rotation < 0)
                rotation = Mathf.Clamp( rotation, -maxAngle, 0);
            else
                rotation = Mathf.Clamp( rotation, 0, maxAngle);
        }
        else
        {
            rotation = 0;
        }

        //Vector3 shipStaticRot = Vector3.Scale(transform.rotation.eulerAngles, (Vector3.one - axis)); //preserve rotation on other axles
        Vector3 shipDynamicRot = rotation * axis;
        Quaternion targetRotation = Quaternion.Euler(shipDynamicRot);// + shipStaticRot );
        //targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.unscaledTime * rotateSpeed);// + shipStaticRot );
        return targetRotation;
    }

    // Dynamic Lateral Forces Moving Ship Sideways
    private void FixedUpdate()
    {
        float rot = Vector3.SignedAngle(transform.up, Vector3.up, axis);
        Vector3 forceDir = new Vector3(1, 0, 1) - axis;

        Vector3 force = forceDir * rot / maxAngle * this.force * rb.mass;
        if (applyForceLocally)
        {
            force = transform.TransformDirection(force);
        }
        Debug.DrawRay(transform.position, force / rb.mass / this.force, Color.red);
        rb.AddForce(force);
    }
}
