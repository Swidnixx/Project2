using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover : MonoBehaviour
{
    public Transform topPos;
    public Transform bottomPos;

    public Transform obj;

    public float speed = 1;

    public bool startTop = true;

    private void Start()
    {
        if(startTop)
        {
            obj.position = topPos.position;
        }
        else
        {
            obj.position = bottomPos.position;
        }

        SwitchDir();
    }

    IEnumerator MoveTo(Vector3 pos)
    {
        float dist;
        do
        {
            obj.position = Vector3.MoveTowards(obj.position, pos, Time.deltaTime * speed);
            yield return null;
            dist = Vector3.Distance(obj.position, pos); 
        } while (dist > 0.1);

        obj.position = pos;

        SwitchDir();
    }

    void SwitchDir()
    {
        // Have to watchout since u can't move bottom and top transforms during playmode. If you do, switch position might not be working as expected
        if(obj.position == topPos.position)
        {
            StartCoroutine(MoveTo(bottomPos.position));
        }
        else
        {
            StartCoroutine(MoveTo(topPos.position));
        }
    }
}
