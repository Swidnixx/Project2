using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public Joystick joystick;

    public float maxAngle = 45;
    public float rotateSpeed = 1;
    public float sidePower = 50;
    public bool flipLeftRight = true;

    Rigidbody rb;
    SpaceShipStabilizer stabilizer;
    SpaceShipEngine engine;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        stabilizer = GetComponent<SpaceShipStabilizer>();
        engine = GetComponent<SpaceShipEngine>();
    }
    float leftRight;
    private void FixedUpdate()
    {
        //Debug.Log("LeftRightL:" + leftRight);
        rb.AddForce(transform.right * sidePower * leftRight * (flipLeftRight?-1:1));
        #region SideForce
        //if (leftRight > 0.2)
        //{
        //    rb.AddForce(transform.right * (engine.MaxPower - Physics.gravity.y * rb.mass) * leftRight);
        //}

        //if(leftRight < -0.2)
        //{
        //    rb.AddForce(transform.right * (engine.MaxPower - Physics.gravity.y * rb.mass) * leftRight);
        //}    
        #endregion
    }

    private void Update()
    {
        #region rotation (Same as SpaceShipSteer
        leftRight = joystick.Horizontal;

        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.Euler(transform.rotation.eulerAngles.x,
                                transform.rotation.eulerAngles.y,
                                maxAngle * leftRight * (flipLeftRight ? 1 : -1)
                                    ), Time.deltaTime * rotateSpeed);
        #endregion



        if (joystick.Vertical > 0)
        {
            engine.Push = true;
            stabilizer.Power = 0.5f;
        }
        else if( joystick.Vertical < 0)
        {
            engine.Push = false;
            stabilizer.Power = 0;
        }
        else
        {
            engine.Push = false;
            stabilizer.Power = 1;
        }
    }
}
