using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipIncrementalThruster : SpaceShipThruster
{
    public float powerRiseSpeed = 1;
    public float powerFallSpeed = 1;

    protected override void Start()
    {
        base.Start();
    }

    protected override void AccumulateForce()
    {
        if (InputHandler.Instance.MouseHold)
        {
            power = Mathf.MoveTowards(power, MaxPower, Time.deltaTime * powerRiseSpeed);
        }
        else
        {
            power = Mathf.MoveTowards(power, 0, Time.deltaTime * powerFallSpeed);
        }
    }
}
