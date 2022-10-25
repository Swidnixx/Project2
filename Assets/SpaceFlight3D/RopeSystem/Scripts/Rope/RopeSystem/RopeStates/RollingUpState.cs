using UnityEngine;

namespace RopeMechanim
{
    internal class RollingUpState : RopeState
    {
        Rigidbody lastJointRb => ropeMechanim.lastJoint.GetComponent<Rigidbody>();

        public RollingUpState(RopeMechanim ropeMechanim) : base(ropeMechanim) { }

        public override void OnEnter()
        {
            ropeMechanim.ropeBuilder.StartRollingUp();
        }

        public override void FixedUpdate()
        {
            //ropeMechanim.lastJoint.position = Vector3.MoveTowards(ropeMechanim.lastJoint.position, ropeMechanim.transform.position, ropeMechanim.dropSpeed * Time.deltaTime);
            //Vector3 newPos = Vector3.MoveTowards(ropeMechanim.lastJoint.position, ropeMechanim.transform.position, ropeMechanim.dropSpeed * Time.deltaTime);
            //lastJointRb.MovePosition(newPos);

            if (ropeMechanim.ropeBuilder.UpdateLastJoint(Time.deltaTime * ropeMechanim.dropSpeed * -1))//|| (ropeMechanim.lastJoint.position - ropeMechanim.transform.position).magnitude < 0.1) //
            {
                if (ropeMechanim.lastJoint == ropeMechanim.hook)
                {
                    ropeMechanim.ropeBuilder.FinishRollingUp();
                    ropeMechanim.ChangeState(new RolledUpState(ropeMechanim));
                    return;
                }
                ropeMechanim.ropeBuilder.DestroyLastJoint();
            }
        }
    }
}