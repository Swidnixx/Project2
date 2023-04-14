using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fructurer : MonoBehaviour
{
    public GameObject fullModel;
    public GameObject fracturedModel;

    public UnityEvent OnFracture;

    MeshRenderer[] renderers;

    public string destructingTag = "Player";

    private void Start()
    {
        renderers = fracturedModel.GetComponentsInChildren<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag(destructingTag))
        {
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            if(rb.velocity.magnitude > 3)
            {
                Fracture();
            }
        }
    }

    public void Fracture()
    {
     //   gameObject.layer = LayerMask.NameToLayer("Respawn");

        fullModel.SetActive(false);
        fracturedModel.transform.parent = null;
        fracturedModel.SetActive(true);
        Destroy(gameObject);

        OnFracture?.Invoke();
    }
}
