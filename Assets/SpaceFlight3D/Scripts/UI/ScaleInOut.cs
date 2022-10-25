using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScaleInOut : MonoBehaviour
{
    public float speed = 1;
    new RectTransform transform;

    private void Start()
    {
        transform = (RectTransform)base.transform;
    }

    public void ScaleUp()
    {
        StartCoroutine(ScaleIn());
    }
    public void ScaleDown()
    {
        StartCoroutine(ScaleOut());
    }

    IEnumerator ScaleIn()
    {
        float t = 0;
        while(t<1)
        {
            Vector2 lerp = Vector2.Lerp(Vector3.zero, Vector3.one, t);
            transform.localScale = lerp;

            t += Time.deltaTime * speed;

            yield return null;
        }

        transform.localScale = Vector3.one;
    }
    IEnumerator ScaleOut()
    {
        float t = 0;
        while (t < 1)
        {
            Vector2 lerp = Vector2.Lerp(Vector3.one, Vector3.zero, t);
            transform.localScale = lerp;

            t += Time.deltaTime * speed;

            yield return null;
        }

        transform.localScale = Vector3.zero;
    }
}
