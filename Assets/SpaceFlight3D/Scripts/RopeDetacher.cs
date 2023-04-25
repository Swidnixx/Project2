using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDetacher : MonoBehaviour
{
public void Detach()
    {
        var hook = GameObject.FindObjectOfType<RopeHook>();
        if(hook)
        {
            hook.ResetPickup();
        }
    }
}
