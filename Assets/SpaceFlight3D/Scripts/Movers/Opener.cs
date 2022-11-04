using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : MonoBehaviour
{
    public bool automatic;
    public bool closed = true;
    public Transform objectToMove;
    public Transform openPos;
    public Transform closedPos;

    public float speed = 1;

    public float closeAfterAWhile = 0;
    public float openAfterAWhile = 0;

    private void Start()
    {
        if(closed)
        {
            objectToMove.position = closedPos.position;
            if (automatic)
            {
                Open();
            }
        }
        else
        {
            objectToMove.position = openPos.position;
            if (automatic)
            {
                Close();
            }
        }


    }

    public void Open()
    {
        StopAllCoroutines();
        StartCoroutine(Move(openPos.position));
        if (closeAfterAWhile > 0)
        {
            Invoke(nameof(Close),closeAfterAWhile);
        }
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(Move(closedPos.position));
        if (openAfterAWhile > 0)
        {
            Invoke(nameof(Open), openAfterAWhile);
        }
    }

    IEnumerator Move(Vector3 pos)
    {
        while (Vector3.Distance(objectToMove.position, pos) > 0.01)
        {
            objectToMove.position = Vector3.MoveTowards(objectToMove.position, pos, Time.deltaTime * speed);
            yield return null;
        }

        objectToMove.position = pos;
    }
}
