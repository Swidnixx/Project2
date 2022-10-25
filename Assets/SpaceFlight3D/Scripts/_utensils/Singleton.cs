using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    // Property is only for outside
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    // Internally we work with this field only
    private static T _instance;

    private void Awake()
    {
            _instance = this.GetComponent<T>();
        if (_instance == null)
        {

        }
        else
        {
#if UNITY_EDITOR
            //Debug.LogError("Multiple instances of Singleton: " + this.GetType());
#endif
        }

    }
}
