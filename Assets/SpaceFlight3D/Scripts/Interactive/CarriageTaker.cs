using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarriageTaker : MonoBehaviour
{
    public UnityEvent onTake;

    public float takeDuration = 3;

    float actualDuration = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            actualDuration += Time.deltaTime;
            if (actualDuration >= takeDuration)
            {
                Take(other.transform);
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            actualDuration = 0;
            other.transform.parent = null;
        }
    }


    private void Take(Transform carriage)
    {
        onTake?.Invoke();
        GetComponent<Target>().ScaleDown();
        Destroy(carriage.gameObject);
        Destroy(gameObject);
    }
}
