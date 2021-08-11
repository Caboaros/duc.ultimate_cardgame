using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoSides : MonoBehaviour
{
    // Singleton

    public static NeoSides Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public Side current;

    public int side1Zones;
    public int side2Zones;

    public void ChangeSide()
    {
        if (current == Side.Day)
        {
            current = Side.Night;
        }

        else
        {
            current = Side.Day;
        }
    }
}

public enum Side
{
    Day = 0,
    Night = 1,
}
