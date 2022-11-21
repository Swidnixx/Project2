using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{
    public float speed = 1;
    public Transform target;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
    }

}
