using System;
using UnityEngine.Events;

[Serializable]
public class GameEvent
{
    public string name = "Game Event";
    public UnityEvent OnTriggered;
    public float cooldown;
    public UnityEvent AfterCooldown;


    public void Trigger()
    {
        OnTriggered?.Invoke();
    }
}