using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public float fadeSpeed = 1;
    public UnityEvent OnFade;
    Image image;

    public bool destroyOnFade = true;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Fade()
    {
        Color c = image.color;
        c.a = 1;
        StartCoroutine(SetColor(c));
    }

    internal void FadeImmediately()
    {
        Color c = image.color;
        c.a = 1;
        image.color = c;
    }

    public void Reveal()
    {
        Color c = image.color;
        c.a = 0;
        StartCoroutine(SetColor(c));
    }
    internal void RevealImmediately()
    {
        Color c = image.color;
        c.a = 0;
        image.color = c;
    }

    IEnumerator SetColor(Color c)
    {
        Color startC = image.color;
        for(float t=0; t<1; t+=Time.deltaTime * fadeSpeed)
        {
            image.color = Color.Lerp(startC, c, t);
            yield return null;
        }

        image.color = c;
        OnFade?.Invoke();

        if(destroyOnFade)
        {
            Destroy(gameObject);
        }
    }

}
