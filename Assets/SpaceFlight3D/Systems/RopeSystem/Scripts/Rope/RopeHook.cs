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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pickup") & !pickup)
        {
            pickup = other.gameObject.GetComponent<Rigidbody>();
          
            pickup.isKinematic = false;

            Joint joint = pickup.gameObject.AddComponent<HingeJoint>();
            joint.connectedBody = rb;

        }
    }

    public void ResetPickup()
    {
        pickup = null;
    }
}
