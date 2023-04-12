using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Diamond : MonoBehaviour
{
    public ParticleSystem destroyEffect;
    public UnityEvent onDestroy;

    public DiamondType type;
    public enum DiamondType {Big, Small }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            destroyEffect.transform.position = transform.position;
            destroyEffect.Play();
            destroyEffect.transform.parent = null;

            onDestroy?.Invoke();

            Destroy(gameObject);

            ScoreKeeper.Instance.CollectDiamond(type);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider.CompareTag("Player"))
    //    {
    //        destroyEffect.transform.position = transform.position;
    //        destroyEffect.Play();
    //        destroyEffect.transform.parent = null;

    //        onDestroy?.Invoke();

    //        Destroy(gameObject);
    //    }
    //}

}
