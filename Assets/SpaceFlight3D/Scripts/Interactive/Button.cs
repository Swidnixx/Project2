using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public Transform normalPosition;
    public Transform pressedPosition;

    public Transform movableButton;

    public float pressSpeed = 1;

    public UnityEvent OnPress;

    private void Start()
    {
        movableButton.position = normalPosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something hit");
        StartCoroutine(Move(pressedPosition.position));
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(Move(normalPosition.position));
    }

    IEnumerator Move(Vector3 targetPos)
    {
        while( Vector3.Distance(movableButton.position, targetPos) > 0.01 )
        {
            movableButton.position = Vector3.MoveTowards(movableButton.position, targetPos, Time.deltaTime * pressSpeed);
            yield return null;
        }

        OnPress?.Invoke();
    }
}
