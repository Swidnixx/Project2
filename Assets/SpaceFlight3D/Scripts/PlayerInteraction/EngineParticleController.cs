using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineParticleController : MonoBehaviour
{
    public ParticleSystem[] engines;
    public float thrustSpeedZ = -10;
    public float steadySpeedZ = -2;
    public float thrustRate = 100;
    public float steadyRate = 10;
    
    bool thrusting;
    ParticleSystem.VelocityOverLifetimeModule[] velocityModules;
    ParticleSystem.EmissionModule[] emissionModules;
    ParticleSystemRenderer[] rendererModules;

    //Initialize PS modules not to retrieve them every frame
    private void Start()
    {
        velocityModules = new ParticleSystem.VelocityOverLifetimeModule[engines.Length];
        for(int i=0; i<engines.Length; i++)
        {
            velocityModules[i] = engines[i].velocityOverLifetime;
        }

        emissionModules = new ParticleSystem.EmissionModule[engines.Length];
        for(int i=0; i<engines.Length; i++)
        {
            emissionModules[i] = engines[i].emission;
        }

        rendererModules = new ParticleSystemRenderer[engines.Length];
        for (int i = 0; i < engines.Length; i++)
        {
            rendererModules[i] = engines[i].GetComponent<ParticleSystemRenderer>();
        }
    }

    // called repeatadly
    public void Thrust()
    {
        if (!thrusting)
        {
            thrusting = true;

            for(int i=0; i<engines.Length; i++)
            {
                velocityModules[i].z = new ParticleSystem.MinMaxCurve(thrustSpeedZ);
                emissionModules[i].rateOverTime = new ParticleSystem.MinMaxCurve(thrustRate);
                StartCoroutine(IncreaseParticleLength());
               // rendererModules[i].lengthScale -= 0.1f;
              //  rendererModules[i].lengthScale += Mathf.Clamp(rendererModules[i].lengthScale, 0, -5);
            }
        }
    }
    IEnumerator IncreaseParticleLength()
    {
        float length = rendererModules[0].lengthScale;
        while (length > -5)
        {
            for (int i = 0; i < engines.Length; i++)
            {
                rendererModules[i].lengthScale = length;
                length -= 0.1f;
            }

            yield return null;
        }
    }

    public void Steady()
    {
        if (thrusting)
        {
            thrusting = false;

            for(int i=0; i<engines.Length; i++)
            {
                velocityModules[i].z = new ParticleSystem.MinMaxCurve(steadySpeedZ);
                emissionModules[i].rateOverTime = new ParticleSystem.MinMaxCurve(steadyRate);
                rendererModules[i].lengthScale = -1;
            }
        }
    }
}
