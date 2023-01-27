using System;
using UnityEngine;

namespace RopeMechanim
{
    [CreateAssetMenu(fileName = "ConfigurableConfiguration", menuName = "Joint Configuration/Configurable Joint Configuration", order = 1)]
    public class ConfigurableSetup : JointConfig //where T:ConfigurableJoint
    {
        public override Type GetJointType()
        {
            return typeof(ConfigurableJoint);
        }

        [Header("Configurable Joint Specific")]
        public JointProjectionMode projectionMode;
        public float projectionDistance = 1;

        public ConfigurableJointMotion Xmotion;
        public ConfigurableJointMotion Ymotion = ConfigurableJointMotion.Limited;
        public ConfigurableJointMotion Zmotion;
        public ConfigurableJointMotion XangularMotion;
        public ConfigurableJointMotion YangularMotion;
        public ConfigurableJointMotion ZangularMotion;

        public float linearLimit = 1;
        public float linearLimitBounciness;
        public float linearLimitContactDistance;

        public float spring;
        public float damper;

        public override void ConfigureJoint(Joint joint)
        {
            base.ConfigureJoint(joint);

            if(joint is ConfigurableJoint)
            {
                ConfigurableJoint j = joint as ConfigurableJoint;

                j.projectionMode = this.projectionMode;
                j.projectionDistance = this.projectionDistance;

                j.xMotion = this.Xmotion;
                j.yMotion = this.Ymotion;
                j.zMotion = this.Zmotion;
                j.angularXMotion = this.XangularMotion;
                j.angularYMotion = this.YangularMotion;
                j.angularZMotion = this.ZangularMotion;

                SoftJointLimit linearLimit = new SoftJointLimit();
                linearLimit.limit = this.linearLimit;
                linearLimit.bounciness = this.linearLimitBounciness;
                linearLimit.contactDistance = this.linearLimitContactDistance;

                j.linearLimit = linearLimit;

                SoftJointLimitSpring linearLimitSpring = new SoftJointLimitSpring();
                linearLimitSpring.spring = this.spring;
                linearLimitSpring.damper = this.damper;

                j.linearLimitSpring = linearLimitSpring;


                //j.useSpring = this.useSpring;
                //if (j.useSpring)
                //{

                //    JointSpring spring = new JointSpring() { spring = this.spring, damper = damper, targetPosition = targetPosition };
                //    j.spring = spring;
                //}

                //j.useLimits = useLimits;
                //if (j.useLimits)
                //{

                //    JointLimits limits = new JointLimits() { min = minLim, max = maxLim, bounciness = this.bounciness, bounceMinVelocity = this.minBounceVelocity, contactDistance = this.contactDistance };

                //    j.limits = limits;
                //}
            }
        }
    }
}