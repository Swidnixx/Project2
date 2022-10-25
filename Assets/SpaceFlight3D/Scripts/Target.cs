using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Color color;

    public void ScaleDown()
    {
        Vector3 scale = transform.localScale;
        scale.y = 0;
        StartCoroutine(LerpScaleTo(scale));
    }

    IEnumerator LerpScaleTo(Vector3 targetScale)
    {
        while(transform.localScale.y > 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 3f);
            yield return null;
        }
    }
}
