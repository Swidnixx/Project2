using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputInvoker : MonoBehaviour
{
    public enum InputType
    {
        Tap,
        Thrust,
        FullLeft,
        FullRight
    }

    public InputType inputType;

    public UnityEvent OnInput;

    public bool singleUsage = true;

    private void Update()
    {
        switch (inputType)
        {
            case InputType.Tap:
                if (InputHandler.Instance.MouseDown)
                {
                    Invoke();
                }
                break;

            case InputType.Thrust:
                if (InputHandler.Instance.Upwards == 1)
                {
                    Invoke();
                }
                break;

            case InputType.FullRight:
                if (InputHandler.Instance.LeftRight == 1)
                {
                    Invoke();
                }
                break;

            case InputType.FullLeft:
                if (InputHandler.Instance.LeftRight == -1)
                {
                    Invoke();
                }
                break;
        }
    }

    void Invoke()
    {
        OnInput.Invoke();
        if(singleUsage)
        {
            Destroy(gameObject);
        }
    }
    
}
