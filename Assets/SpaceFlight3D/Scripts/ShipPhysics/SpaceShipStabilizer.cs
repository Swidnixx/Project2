using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceShipStabilizer : MonoBehaviour
{
    public float Power { get { return power; } set { value = Mathf.Clamp(value, 0, 1); power = value; } }
    protected float Mass { get { return rb.mass + childrenBodies.Sum(x => !x.isKinematic ? x.mass : 0); } }

    [Range(0, 1)]
    [SerializeField]protected float power = 1;
    [Range(0,1)]
    [SerializeField]protected float velocityCounterForce = 1;

    protected Rigidbody rb;
    protected Rigidbody[] childrenBodies;
    protected Vector3 force;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Rigidbody[] tmp = GetComponentsInChildren<Rigidbody>();
        childrenBodies = new Rigidbody[tmp.Length - 1];
        for(int i=0; i< childrenBodies.Length; i++)
        {
            childrenBodies[i] = tmp[i + 1];
        }
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, transform.up);
        Debug.DrawRay(transform.position, force / Mass / -Physics.gravity.y, Color.cyan);
        //Debug.Log(Mass);
    }

    private void FixedUpdate()
    {
        AddForce();
    }

    protected virtual void AddForce()
    {
        force = power * -Physics.gravity * Mass + -rb.velocity * velocityCounterForce;

        rb.AddForce(force);
    }
}
