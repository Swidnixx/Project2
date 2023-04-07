using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPointer : MonoBehaviour
{
    public Color Color { set { SetColor(value); } }

    public Transform pointer;
    public Target target;

    private void Update()
    {
        if(target == null)
        {
            pointer.gameObject.SetActive(false);
            return;
        }

        if(Vector3.Distance(pointer.position, target.transform.position) < Vector3.Distance(transform.position, pointer.position))
        {
            pointer.gameObject.SetActive(false);
        }
        else
        {
            pointer.gameObject.SetActive(true);
            Vector3 dir = (target.transform.position - pointer.position).normalized;

            pointer.position = transform.position + dir;
            pointer.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        }
    }

    void SetColor(Color color)
    {
        var renderers = GetComponentsInChildren<MeshRenderer>(true);
        Array.ForEach(renderers, r => { r.material.color = color; /*print(r); print(color);*/ } );
    }

    public void AssignTarget(Target newTarget)
    {
        //Debug.Log("Target assigned: " + newTarget.color);
        target = newTarget;
        SetColor(newTarget.color); 
    }

    public void ClearTarget()
    {
        target = null;
    }
}
