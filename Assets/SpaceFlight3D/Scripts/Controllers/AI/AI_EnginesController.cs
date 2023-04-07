using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_EnginesController : MonoBehaviour
{
    protected SpaceShipEngine[] engines;
    protected float maxPower;

    protected virtual void Start()
    {
        engines = GetComponentsInChildren<SpaceShipEngine>();
    }

    public virtual void Activate()
    {
        foreach(var engine in engines)
        {
            //Debug.Log("Upwards: " + InputHandler.Instance.Upwards);
            engine.Push = true;
        }  
    }

    public  virtual void Deactivate()
    {
        foreach (var engine in engines)
        {
            engine.Push = false;
        }
    }

    public void OnDisable()
    {
        foreach (var engine in engines)
        {
            engine.Push = false;
        }
    }
}
