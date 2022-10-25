using System;
using UnityEngine;

namespace RopeMechanim
{
    [CreateAssetMenu(fileName = "HingeConfiguration", menuName = "Joint Configuration/Hinge Joint Configuration", order = 0)]
    public class HingeSetup : JointConfig
    {
        public override Type GetJointType()
        {
            return typeof(HingeJoint);
        }

        [Header("Hinge Joint Configuration")]
        public bool useSpring;
        public float spring;
        public float damper;
        public float targetPosition;

        public bool useMotor;
        public float targetVelocity;
        public float force;
        public bool freeSpin;

        public bool useLimits;
        public float minLim;
        public float maxLim;
        public float bounciness;
        public float minBounceVelocity;
        public float contactDistance;

        public override void ConfigureJoint(Joint joint)
        {
            base.ConfigureJoint(joint);

            if (joint is HingeJoint)
            {
                HingeJoint j = joint as HingeJoint;
                j.useSpring = this.useSpring;
                if (j.useSpring)
                {

                    JointSpring spring = new JointSpring() { spring = this.spring, damper = damper, targetPosition = targetPosition };
                    j.spring = spring;
                }

                j.useMotor = this.useMotor;
                if(j.useMotor)
                {
                    JointMotor motor = new JointMotor() { targetVelocity = this.targetVelocity, force = this.force, freeSpin = this.freeSpin };
                    j.motor = motor;
                }

                j.useLimits = useLimits;
                if (j.useLimits)
                {

                    JointLimits limits = new JointLimits() { min = minLim, max = maxLim, bounciness = this.bounciness, bounceMinVelocity = this.minBounceVelocity, contactDistance = this.contactDistance };

                    j.limits = limits;
                }
            }
            else
            {
                Debug.LogWarning("The passed Joint is not Hinge and was not fully configured!");
            }

        }
    }
}