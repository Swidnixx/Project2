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
        if (_instance == null)
        {
            Debug.Log("Singleton of type: " + typeof(T) + " was created");
            _instance = GetComponent<T>();
        }
        else
        {
            Destroy(this);
        }
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
