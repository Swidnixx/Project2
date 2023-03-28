using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginesController : MonoBehaviour
{
    protected SpaceShipEngine[] engines;
    protected float maxPower;

    protected virtual void Start()
    {
        engines = GetComponentsInChildren<SpaceShipEngine>();
    }

    protected virtual void Update()
    {
        if (InputHandler.Instance == null) return;

        //Debug.Log(InputHandler.Instance.MouseHold);
        if(InputHandler.Instance.Upwards > 0)
        {
            foreach(var engine in engines)
            {
                //Debug.Log("Upwards: " + InputHandler.Instance.Upwards);
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
