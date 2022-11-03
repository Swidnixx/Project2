using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelRefiller : MonoBehaviour
{
    public Transform forceField;
    public float percentPerSecond = 0.25f;

    float yScale;
    private void Start()
    {
        if (forceField != null)
        {
            yScale = forceField.localScale.y; 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var tank = other.GetComponentInParent<FuelTank>();
            if(tank != null && ( forceField == null || forceField.localScale.y > 0))
            {
                //Debug.Log(other);
                tank.AddFuel(percentPerSecond * Time.deltaTime);
                if (forceField != null)
                {
                    forceField.localScale = forceField.localScale - Vector3.up * (percentPerSecond * Time.deltaTime) * yScale; 
                }
            }
        }
    }
}
