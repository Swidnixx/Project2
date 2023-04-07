using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyFlyStart : MonoBehaviour
{
    public Rigidbody spaceship;

    private void Start()
    {
        //Block();
    }

    public void Block()
    {
        spaceship.isKinematic = true;
    }

    public void StartFlight()
    {
        spaceship.isKinematic = false;
        Destroy(this.gameObject);
    }
}
