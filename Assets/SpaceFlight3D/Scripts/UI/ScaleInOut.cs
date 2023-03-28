using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScaleInOut : MonoBehaviour
{
    public float speed = 1;
    public bool openOnStart;
    new RectTransform transform;

    public UnityEvent OnClose;
    public UnityEvent OnOpen;

    private void Awake()
    {
        transform = (RectTransform)base.transform;
    }

    private void Start()
    {

        //if(openOnStart)
        //{
        //    ScaleUp();
        //}
        //else
        //{
        //    ScaleDown();
        //}
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

            t += Time.unscaledDeltaTime * speed;

            yield return null;
        }

        transform.localScale = Vector3.one;
        OnOpen?.Invoke();
    }
    IEnumerator ScaleOut()
    {
        float t = 0;
        while (t < 1)
        {
            Vector2 lerp = Vector2.Lerp(Vector3.one, Vector3.zero, t);
            transform.localScale = lerp;

            t += Time.unscaledDeltaTime * speed;

            yield return null;
        }

        transform.localScale = Vector3.zero;
        OnClose?.Invoke();
    }
}
