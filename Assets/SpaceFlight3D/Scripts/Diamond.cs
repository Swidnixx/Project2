using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Diamond : MonoBehaviour
{
    public ParticleSystem destroyEffect;
    public UnityEvent onDestroy;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            destroyEffect.transform.position = transform.position;
            destroyEffect.Play();
            destroyEffect.transform.parent = null;

            onDestroy?.Invoke();

            Destroy(gameObject);
        }
    }

}
