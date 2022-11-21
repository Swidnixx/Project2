using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginesController : MonoBehaviour
{
    SpaceShipEngine[] engines;

    private void Start()
    {
        engines = GetComponentsInChildren<SpaceShipEngine>();
    }

    protected virtual void Update()
    {
        if (InputHandler.Instance == null) return;

        Debug.Log(InputHandler.Instance.MouseHold);
        if(InputHandler.Instance.MouseHold)
        {
            foreach(var engine in engines)
            {
                engine.Push = true;
            }
        }
        else
        {
            foreach (var engine in engines)
            {
                engine.Push = false;
            }
        }
    }
}
