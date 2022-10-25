using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelMissionUpdater : MissionUpdater
{
    public FuelTank fuelTank;

    public float targetStatus = 0f;

    private void Update()
    {
        if(fuelTank.Status <= targetStatus)
        {
            UpdateMission();
        }
    }
}
