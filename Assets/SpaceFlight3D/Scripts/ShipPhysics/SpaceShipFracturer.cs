using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShipFracturer : MonoBehaviour
{
    public GameObject fullModel;
    public GameObject fracturedModel;

    public int blinkCountOnDeath = 3;
    public float timeToBlinkAfterDeath = 3;
    public float blinkTime = 0.1f;
    public float timeBetweenBlinks = 0.5f;

    public UnityEvent OnFracture;
    public UnityEvent OnRevive;

    MeshRenderer[] renderers;

    private void Start()
    {
        renderers = fracturedModel.GetComponentsInChildren<MeshRenderer>();
    }

    public void Fracture()
    {
        gameObject.layer = LayerMask.NameToLayer("Respawn");

        fullModel.SetActive(false);
        fracturedModel.transform.parent = null;
        fracturedModel.SetActive(true);
        StartCoroutine(RevivePlayer());

        OnFracture?.Invoke();
    }

    IEnumerator RevivePlayer()
    {
        yield return new WaitForSeconds(timeToBlinkAfterDeath);
        for(int i=0; i<blinkCountOnDeath; i++)
        {
            Array.ForEach(renderers, r => r.enabled = false);
            yield return new WaitForSeconds(blinkTime);
            Array.ForEach(renderers, r => r.enabled = true);

            yield return new WaitForSeconds(timeBetweenBlinks);
        }

        Array.ForEach(renderers, r => r.transform.localPosition = Vector3.zero);
        fracturedModel.transform.parent = transform;
        fracturedModel.SetActive(false);

        Revive();

    }

    void Revive()
    {
        OnRevive?.Invoke();
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
