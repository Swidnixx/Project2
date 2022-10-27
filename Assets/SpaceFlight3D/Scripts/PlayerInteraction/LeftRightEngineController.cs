using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightEngineController : MonoBehaviour
{
    [SerializeField]SpaceShipEngine leftEngine;
    [SerializeField]SpaceShipEngine rightEngine;

    protected virtual void Update()
    {
        if (InputHandler.Instance == null) return;

        if(InputHandler.Instance.LeftRight < -0.1)
        {
            leftEngine.Push = true;
        }

        if(InputHandler.Instance.LeftRight > 0.1)
        {
            rightEngine.Push = true;
        }

        if(Mathf.Approximately(InputHandler.Instance.LeftRight, 0))
        {
            leftEngine.Push = false;
            rightEngine.Push = false;
        }

        if(InputHandler.Instance.MouseHold)
        {
            leftEngine.Push = true;
            rightEngine.Push = true;
        }
    }
}
