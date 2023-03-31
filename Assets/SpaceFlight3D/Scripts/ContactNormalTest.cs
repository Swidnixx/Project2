using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ContactNormalTest : MonoBehaviour
{
    Vector3 point;
    Vector3 normal;
    private void Update()
    {
        RaycastHit hit;
        bool didHit = Physics.Raycast(transform.position, -transform.up, out hit, 5);

        if(didHit)
        {
            point = hit.point;
            normal = hit.normal;
        }

        Debug.DrawRay(point, normal);
    }
}
