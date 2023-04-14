using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringManual : SteeringRefactored
{

    //this needs to be done not to apply any lateral forces
    private void OnEnable()
    {
        force = 0;
    }

    protected override Quaternion SetTargetRotation(float leftRight)
    {
        //angleRotation += leftRight * rotateSpeed * maxAngle * Time.deltaTime;
        rotation -= leftRight * rotateSpeed * Time.unscaledDeltaTime * 100;

        rotation = Mathf.Clamp(rotation, -maxAngle, maxAngle);

        if(rotation == 360)
        {
            rotation = 0;
        }
        else if(rotation == -360)
        {
            rotation = 0;
        }

        return Quaternion.AngleAxis(rotation * (flipLeftRight ? -1 : 1), axis);
    }
}
