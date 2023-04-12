using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "LevelsList", menuName = "Levels/List")]
public class LevelsList : ScriptableObject
{

    public string[] scenes;

    internal string GetNext(string name)
    {
        try
        {

                int currIndex = Array.IndexOf(scenes, name);

                if (scenes.Length > currIndex + 1)
                {
                    return scenes[currIndex + 1];
                }
            
        }
        catch
        {

        }


        return "";
    }

    //add types and such stuff
}
