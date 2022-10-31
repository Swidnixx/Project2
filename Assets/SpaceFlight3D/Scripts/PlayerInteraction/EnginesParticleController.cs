using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginesParticleController : MonoBehaviour
{
    bool thrusting;

    public ParticleSystem leftEngine;
    public ParticleSystem rightEngine;

    ParticleSystem.VelocityOverLifetimeModule velocityModuleL;
    ParticleSystem.VelocityOverLifetimeModule velocityModuleR;
    ParticleSystem.EmissionModule emissionModuleL;
    ParticleSystem.EmissionModule emissionModuleR;

    private void Start()
    {
        velocityModuleL = leftEngine.velocityOverLifetime;
        velocityModuleR = rightEngine.velocityOverLifetime;
        emissionModuleL = leftEngine.emission;        
        emissionModuleR = rightEngine.emission;
    }

    public void Thrust()
    {
        if (!thrusting)
        {
            thrusting = true;

            velocityModuleL.z = new ParticleSystem.MinMaxCurve(-15f);
            velocityModuleR.z = new ParticleSystem.MinMaxCurve(-15f);
            emissionModuleL.rateOverTime = new ParticleSystem.MinMaxCurve(150);
            emissionModuleR.rateOverTime = new ParticleSystem.MinMaxCurve(150); 
        }

    }

    public void Steady()
    {
        if (thrusting)
        {
            thrusting = false;

            velocityModuleL.z = new ParticleSystem.MinMaxCurve(-1f);
            velocityModuleR.z = new ParticleSystem.MinMaxCurve(-1f);
            emissionModuleL.rateOverTime = new ParticleSystem.MinMaxCurve(5);
            emissionModuleR.rateOverTime = new ParticleSystem.MinMaxCurve(5); 
        }
    }
}
