using System;
using ConvexHull;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Display the convex hull.
/// </summary>
public class Visualize : MonoBehaviour
{
    private List<Vector2> input;
    private List<Vector2> output;

    /// <summary>
    /// Generate the convex hull.
    /// </summary>
    void Start()
    {
        // Generate random points.
        input = VectorUtils.RandomVectorList(50);

        // Create the game object representation of the points.
        GameObject points = new GameObject { name = "Points" };
        foreach (Vector2 v in input)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.parent = points.transform;
            sphere.transform.position = new Vector3(v.x, v.y, 0);
            sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        // Calculate the convex hull.
        IConvexHull convexHull = new GrahamScan();
        output = convexHull.Compute(input);
    }

    /// <summary>
    /// Use OpenGL to render the lines between point on the convex hull.
    /// </summary>
    private void OnPostRender()
    {
        GL.Begin(GL.LINES);
        GL.Color(new Color(0.0f, 1.0f, 0.0f, 1.0f));

        for (int i = 0; i < output.Count - 1; ++i)
        {
            GL.Vertex3(output[i].x, output[i].y, 0f);
            GL.Vertex3(output[i + 1].x, output[i + 1].y, 0f);
        }

        GL.Vertex3(output[output.Count - 1].x, output[output.Count - 1].y, 0f);
        GL.Vertex3(output[0].x, output[0].y, 0f);

        GL.End();
    }
}
