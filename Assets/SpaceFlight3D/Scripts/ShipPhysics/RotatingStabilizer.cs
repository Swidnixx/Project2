using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStabilizer : SpaceShipStabilizer
{
    protected override void AddForce()
    {
        float counterGravityOntoEngineUp = 2 - (Vector3.Dot(-Physics.gravity, transform.up) / Physics.gravity.magnitude);
        //Debug.Log("Counter gravity: " + counterGravityOntoEngineUp);
        force = power * (transform.up * counterGravityOntoEngineUp * Physics.gravity.magnitude * Mass);
        force += -rb.velocity * velocityCounterForce;

        rb.AddForce(force);
    }
}
