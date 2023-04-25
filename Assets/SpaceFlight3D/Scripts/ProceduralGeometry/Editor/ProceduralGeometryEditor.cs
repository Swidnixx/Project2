using ProceduralGen;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProceduralGeometry))]
public class ProceduralGeometryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ProceduralGeometry instance = (ProceduralGeometry)target;

        if(GUILayout.Button("Init"))
        {
            instance.Init();
        }

        if(GUILayout.Button("Increment"))
        {
            instance.Increment();
        }
        if (GUILayout.Button("Decrement"))
        {
            instance.Decrement();
        }

        if (GUILayout.Button("Clear"))
        {
            instance.Clear();
        }
    }
}
