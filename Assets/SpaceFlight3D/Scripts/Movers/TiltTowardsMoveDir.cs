using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltTowardsMoveDir : MonoBehaviour
{
    public Vector3 forward = Vector3.forward;
    [Range(0, 1)]public float weight;
    Quaternion startRot;
    Vector3 previousPos;

    void Start()
    {
        previousPos = transform.position;
        startRot = transform.rotation;
    }


    void Update()
    {
        Vector3 direction = transform.position - previousPos;
        direction = direction.normalized;
        Quaternion targetRot = Quaternion.LookRotation(forward, direction);
        transform.rotation = Quaternion.Lerp(startRot, targetRot, weight);

        previousPos = transform.position;
    }
}
