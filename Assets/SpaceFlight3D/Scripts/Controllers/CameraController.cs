using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public NoiseSettings noiseProfile;

    public void SetupNoise()
    {
        //vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        vcam.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = noiseProfile;
    }

    public void DisableNoise()
    {
        vcam.DestroyCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
}
