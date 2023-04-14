using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipCameraSetup : MonoBehaviour
{
    public Transform camera3D;
    public CameraSettings settings;

    private void Start()
    {
        settings.Init(camera3D);
    }

    [Serializable]
    public class CameraSettings
    {
        Transform camera;

        public Slider distanceSlider;
        public Text distanceText;

        public void Init(Transform camera)
        {
            this.camera = camera;

            SetDistance(camera.localPosition.z);
            distanceSlider.value = camera.localPosition.z;
            distanceSlider.onValueChanged.AddListener(SetDistance);
        }

        void SetDistance(float distance)
        {
            camera.localPosition = new Vector3(0, 0, distance);
            distanceText.text = distance.ToString("n2");
        }

    }
}
