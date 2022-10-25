using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipStabilizer : MonoBehaviour
{
    public float Power { get { return power; } set { value = Mathf.Clamp(value, 0, 1); power = value; } }

    [Range(0, 1)]
    [SerializeField] float power = 1;
    [Range(0,1)]
    [SerializeField] float velocityCounterForce = 1;

    Rigidbody rb;
    Vector3 force;
    float verticalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        force = power * -Physics.gravity + -rb.velocity * velocityCounterForce;
        //force *= power;

        rb.AddForce( force, ForceMode.Acceleration );
    }
}
