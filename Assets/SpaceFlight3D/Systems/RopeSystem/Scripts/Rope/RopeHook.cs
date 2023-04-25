using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RopeHook : MonoBehaviour
{
    public LayerMask pickup_layer;

    Rigidbody pickup;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && pickup != null)
        {
            ResetPickup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup") & !pickup)
        {
            pickup = other.gameObject.GetComponent<Rigidbody>();

            pickup.isKinematic = false;

            Joint joint = pickup.gameObject.AddComponent<HingeJoint>();
            joint.connectedBody = rb;

        }
    }

    public void ResetPickup()
    {
        var joint = pickup.GetComponent<HingeJoint>();
        if (joint) Destroy(joint);
        pickup = null;
    }
}
