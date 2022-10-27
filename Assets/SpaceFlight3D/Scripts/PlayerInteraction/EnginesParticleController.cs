using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginesParticleController : MonoBehaviour
{
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
        velocityModuleL.z = new ParticleSystem.MinMaxCurve(-2.5f);
        velocityModuleR.z = new ParticleSystem.MinMaxCurve(-2.5f);
        emissionModuleL.rateOverTime = new ParticleSystem.MinMaxCurve(50);        
        emissionModuleR.rateOverTime = new ParticleSystem.MinMaxCurve(50);

    }

    public void Steady()
    {
        velocityModuleL.z = new ParticleSystem.MinMaxCurve(-1f);
        velocityModuleR.z = new ParticleSystem.MinMaxCurve(-1f);
        emissionModuleL.rateOverTime = new ParticleSystem.MinMaxCurve(5);
        emissionModuleR.rateOverTime = new ParticleSystem.MinMaxCurve(5);
    }
}
