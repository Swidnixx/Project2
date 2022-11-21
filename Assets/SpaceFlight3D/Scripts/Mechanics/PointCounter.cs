using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    private int redPoints;
    private int greenPoints;
    private int goldenPoints;

    public void AddRed()
    {
        redPoints++;
    }

    public void AddGreen()
    {
        greenPoints++;
    }

    public void AddGold()
    {
        goldenPoints++;
    }
}
