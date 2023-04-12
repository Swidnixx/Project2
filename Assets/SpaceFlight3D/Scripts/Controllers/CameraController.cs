using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public NoiseSettings noiseProfile;
    public CinemachineConfiner confiner;

    void Awake() => LevelLoader.SceneLoaded += FindAndSetConfiner;
    void OnDestroy() => LevelLoader.SceneLoaded -= FindAndSetConfiner;

    void FindAndSetConfiner(string name)
    {
        GameObject go = GameObject.Find("CameraConfiner");
        if(go)
        {
            PolygonCollider2D collider = go.GetComponent<PolygonCollider2D>();
            if (collider)
                confiner.m_BoundingShape2D = collider;
        }
    }

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
