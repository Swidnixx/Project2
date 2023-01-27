using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShipDestroyer : MonoBehaviour
{
    public string[] immiediateDestroyTags;
    [Tooltip("Velocity in m/s")]
    public float destroySpeed = 10;

    public ParticleSystem crashEffect;

    public UnityEvent onCrash;

    //SpaceShipEngine thruster;
    //SpaceShipSteer steer;

    //private void Start()
    //{
    //    thruster = GetComponent<SpaceShipEngine>();
    //    steer = GetComponent<SpaceShipSteer>();
    //}

    private void OnCollisionEnter(Collision collision)
    {
        bool deadlyTag = Array.IndexOf(immiediateDestroyTags, collision.transform.tag) != -1;
        //Debug.Log("Relative velocity: " + collision.relativeVelocity.magnitude);
        if(collision.relativeVelocity.magnitude > destroySpeed || deadlyTag)
        {
            Crash();
        }
    }

    public void Crash()
    {
        crashEffect.Play();
        //thruster.enabled = false;
        //steer.enabled = false;
        onCrash?.Invoke();
    }
}
