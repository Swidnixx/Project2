using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up* 10000);
            collision.gameObject.GetComponent<SpaceShipDestroyer>().Crash(collision.contacts[0].point, collision.contacts[0].normal);
        }
    }
}
