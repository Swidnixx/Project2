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

    protected int current = 0;
    int previous = 0;
    protected bool cooldown;

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
            //Need to fix it bcuz Updaters calling Update during cooldown will fail (especially single usage)
        }
    }

    protected virtual IEnumerator Reset(float time)
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
