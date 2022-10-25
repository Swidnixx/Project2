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

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void Fade()
    {
        Color c = image.color;
        c.a = 1;
        StartCoroutine(SetColor(c));
    }

    public void Reveal()
    {
        Color c = image.color;
        c.a = 0;
        StartCoroutine(SetColor(c));
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
    }
}
