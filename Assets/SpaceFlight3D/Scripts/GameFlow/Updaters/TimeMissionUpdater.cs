using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMissionUpdater : MissionUpdater
{
    public float Time { get {return time;} set { time = value; }}
    [SerializeField] float time = 10f;

    private void OnEnable()
    {
        Debug.Log("Time trigger started");
        StartCoroutine(WaitAndUpdateMission());
    }

    IEnumerator WaitAndUpdateMission()
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Time trigger resolved");
        UpdateMission();
        gameObject.SetActive(false);
    }
}
