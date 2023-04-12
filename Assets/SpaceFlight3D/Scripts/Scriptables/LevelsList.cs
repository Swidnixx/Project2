using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "LevelsList", menuName = "Levels/List")]
public class LevelsList : ScriptableObject
{

    public UnityEngine.Object[] scenes;

    internal string GetNext(string name)
    {
        Object current = scenes.First(s => s.name == name);
        if (current)
        {
            int currIndex = Array.IndexOf(scenes, current);

            if (scenes.Length > currIndex + 1)
            {
                return scenes[currIndex + 1].name;
            }
        }

        return "";
    }

    //add types and such stuff
}
