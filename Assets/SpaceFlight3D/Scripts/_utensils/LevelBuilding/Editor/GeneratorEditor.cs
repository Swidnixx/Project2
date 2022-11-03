using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGenerator))]
public class GeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelGenerator generator = (LevelGenerator)target;

        if(GUILayout.Button("Generate"))
        {
            generator.Generate();
        }

        if(GUILayout.Button("Clear"))
        {
            generator.Clear();
        }
    }
}
