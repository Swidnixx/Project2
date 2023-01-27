using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonSwitchable<T> : MonoBehaviour where T : SingletonSwitchable<T>
{
    public static T Instance
    {
        get { return _instance; }
    }

    private static T _instance;

    protected virtual void Awake()
    {
        Debug.Log(typeof(T));
        _instance = GetComponent<T>();
    }

    public void Reset()
    {
        _instance = null;
        Debug.Log("Reseting singleton, instance: " + _instance ==null?"null":"Jakiœ");
    }

    public void Switch( T newOne )
    {
        _instance = newOne;
    }
}
