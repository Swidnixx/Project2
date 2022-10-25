using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Balast : MonoBehaviour
{
    public UnityEvent OnLose;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Balas Collision");
        if(collision.transform.CompareTag("Terrain"))
        {
            Debug.Log("Balas terrain Collision");
            OnLose?.Invoke();
            Destroy(gameObject);
        }
    }

    public void OnRopeGoinUp()
    {
        HingeJoint joint = GetComponent<HingeJoint>();

        if(joint != null)
        {
            joint.connectedBody.GetComponent<RopeHook>().ResetPickup();
            Destroy(joint);
        }
    }
}
