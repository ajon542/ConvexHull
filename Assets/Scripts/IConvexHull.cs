namespace ConvexHull
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Interface for computing the convex hull.
    /// </summary>
    public interface IConvexHull
    {
        /// <summary>
        /// Compute the convex hull given a set of points on a 2D plane.
        /// </summary>
        /// <param name="input">A set of points on a 2D plane.</param>
        /// <returns>The convex hull.</returns>
        List<Vector2> Compute(List<Vector2> input);
    }
}
