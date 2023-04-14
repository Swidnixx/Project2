using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatoer : MonoBehaviour
{
    public Vector3 axis = Vector3.up;

    public float speed = 1;

    private void Update()
    {
        transform.Rotate(axis * Time.deltaTime * speed, Space.Self);
    }
}
