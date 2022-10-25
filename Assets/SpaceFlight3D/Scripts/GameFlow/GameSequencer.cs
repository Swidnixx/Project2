using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameSequencer : TextMessageDisplayer
{
    public bool Cooldown { get { return cooldown; } }

    public bool activateOnStartup;
    public GameEvent[] events;

    int current = 0;
    int previous = 0;
    private bool cooldown;

    private void Start()
    {
        if(activateOnStartup)
        {
            UpdateMission();
        }
    }

    public void UpdateMission()
    {
        if (!cooldown)
        {

            cooldown = true;
            events[current].Trigger();
            if (gameObject.activeSelf)
            {
                StartCoroutine(Reset(events[current].cooldown)); 
            }
            //Debug.Log(gameObject + ": " + current + " event resolved");
            previous = current;
            current++;

        }
        else
        {

        }
    }

    IEnumerator Reset(float time)
    {
        yield return new WaitForSeconds(time);
        events[previous].AfterCooldown?.Invoke();
        cooldown = false;
        if(current >= events.Length)
        {
            FinishMission();
        }
    }

    public void FinishMission()
    {
        Destroy(gameObject);
    }
}
