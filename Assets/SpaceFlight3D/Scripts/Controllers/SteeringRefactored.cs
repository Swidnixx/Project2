using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringRefactored : MonoBehaviour
{
    // Kinematic Ship rotation
    public float maxAngle = 45;
    public float rotateSpeed = 1;
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
        //float angleRotation = maxAngle * leftRight * (flipLeftRight ? -1 : 1);
        //Vector3 shipStaticRot = Vector3.Scale(transform.rotation.eulerAngles, (Vector3.one - axis)); //preserve rotation on other axles
        //Vector3 shipDynamicRot = angleRotation * axis;
        //Quaternion targetRotation = Quaternion.Euler( shipStaticRot + shipDynamicRot );

        if (Time.deltaTime * rotateSpeed > 1)
        {
            Debug.LogWarning(gameObject + ": Lerp step is greater than 1"); // As we're lerping, step must be within 0-1 range
        }

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        //Debug.Log("Cur rot: " + transform.rotation.eulerAngles + " Target rot: " + targetRotation.eulerAngles + " step: " + Time.deltaTime * rotateSpeed);
    }

    protected virtual Quaternion SetTargetRotation(float leftRight)
    {
        float angleRotation = maxAngle * leftRight * (flipLeftRight ? 1 : -1);
        //Vector3 shipStaticRot = Vector3.Scale(transform.rotation.eulerAngles, (Vector3.one - axis)); //preserve rotation on other axles
        Vector3 shipDynamicRot = angleRotation * axis;
        Quaternion targetRotation = Quaternion.Euler(shipDynamicRot);// + shipStaticRot );
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
