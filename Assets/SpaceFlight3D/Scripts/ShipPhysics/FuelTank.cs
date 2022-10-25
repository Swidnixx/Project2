using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    [SerializeField] int capacity = 100;

    // How full the tank is (0-1)
    public float Status { get; private set; } = 1f;

    public float UseFuel(float amount)
    {
        float percentageAmount = amount / capacity;

        if(Status > percentageAmount)
        {
            Status -= percentageAmount;
            return amount;
        }
        else
        {
            float lastFuel = Status;
            Status = 0;
            return lastFuel;
        }
    }

    internal void AddFuel(object p)
    {
        throw new NotImplementedException();
    }

    public void AddFuel(float percent)
    {
        Status += percent;
        Status = Mathf.Clamp(Status, 0, 1);
    }
}
