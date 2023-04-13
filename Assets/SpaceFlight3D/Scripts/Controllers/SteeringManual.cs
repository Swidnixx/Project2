using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringManual : SteeringRefactored
{
    float angleRotation = 0;

    //this needs to be done not to apply any lateral forces
    private void OnEnable()
    {
        force = 0;
    }

    protected override Quaternion SetTargetRotation(float leftRight)
    {
        //angleRotation += leftRight * rotateSpeed * maxAngle * Time.deltaTime;
        angleRotation -= leftRight * rotateSpeed * Time.unscaledDeltaTime * 100;

        angleRotation = Mathf.Clamp(angleRotation, -maxAngle, maxAngle);

        if(angleRotation == 360)
        {
            angleRotation = 0;
        }
        else if(angleRotation == -360)
        {
            angleRotation = 0;
        }

        return Quaternion.AngleAxis(angleRotation * (flipLeftRight ? -1 : 1), axis);
    }
}
