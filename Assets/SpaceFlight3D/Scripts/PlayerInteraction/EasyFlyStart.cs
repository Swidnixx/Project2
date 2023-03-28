using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyFlyStart : MonoBehaviour
{
    public Rigidbody spaceship;

    private void Start()
    {
        spaceship.isKinematic = true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            spaceship.isKinematic = false;
            Destroy(this.gameObject);
        }
    }
}
