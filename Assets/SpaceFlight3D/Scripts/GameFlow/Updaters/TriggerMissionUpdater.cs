using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TriggerMissionUpdater : MissionUpdater
{
    public string otherTag = "Player";

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(otherTag))
        {
            UpdateMission();
        }
    }
}
