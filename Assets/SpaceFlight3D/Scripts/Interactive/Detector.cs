using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public LayerMask detectionMask;
    public string detectionTargetTag;
    public float detectionRadius = 0.1f;
    Collider[] colliders;

    public bool Detected => colliders.Length > 0;

    private void Update()
    {
        Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders, detectionMask);
    }

}
