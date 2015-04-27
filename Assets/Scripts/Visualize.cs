using System;
using ConvexHull;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Display the convex hull.
/// </summary>
public class Visualize : MonoBehaviour
{
    private List<Vector2> input;
    private List<Vector2> output;
    private GameObject points;

    /// <summary>
    /// Generate the convex hull.
    /// </summary>
    private void Update()
    {
        // Generate a new set of points when the space bar is pressed.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Destroy the previous game objects.
            if(points != null)
            {
                Destroy(points);
            }

            // Generate random points.
            input = VectorUtils.RandomVectorList(40, 20);

            // Create the game object representation of the points.
            points = new GameObject { name = "Points" };
            foreach (Vector2 v in input)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.parent = points.transform;
                sphere.transform.position = new Vector3(v.x, v.y, 0);
                sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            // Calculate the convex hull.
            IConvexHull convexHull = new GrahamScan();
            output = convexHull.Compute(input);

            stopwatch.Stop();
            UnityEngine.Debug.Log("Elapsed time: " + stopwatch.ElapsedMilliseconds);
        }
    }

    /// <summary>
    /// Use OpenGL to render the lines between point on the convex hull.
    /// </summary>
    private void OnPostRender()
    {
        // Sucky error checking :P
        if(output == null)
        {
            return;
        }

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
