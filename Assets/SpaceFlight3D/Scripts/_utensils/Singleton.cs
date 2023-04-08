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

    protected virtual void Awake()
    {
        if (_instance == null )// || _instance == this)
        {
            //Debug.Log("Singleton: " + this.name + " (" + this.GetType() + ")");
            _instance = GetComponent<T>();
           // Debug.Log("Awake Instance:" + _instance.gameObject.name + ", " + _instance.GetType());
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("Multiple instances of Singleton: " + Instance.GetType() + "; " + this.GetType());
#endif
        }
    }

    //private void OnDestroy()
    //{
    //    Debug.Log("Singleton: " + this.name + " destroyed");
    //    Reset();
    //}

    public void Reset()
    {
        Debug.Log("Singleton: " + _instance.name + " destroyed" + " (" + _instance.GetType() + ")");
        _instance = null;
    }
}

public class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}

