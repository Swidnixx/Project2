using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerInvoker : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    public string otherTag = "";

    private void OnTriggerEnter(Collider other)
    {
        bool tagOk = true;
        if(otherTag != "")
        {
            tagOk = other.CompareTag(otherTag);
        }

        if(tagOk)
        {
            OnEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool tagOk = true;
        if (otherTag != "")
        {
            tagOk = other.CompareTag(otherTag);
        }

        if (tagOk)
        {
            OnExit?.Invoke();
        }
    }
}
