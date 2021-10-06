using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorPicker
{
    public static float GetDistance(Color current, Color match)
    {
        float redDifference;
        float greenDifference;
        float blueDifference;

        redDifference = current.r - match.r;
        greenDifference = current.g - match.g;
        blueDifference = current.b - match.b;

        return redDifference * redDifference + greenDifference * greenDifference + blueDifference * blueDifference;
    }

    public static Color FindNearestColor(Color[] map, Color current)
    {
        float shortestDistance;
        Color clr;

        clr = Color.black;
        shortestDistance = int.MaxValue;

        for (int i = 0; i < map.Length; i++)
        {
            Color match;
            float distance;

            match = map[i];
            distance = GetDistance(current, match);
            //Debug.Log(distance);

            if (distance < shortestDistance)
            {
                clr = match;
                shortestDistance = distance;
            }
        }

        return clr;
    }
}
