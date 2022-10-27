using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipGlideDown : MonoBehaviour
{
    public float force = 1;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 force = Vector3.forward * transform.rotation.x * this.force;
        Debug.DrawLine(transform.position, transform.position + force, Color.red);
        rb.AddForce(force);   
    }
}
