using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineParticleController : MonoBehaviour
{
    bool thrusting;

    public ParticleSystem[] engines;

    ParticleSystem.VelocityOverLifetimeModule[] velocityModules;
    ParticleSystem.EmissionModule[] emissionModules;

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
    }

    public void Thrust()
    {
        if (!thrusting)
        {
            thrusting = true;

            for(int i=0; i<engines.Length; i++)
            {
                velocityModules[i].z = new ParticleSystem.MinMaxCurve(-15f);
                emissionModules[i].rateOverTime = new ParticleSystem.MinMaxCurve(150);
            }
        }

    }

    public void Steady()
    {
        if (thrusting)
        {
            thrusting = false;

            for(int i=0; i<engines.Length; i++)
            {
                velocityModules[i].z = new ParticleSystem.MinMaxCurve(-1f);
                emissionModules[i].rateOverTime = new ParticleSystem.MinMaxCurve(5);
            }
        }
    }
}
