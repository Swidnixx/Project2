using System.Collections;
using UnityEngine;

public class SpaceShipSteer : MonoBehaviour
{
    public float maxAngle = 45;
    public float rotateSpeed = 1;
    public bool flipLeftRight = true;

    private void Start()
    {
        Input.gyro.enabled = true;
    }

    private void Update()
    {
        if (InputHandler.Instance == null) return;

        float leftRight = InputHandler.Instance.LeftRight;

        transform.rotation = Quaternion.Lerp(transform.rotation, 
            Quaternion.Euler(   transform.rotation.eulerAngles.x, 
                                transform.rotation.eulerAngles.y,
                                maxAngle * leftRight * (flipLeftRight?-1:1)
                                    ), Time.deltaTime * rotateSpeed     );

    }

    public void ResetOrientation()
    {
        StartCoroutine(LerpDefaultRotation());
    }

    IEnumerator LerpDefaultRotation()
    {
        for (float t = 0; t < 1; t+=Time.deltaTime * 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
Quaternion.Euler(transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                maxAngle * 0 * (flipLeftRight ? -1 : 1)
                    ), t);

            yield return null;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                                transform.rotation.eulerAngles.y,
                                                0);
    }

    //private void OnGUI()
    //{
    //    GUI.Box(new Rect(10, 10, 250, 120), "Ship Steer");
    //    GUI.Label(new Rect(15, 30, 245, 60), "Accelerometer: " + InputHandler.Instance.LeftRight);
    //}
}
