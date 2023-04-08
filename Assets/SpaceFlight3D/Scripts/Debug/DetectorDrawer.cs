using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorDrawer : MonoBehaviour
{
    public bool draw;
    public Color colorDetected = Color.red;
    public Color colorNotDetected = Color.green;

    public Detector[] detectors;

    private void OnDrawGizmos()
    {
        if(!draw) return;


        foreach(var d in detectors)
        {
            if(d.Detected)
                Gizmos.color = colorDetected;
            else
                Gizmos.color = colorNotDetected;
            Gizmos.DrawSphere(d.transform.position, d.detectionRadius);
        }
    }
}
