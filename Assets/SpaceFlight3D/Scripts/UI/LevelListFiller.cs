using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelListFiller : MonoBehaviour
{
    public LevelsList list;

    public LevelPanel prefab;

    Transform parent;

    private void Start()
    {
        parent = transform;

        foreach(Object s in list.scenes)
        {
            LevelPanel panel = Instantiate(prefab, parent);
            panel.Setup(s);
        }
    }
}
