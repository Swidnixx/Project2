using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShipDestroyer : MonoBehaviour
{
    [Tooltip("Velocity in m/s")]
    public float destroySpeed = 10;

    public ParticleSystem crashEffect;

    public UnityEvent onCrash;

    SpaceShipThruster thruster;
    SpaceShipSteer steer;

    private void Start()
    {
        thruster = GetComponent<SpaceShipThruster>();
        steer = GetComponent<SpaceShipSteer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Relative velocity: " + collision.relativeVelocity.magnitude);
        if(collision.relativeVelocity.magnitude > destroySpeed || collision.transform.CompareTag("Terrain"))
        {
            Crash();
        }
    }

    private void Crash()
    {
        crashEffect.Play();
        thruster.enabled = false;
        steer.enabled = false;
        onCrash?.Invoke();
    }
}
