using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector3 dir = Vector3.right;
    public float dist = 5;
    public LayerMask raycastMask;

    LineRenderer lr;

    private void Start()
    {
        lr = GetComponentInChildren<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, lr.transform.localPosition);
        lr.SetPosition(1, lr.transform.localPosition + dir * dist);
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, dir);
        Debug.DrawRay(transform.position, dir);
        bool didHit = Physics.Raycast(ray, out hit, dist, raycastMask);

        if(!didHit)
        {
            lr.SetPosition(1, lr.transform.localPosition + dir * dist);
        }
        else
        {
            Debug.Log(hit);
            lr.SetPosition(1, lr.transform.InverseTransformPoint(hit.point));
        }

        if(didHit)
        {
            if(hit.collider.CompareTag("Player"))
            {
                hit.collider.transform.GetComponent<SpaceShipDestroyer>().Crash();
            }
        }
    }
}
