using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUpdater : MonoBehaviour
{
    public enum MissionPhase
    {
        StartMission,
        UpdateMission,
        FinishMission
    }

    // Use just UpdateMission for now
    public GameSequencer mission;

    [SerializeField] MissionPhase onInteract = MissionPhase.UpdateMission;

    public bool singleUsage;

    public void UpdateMission()
    {
        if (!mission.Cooldown)
        {
            switch (onInteract)
            {
                case MissionPhase.StartMission:
                    //mission.StartMission();
                    break;

                case MissionPhase.UpdateMission:
                    mission.UpdateMission();
                    break;

                case MissionPhase.FinishMission:
                    mission.FinishMission();
                    break;
            }

            if (singleUsage)
            {
                Destroy(gameObject);
            } 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position,  "Arrow1");
    }
}
