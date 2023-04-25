using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeMechanim
{
    [CreateAssetMenu]
    public class LimitBasedRopeBuilder : RopeBuilder
    {
        public override bool UpdateLastJoint(float deltaPosition)
        {
            if (lastJoint.Joint is ConfigurableJoint)
                return UpdateLastJointLimit(deltaPosition);

            return base.UpdateLastJoint(deltaPosition);
        }
        public bool UpdateLastJointLimit(float deltaPosition)
        {
            float lastJointLimit = lastJoint.Limit;
            if (lastJointLimit + deltaPosition < 0)
            {
                lastJoint.SetLimit( 0 );
                return true;
            }
            lastJoint.SetLimit( lastJoint.Limit + deltaPosition );
            return false;
        }
    }
}
