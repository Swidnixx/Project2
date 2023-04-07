using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SteeringManual : SteeringRefactored
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
        angleRotation += leftRight * rotateSpeed * Time.deltaTime * 60;
        return Quaternion.AngleAxis(angleRotation * (flipLeftRight ? -1 : 1), axis);
    }
}
