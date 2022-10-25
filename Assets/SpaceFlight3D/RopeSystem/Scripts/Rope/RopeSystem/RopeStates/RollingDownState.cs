using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeMechanim
{
    internal class RollingDownState : RopeState
    {
        Transform hook => ropeMechanim.hook;
        Transform lastJoint => ropeMechanim.lastJoint;
        Transform handlePlacement => ropeMechanim.transform;
        float ropeLength => ropeMechanim.ropeLength;
        float distanceBetweenJoints => ropeMechanim.distanceBetweenJoints;

        int jointsCount;

        public RollingDownState(RopeMechanim ropeMechanim) : base(ropeMechanim) { }

        public override void OnEnter()
        {
            ropeMechanim.ropeBuilder.StartRollingDown();
            hook.position -= Vector3.up * 0.01f;

            jointsCount = (int)Mathf.Ceil(ropeLength / distanceBetweenJoints);
        }

        public override void FixedUpdate()
        {
            if (jointsCount <= 0)//Vector3.Distance(hook.position, handlePlacement.position) >= ropeLength)
            {
                ropeMechanim.ropeBuilder.FinishRollingDown();
                ropeMechanim.ChangeState(new RolledDownState(ropeMechanim));
                return;
            }

            float handleToPlacementDistance = Vector3.Distance(lastJoint.position, handlePlacement.position);
            //Debug.Log(handleToPlacementDistance);
            if (handleToPlacementDistance + Time.deltaTime * ropeMechanim.dropSpeed < ropeMechanim.distanceBetweenJoints)
            {
                ropeMechanim.ropeBuilder.UpdateLastJoint(Time.deltaTime * ropeMechanim.dropSpeed);
            }
            else
            {
                ropeMechanim.ropeBuilder.BuildJoint();
                jointsCount--;
            }
        }
    }
}