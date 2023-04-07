using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CarouselItem : Selectable
{
    public float slideSpeed = 1000;

    public override void Select()
    {
        Selectable[] selectables = GetComponentsInChildren<Selectable>();
        Selectable selected = selectables[1];
        selected.Select();
        Debug.Log(selected + " selected from " + gameObject);
    }

    internal void GoRight()
    {
        (transform as RectTransform).anchoredPosition = Vector2.zero;
        StartCoroutine(MoveItemToPosition(new Vector2(2000, 0), TurnGameObjectOff));
    }

    internal void GoLeft()
    {
        (transform as RectTransform).anchoredPosition = Vector2.zero;
        StartCoroutine(MoveItemToPosition(new Vector2(-2000, 0), TurnGameObjectOff));
    }

    internal void GoUp()
    {
        //(transform as RectTransform).anchoredPosition = Vector2.zero;
        StartCoroutine(MoveItemToPosition(new Vector2((transform as RectTransform).anchoredPosition.x, 2000), TurnGameObjectOff));
    }

    internal void GoDown()
    {
       // (transform as RectTransform).anchoredPosition = Vector2.zero;
        StartCoroutine(MoveItemToPosition(new Vector2((transform as RectTransform).anchoredPosition.x, -2000), TurnGameObjectOff));
    }

    IEnumerator MoveItemToPosition(Vector2 position)
    {
        RectTransform myTransform = transform as RectTransform;
        while( !Mathf.Approximately(Vector2.Distance(myTransform.anchoredPosition, position), 0) )
        {
            myTransform.anchoredPosition = Vector2.MoveTowards(myTransform.anchoredPosition, position, Time.unscaledDeltaTime * slideSpeed);
            yield return null;
        }
    }
    IEnumerator MoveItemToPosition(Vector2 position, Action callback)
    {
        RectTransform myTransform = transform as RectTransform;
        while ( Vector2.Distance(myTransform.anchoredPosition, position) > 0.01f)
        {
            Vector2 currentTargetPos = Vector2.MoveTowards(myTransform.anchoredPosition, position, Time.unscaledDeltaTime * slideSpeed);
            Debug.Log(gameObject + ": " + (myTransform.anchoredPosition.y - currentTargetPos.y));
            myTransform.anchoredPosition = currentTargetPos;
            yield return null;
        }
        callback?.Invoke();
    }

    internal void FromRight(Action callback)
    {
        callback += TurnGameObjectOn;
        (transform as RectTransform).anchoredPosition = Vector2.right * 1000;
        StartCoroutine(MoveItemToPosition(Vector2.zero, callback));
    }

    internal void FromLeft(Action callback)
    {
        callback += TurnGameObjectOn;
        (transform as RectTransform).anchoredPosition = Vector2.right * -1000;
        StartCoroutine(MoveItemToPosition(Vector2.zero, callback));
    }

    internal void FromUp(Action callback)
    {
        callback += TurnGameObjectOn;
        (transform as RectTransform).anchoredPosition = new Vector2((transform as RectTransform).anchoredPosition.x, 1000);
        Vector2 targetPos = new Vector2((transform as RectTransform).anchoredPosition.x, 0);
        StartCoroutine(MoveItemToPosition(targetPos, callback));
    }

    internal void FromDown(Action callback)
    {
        callback += TurnGameObjectOn;
        (transform as RectTransform).anchoredPosition = new Vector2((transform as RectTransform).anchoredPosition.x,  -1000);
        Vector2 targetPos = new Vector2((transform as RectTransform).anchoredPosition.x, 0);
        StartCoroutine(MoveItemToPosition(targetPos, callback));
    }

    IEnumerator ActionInvoke( float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    public void TurnGameObjectOn()
    {
        gameObject.SetActive(true);
    }

    public void TurnGameObjectOff()
    {
        gameObject.SetActive(false);
    }
}
