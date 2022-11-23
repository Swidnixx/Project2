using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
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
        if (_instance == null || _instance == this)
        {
            Debug.Log("InputHandler: " + this.name + " (" + this.GetType() + ")");
            _instance = this.GetComponent<T>();
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("Multiple instances of Singleton: " + this.GetType());
#endif
        }

    }
}
