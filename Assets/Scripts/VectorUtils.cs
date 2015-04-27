using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Vector utility methods.
/// </summary>
public class VectorUtils
{
    /// <summary>
    /// Calculates the orientation of a vector given two other vectors.
    /// </summary>
    /// <remarks>
    ///     > 0 counter-clockwise
    ///     = 0 colinear
    ///     < 0 clockwise 
    /// </remarks>
    /// <param name="v1">The first vector.</param>
    /// <param name="v2">The second vector.</param>
    /// <param name="v3">The vector to calculate the orientation for.</param>
    /// <returns>The orientation of the given vector.</returns>
    public static float Orientation(Vector2 v1, Vector2 v2, Vector2 v3)
    {
        return (v2.x - v1.x) * (v3.y - v1.y) - (v2.y - v1.y) * (v3.x - v1.x);
    }

    /// <summary>
    /// Calculate the angle between two vectors.
    /// TODO: This could probably be removed and the Orientation method utilized instead.
    /// </summary>
    /// <param name="v1">The first vector.</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>The angle between two vectors.</returns>
    public static double FindAngle(Vector2 v1, Vector2 v2)
    {
        double dx = v2.x - v1.x;
        double dy = v2.y - v1.y;
        return Math.Atan2(dy, dx) * (180 / Math.PI);
    }

    /// <summary>
    /// Calculate the distance squared between two vectors.
    /// </summary>
    /// <param name="v1">The first vector.</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>The distance squared between two vectors.</returns>
    public static float DistanceSquared(Vector2 v1, Vector2 v2)
    {
        float x = Math.Abs(v1.x - v2.x);
        float y = Math.Abs(v1.y - v2.y);

        return (x * x) + (y * y);
    }

    /// <summary>
    /// Generates a vector with random coordinates.
    /// </summary>
    /// <returns>A vector with random coordinates.</returns>
    public static Vector2 RandomVector()
    {
        System.Random rand = new System.Random();
        return new Vector2(rand.Next(20), rand.Next(20));
    }

    /// <summary>
    /// Generates a list of unique vectors with random coordinates.
    /// </summary>
    /// <param name="count">The number of vectors to generate.</param>
    /// <returns>A list of unique vectors with random coordinates.</returns>
    public static List<Vector2> RandomVectorList(int count, int max)
    {
        System.Random rand = new System.Random();
        HashSet<Vector2> set = new HashSet<Vector2>();
        List<Vector2> list = new List<Vector2>();

        while (set.Count < count)
        {
            Vector2 v = new Vector2(rand.Next(max), rand.Next(max));
            if(set.Add(v))
            {
                list.Add(v);
            }
        }

        return list;
    }
}