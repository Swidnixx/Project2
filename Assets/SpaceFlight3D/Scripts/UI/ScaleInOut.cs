using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScaleInOut : MonoBehaviour
{
    public float speed = 1;
    new RectTransform transform;

    Vector2 topLeft = Vector2.up;
    Vector2 bottomLeft = Vector2.zero;
    Vector2 topRight = Vector2.one;
    Vector2 bottomRight = Vector2.right;

    private void Start()
    {
        transform = (RectTransform)base.transform;

        transform.anchorMin = topLeft;
        transform.anchorMax = topLeft;
    }

    public void Scale()
    {
        StartCoroutine(ScaleIn());
    }
    public void Reset()
    {
        transform.anchorMin = topLeft;
        transform.anchorMax = topLeft;
    }

    IEnumerator ScaleIn()
    {
        float t = 0;
        while(t<1)
        {
            Vector2 lerp = Vector2.Lerp(topLeft, bottomRight, t);
            transform.anchorMin = new Vector2(topLeft.x, lerp.y);
            transform.anchorMax = new Vector2(lerp.x, topLeft.y);

            t += Time.deltaTime * speed;

            yield return null;
        }

        transform.anchorMin = bottomLeft;
        transform.anchorMax = topRight;
    }
}
