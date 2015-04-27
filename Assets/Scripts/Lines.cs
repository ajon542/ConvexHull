using UnityEngine;
using System;
using System.Collections;

public class Line
{
    public static readonly double Epsilon = 0.001;

    // We use the more general formula ax + by + c = 0
    // as the foundation of our line type because it
    // covers all possible lines in the plane.

    public double a { get; set; }
    public double b { get; set; }
    public double c { get; set; }

    /// <summary>
    /// Converts a set of vectors to the line representation.
    /// </summary>
    /// <param name="v1">A point on the line.</param>
    /// <param name="v2">A point on the line.</param>
    /// <param name="l">The resulting line.</param>
    public static void PointsToLine(Vector2 v1, Vector2 v2, Line l)
    {
        if (v1.x == v2.x)
        {
            l.a = 1;
            l.b = 0;
            l.c = -v1.x;
        }
        else
        {
            l.b = 1;
            l.a = -(v1.y - v2.y) / (v1.x - v2.x);
            l.c = -(l.a * v1.x) - (l.b * v1.y);
        }
    }

    /// <summary>
    /// Convert a vector and slope to a line representation.
    /// </summary>
    /// <param name="v">A point on the line.</param>
    /// <param name="slope">The slope of the line.</param>
    /// <param name="l">The resulting line.</param>
    public static void PointAndSlopeToLine(Vector2 v, double slope, Line l)
    {
        l.a = -slope;
        l.b = 1;
        l.c = -((l.a * v.x) + (l.b * v.y));
    }

    /// <summary>
    /// Determines whether two lines are parallel.
    /// </summary>
    /// <param name="l1">The first line.</param>
    /// <param name="l2">The second line.</param>
    /// <returns><c>true</c> if the lines are parallel; <c>false</c> otherwise.</returns>
    public static bool Parallel(Line l1, Line l2)
    {
        return ((Math.Abs(l1.a - l2.a) <= Epsilon) && (Math.Abs(l1.b - l2.b) <= Epsilon));
    }

    /// <summary>
    /// Determines whether two lines are the same.
    /// </summary>
    /// <param name="l1">The first line.</param>
    /// <param name="l2">The second line.</param>
    /// <returns><c>true</c> if the lines are the same; <c>false</c> otherwise.</returns>
    public static bool SameLine(Line l1, Line l2)
    {
        return (Parallel(l1, l2) && (Math.Abs(l1.c - l2.c) <= Epsilon));
    }

    /// <summary>
    /// Find the intersecting point of two lines.
    /// </summary>
    /// <param name="l1">The first line.</param>
    /// <param name="l2">The second line.</param>
    /// <returns>The point of intersection.</returns>
    public static Vector2 IntersectionPoint(Line l1, Line l2)
    {
        if (SameLine(l1, l2))
        {
            throw new Exception("Identical lines, all points intersect.");
        }

        if (Parallel(l1, l2))
        {
            throw new Exception("Distinct parallel lines do not intersect.");
        }

        double x = (l2.b * l1.c - l1.b * l2.c) / (l2.a * l1.b - l1.a * l2.b);

        // Test for vertical line.
        double y;
        if (Math.Abs(l1.b) > Epsilon)
        {
            y = -(l1.a * x + l1.c) / l1.b;
        }
        else
        {
            y = -(l2.a * x + l2.c) / l2.b;
        }

        return new Vector2((float)x, (float)y);
    }
}
