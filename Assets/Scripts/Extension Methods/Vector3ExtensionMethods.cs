using System;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3ExtensionMethods
{
    /// <summary>
    /// Finds the position closest to the given one.
    /// </summary>
    /// <param name="position">World position.</param>
    /// <param name="otherPositions">Other world positions.</param>
    /// <returns>Closest position.</returns>
    public static Vector3 GetClosest(this Vector3 position, List<Vector3> otherPositions)
    {
        var closest = Vector3.zero;
        var shortestDistance = Mathf.Infinity;

        foreach (var otherPosition in otherPositions)
        {
            var distance = (position - otherPosition).sqrMagnitude;

            if (distance < shortestDistance)
            {
                closest = otherPosition;
                shortestDistance = distance;
            }
        }

        return closest;
    }

    /// <summary>
    /// Converts a Vector3Int struct to a Vector3.
    /// </summary>
    /// <param name="vector">Vector.</param>
    /// <returns>Vector3 struct.</returns>
    public static Vector3 ToVector3(this Vector3Int vector)
    {
        return new Vector3(
            Convert.ToSingle(vector.x),
            Convert.ToSingle(vector.y),
            Convert.ToSingle(vector.z)
        );
    }
}
