namespace ConvexHull
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Implementation of the Graham Scan convex hull algorithm.
    /// </summary>
    public class GrahamScan : IConvexHull
    {
        /// <summary>
        /// Class to keep track of vector attributes used in the calculation
        /// of the convex hull.
        /// </summary>
        private class VectorAttributes
        {
            public float DistanceSquared { get; set; }
            public double Angle { get; set; }
            public Vector2 Vector { get; set; }
        }

        private Vector2 lowest;

        private void SwapLowestY(List<Vector2> input)
        {
            // Find lowest y valued vector.
            int lowestIndex = 0;
            lowest = input[lowestIndex];

            for (int i = 0; i < input.Count; ++i)
            {
                if (input[i].y < lowest.y)
                {
                    lowest = input[i];
                    lowestIndex = i;
                }
                else if (input[i].y == lowest.y)
                {
                    if (input[i].x < lowest.x)
                    {
                        lowest = input[i];
                        lowestIndex = i;
                    }
                }
            }

            // Swap lowest y valued vector.
            Vector2 tmp = input[lowestIndex];
            input[lowestIndex] = input[0];
            input[0] = tmp;
        }

        /// <summary>
        /// Compute the convex hull given a set of points on a 2D plane.
        /// </summary>
        /// <param name="input">A set of points on a 2D plane.</param>
        /// <returns>The convex hull.</returns>
        public List<Vector2> Compute(List<Vector2> input)
        {
            // Three or less points is already the convex hull.
            if (input.Count <= 3)
            {
                return new List<Vector2>(input);
            }

            // Swap the lowest y to input[0].
            SwapLowestY(input);

            // Calculate angle and distance for each vector based on the lowest point.
            List<VectorAttributes> vectorAttributeList = new List<VectorAttributes>();
            foreach (Vector2 v in input)
            {
                VectorAttributes vectorAttr = new VectorAttributes
                {
                    Vector = v,
                    // TODO: I'm sure this angle calculation can be replaced with orientation.
                    Angle = VectorUtils.FindAngle(lowest, v),
                    DistanceSquared = VectorUtils.DistanceSquared(lowest, v)
                };
                vectorAttributeList.Add(vectorAttr);
            }

            // Sort remaining vectors based on angle with lowest point.
            vectorAttributeList = vectorAttributeList.OrderBy(v => v.Angle).ToList();

            // Remove all points with the same angle except for the furthest.
            // TODO: Can this be improved? Maybe a hashset based on angle that
            // only inserts if the distance is greater than the item currently contained.
            
            // Skip the first point as we know this is definitely in the convex hull
            // and every other point with the same angle will definitely be further from it.
            int index = 1;
            while (index < vectorAttributeList.Count - 1)
            {
                while ((index < vectorAttributeList.Count - 1) && (vectorAttributeList[index].Angle == vectorAttributeList[index + 1].Angle))
                {
                    if (vectorAttributeList[index].DistanceSquared > vectorAttributeList[index + 1].DistanceSquared)
                    {
                        vectorAttributeList.RemoveAt(index + 1);
                    }
                    else if (vectorAttributeList[index].DistanceSquared < vectorAttributeList[index + 1].DistanceSquared)
                    {
                        vectorAttributeList.RemoveAt(index);
                    }
                }
                ++index;
            }

            // The guts of the Graham Scan algorithm.
            List<Vector2> output = new List<Vector2>();
            output.Add(vectorAttributeList[0].Vector);
            output.Add(vectorAttributeList[1].Vector);
            output.Add(vectorAttributeList[2].Vector);

            for (int i = 3; i < vectorAttributeList.Count; ++i)
            {
                while (VectorUtils.Orientation(output[output.Count - 2], output[output.Count - 1], vectorAttributeList[i].Vector) < 0)
                {
                    // Remove vectors that make a clockwise turn.
                    output.RemoveAt(output.Count - 1);
                }
                output.Add(vectorAttributeList[i].Vector);
            }

            return output;
        }
    }
}