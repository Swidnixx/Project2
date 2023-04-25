using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Assertions;

namespace RopeMechanim
{
    [CreateAssetMenu]
    public class RopeBuilder : ScriptableObject, IRopeBuilder
    {
        private RopeMechanim ropeMechanim;
        private List<RopeJoint> joints => ropeMechanim.joints; 
        private JointFactory factory;

        [SerializeField] JointConfig hookSetup;
        [SerializeField] JointConfig jointsSetup;
        [SerializeField] JointConfig lastJointSetup;

        RopeJoint hook => joints.Find(j => j.IsHook);
        protected RopeJoint lastJoint => joints.Last();

        #region Builder Initialization
        public virtual void ResetBuilder(RopeMechanim rope)
        {
            ropeMechanim = rope;
            factory = rope.jointFactory;

            if (joints != null)
            {
                foreach (RopeJoint joint in joints)
                {
                    if (joint)
                    {
                        Destroy(joint.gameObject); 
                    }
                }
                joints.Clear();
            }

            Assert.AreEqual(0, joints.Count);
        }
        #endregion

        #region Starting
        public virtual void BuildHook()
        {
            RopeJoint hook = factory.SpawnHook();

            ConfigureConnection(hook, hookSetup);
            ConfigureNeighbours(hook);
            ConfigureParent(hook);
            //ConfigurePreviousJoint(hook);

            hook.IsHook = true;
            joints.Add(hook);

            
        }
        public virtual void StartRollingDown()
        {
            if(hook == null)
            {
                BuildHook();
            }

            hook.SetDynamic();
        }
        public virtual void StartRollingUp()
        {
            //lastJoint.SetKinematic();
        }
        #endregion

        #region Updating
        public void BuildJoint(bool last)
        {
            RopeJoint newJoint = factory.SpawnJoint();

            if (!last)
            {
                ConfigureConnection(newJoint, jointsSetup); 
            }
            else
            {
                ConfigureConnection(newJoint, lastJointSetup);
            }
            ConfigureNeighbours(newJoint);
            ConfigureParent(newJoint);
            ConfigurePreviousJoint(newJoint);
            newJoint.SetDynamic();

            joints.Add(newJoint);

        }
        public virtual bool UpdateLastJoint(float deltaPosition)
        {
            //Debug.Log($"Current anchor y: {lastJoint.Anchor.y}; DeltaPos: {deltaPosition}");
            Vector3 lastJointAnchor = lastJoint.Anchor; 

            if (lastJointAnchor.y + deltaPosition < 0)
            {
                lastJointAnchor.y = 0;
                return true;
            }
            lastJointAnchor.y += deltaPosition;
            lastJoint.SetAnchor( lastJointAnchor );
            return false;
        }

        public void DestroyLastJoint()
        {
            RopeJoint tmp = lastJoint;
            joints.Remove(lastJoint);
            Destroy(tmp.gameObject);

            if (lastJoint.IsHook)
            {
                lastJoint.ConnectTo(ropeMechanim.rb, hookSetup,
                    new Vector3(0, ropeMechanim.distanceBetweenJoints, 0)); 
            }
            else
            {
                lastJoint.ConnectTo(ropeMechanim.rb, jointsSetup,
                    new Vector3(0, ropeMechanim.distanceBetweenJoints, 0));
            }
            //lastJoint.Disconnect();
            //lastJoint.SetKinematic(); // to refactor (RollingUp using UpdateLastJoint())
        }
        #endregion

        #region Finishing
        public virtual void FinishRollingDown()
        {
            //Vector3 handleFromPlacement = ropeMechanim.transform.position - lastJoint.transform.position;
            //int leftToSpawn = (int)(handleFromPlacement.magnitude / ropeMechanim.distanceBetweenJoints);
            //for (int i = 1; i <= leftToSpawn; i++)
            //{
            //    RopeJoint2 newJoint = factory.SpawnJoint();

            //    ConfigureConnection(newJoint, jointsSetup, ropeMechanim.distanceBetweenJoints * i);
            //    ConfigureRigidbody(newJoint, jointsSetup);
            //    ConfigureNeighbours(newJoint);
            //    ConfigureParent(newJoint);
            //    ConfigurePreviousJoint(newJoint);
            //    ActivateJoint(newJoint);

            //}
        }
        public virtual void FinishRollingUp()
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region Joints Configuration
        protected virtual void ConfigureConnection(RopeJoint joint, JointConfig config)
        {
            joint.transform.position = ropeMechanim.transform.position;
            joint.ConnectTo(ropeMechanim.rb, config, Vector3.zero);
            //joint.SetKinematic();
        }

        protected virtual void ConfigureNeighbours(RopeJoint joint)
        {
        }

        protected virtual void ConfigureParent(RopeJoint joint)
        {
            joint.transform.parent = null;
        }

        protected virtual void ConfigurePreviousJoint(RopeJoint newJoint)
        {
            if (joints.Count == 0)
                return;
            lastJoint.ConnectTo(newJoint, lastJoint.IsHook?hookSetup:jointsSetup, 
                new Vector3(0, ropeMechanim.distanceBetweenJoints, 0));
            lastJoint.SetDynamic();
        }
        #endregion
    }
}