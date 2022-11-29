using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMissionUpdater : MissionUpdater
{
    public enum InputType
    {
        Tap,
        Thrust,
        FullLeft,
        FullRight
    }

    public InputType inputType;

    private void Update()
    {
        switch (inputType)
        {
            case InputType.Tap:
                if(InputHandler.Instance.MouseDown)
                {
                    UpdateMission();
                }
                break;

            case InputType.Thrust:
                if (InputHandler.Instance.Upwards == 1)
                {
                    UpdateMission();
                }
                break;

            case InputType.FullRight:
                if(InputHandler.Instance.LeftRight == 1)
                {
                    UpdateMission();
                }
                break;

            case InputType.FullLeft:
                if(InputHandler.Instance.LeftRight == -1)
                {
                    UpdateMission();
                }
                break;
        }

    }
}
