using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LateActivator : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
        LevelLoader.SceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy() => LevelLoader.SceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(string name)
    {
        Activate();
    }

    async void Activate()
    {
        await Task.Yield();
        gameObject.SetActive(true);
    }
}
