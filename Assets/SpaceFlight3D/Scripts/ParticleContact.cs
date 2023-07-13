using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleContact : MonoBehaviour
{
    public LayerMask effectMask;
    public Transform effect;
    public float distance;

    ParticleSystem ps;
    ParticleSystem.MainModule pmm;
    ParticleSystem.EmissionModule pem;
    ParticleSystem.ShapeModule psm;

    private void Start()
    {
        ps = effect.GetComponent<ParticleSystem>();
        pem = ps.emission;
        psm = ps.shape;
        pmm = ps.main;

    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        if( Physics.Raycast(ray, out hit, distance, effectMask) )
        {
            effect.position = hit.point + hit.normal*0.1f;
            effect.up = hit.normal;

            float dist01 = hit.distance / distance;
            pem.rateOverTimeMultiplier = (1-dist01) * 45;

            psm.radius = Mathf.Clamp((1 - dist01) * 2, 0.2f, 2);

            pmm.startSize = Mathf.Clamp((1 - dist01) * 4, 0.25f, 4);

            if(InputHandler.Instance.Upwards == 0)
            {
                pem.rateOverTimeMultiplier = (1 - dist01) * 25;

               // psm.radius = Mathf.Clamp((1 - dist01) * 1, 0.2f, 2);

                pmm.startSize = Mathf.Clamp((1 - dist01) * 1.5f, 0.25f, 2);
            }
        }
    }
}
