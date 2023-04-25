using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SegmentDataRefactored", menuName = "LevelGeneration/Maps/SegmentDataRefactored")]
public class SegmentData : ScriptableObject
{
    public GameObject prefab;
    public SegmentDirection direction;
    public Vector3 entrance;
    public Vector3 exit;


    public float GetRotation()
    {
        switch (direction)
        {
            default:
            case SegmentDirection.Forward:
                return 0;

            case SegmentDirection.Left:
                return -90;

            case SegmentDirection.Right:
                return 90;

            case SegmentDirection.Back:
                return 180;
        }
    }
}

//Its only for z rotation for now
public enum SegmentDirection
{
    Forward, Left, Right, Back
}