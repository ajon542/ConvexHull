using UnityEngine;
using System.Collections;

public class Triangles
{
    public static double SignedTriangleArea(Vector2 a, Vector2 b, Vector2 c)
    {
        return ((a.x * b.y - a.y * b.x + a.y * c.x - a.x * c.y + b.x * c.y - c.x * b.y) / 2.0);
    }
}
