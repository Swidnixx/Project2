
using UnityEngine;

[CreateAssetMenu(fileName ="SegmentDataLibraryRefactored", menuName = "LevelGeneration/Maps/SegmentDataLibraryRefactored")]
public class SegmentLibrary : ScriptableObject
{
    public SegmentData[] segments;

    public int Count => segments.Length;

    internal SegmentData GetSegment(int index)
    {
        return segments[Random.Range(0, segments.Length)];
    }
}