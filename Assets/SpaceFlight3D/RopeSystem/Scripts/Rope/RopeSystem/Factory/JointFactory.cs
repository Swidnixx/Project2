using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeMechanim
{
    // Rope Joint Factory initializes Rope Joints with initial settings
    public class JointFactory : MonoBehaviour
    {
        [SerializeField]
        private RopeJoint jointPrefab;
        [SerializeField]
        private RopeJoint hookPrefab;

        public RopeJoint SpawnJoint()
        {
            RopeJoint newJoint = Instantiate(jointPrefab);
            return newJoint;
        }
        public RopeJoint SpawnHook()
        {
            RopeJoint newHook = Instantiate(hookPrefab);
            return newHook;
        }
    }
}