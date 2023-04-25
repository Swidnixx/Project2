using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeMechanim
{

    //[CreateAssetMenu(fileName = "JointConfiguration", menuName = "ScriptableObjects/Static Configurations/Hinge Joint Configuration", order = 1)]
    public class JointConfig : ScriptableObject
        //where T : Joint
    {
        public virtual Type GetJointType()
        {
            return typeof(Joint);
        }

        [TextArea]
        public string description;

        [Header("Rigidbody Configuration")]
        public int layer = 0;
        public bool isKinematic;
        public bool useGravity = true;
        public float mass = 1;
        public float drag;
        public float angularDrag;
        public RigidbodyInterpolation interpolate;

        [Tooltip("Constraints Sum Up")]
        public RigidbodyConstraints constraints1;
        public RigidbodyConstraints constraints2;
        public RigidbodyConstraints constraints3;



        [Header("Joint Configuration")]
        public bool enablePreprocessing;
        public float massScale = 1;
        public float connectedMassScale = 1;
        public bool autoConfigure;
        //public Vector3 anchor;
        public Vector3 axis = Vector3.forward;
        public Vector3 connectedAnchor;

        [Header("Additional")]
        public float pushDownForce;

        public virtual void ConfigureAdditional(GameObject jointObject)
        {
            ConstantForce cf = jointObject.GetComponent<ConstantForce>();
            if(cf)
            {
                cf.force = new Vector3(0, -pushDownForce, 0);
            }
        }

        public virtual void ConfigureRigidbody(Rigidbody rb)
        {
            rb.gameObject.layer = layer;

            //Rigidbody rb = joint.rb;
            rb.interpolation = this.interpolate;

            rb.useGravity = this.useGravity;
            rb.drag = this.drag;
            rb.angularDrag = this.angularDrag;
            rb.mass = this.mass;
            rb.constraints = this.constraints1 | this.constraints2 | this.constraints3;
        }

        public virtual void ConfigureJoint(Joint j)
        {
            //Joint j = joint.joint;

            j.enablePreprocessing = this.enablePreprocessing;

            j.autoConfigureConnectedAnchor = this.autoConfigure;
            j.massScale = this.massScale;
            j.connectedMassScale = this.connectedMassScale;

            if (!j.autoConfigureConnectedAnchor)
            {
                j.axis = this.axis;
                j.connectedAnchor = connectedAnchor;
                //j.anchor = Vector3.zero;
            }

            
        }

        //public virtual void ConfigureAnchor(RopeJoint2 joint)
        //{
        //    Joint j = joint.joint;

        //    if (!j.autoConfigureConnectedAnchor)
        //    {
        //        j.anchor = this.anchor / j.transform.localScale.y;
        //        j.axis = this.axis;
        //        j.connectedAnchor = connectedAnchor;
        //    }
        //}
    }
}