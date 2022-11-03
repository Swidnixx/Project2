using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclicGameSequencer : GameSequencer
{
    protected override IEnumerator Reset(float time)
    {
        yield return new WaitForSeconds(time);
        events[current-1].AfterCooldown?.Invoke();
        cooldown = false;
        if (current >= events.Length)
        {
            current = 0;
        }
    }
}
