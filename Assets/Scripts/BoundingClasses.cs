using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingCircle
{
    public Vector3 centrePoint;
    public float radius;

    public BoundingCircle(Vector3 centrePoint, float radius)
    {
        this.centrePoint = centrePoint;
        this.radius = radius;
    }

    public bool Intersects(BoundingCircle otherCircle)
    {
        // Vector representing the direction and length to the other circle
        Vector3 VectorToOther = otherCircle.centrePoint - centrePoint;

        // Combined radii squared
        float CombinedRadiusSq = (otherCircle.radius + radius);
        CombinedRadiusSq *= CombinedRadiusSq;

        // Return boolean statement, if true they intersect
        return VectorMaths.VectorLengthSq(VectorToOther) <= CombinedRadiusSq;
    }

    public bool IntersectWithAABB(BoundingCircle circle, AABB box)
    {
        float circleLeft = circle.centrePoint.x - circle.radius;
        float circleRight = circle.centrePoint.x + circle.radius;
        float circleTop = circle.centrePoint.y + circle.radius;
        float circleBottom = circle.centrePoint.y - circle.radius;


        return !(circleLeft > box.right
            || circleRight < box.left
            || circleTop < box.bottom
            || circleBottom > box.top);
    }

    public static bool LineIntersection(BoundingCircle circle, Vector3 startPoint, Vector3 endPoint, out Vector3 intersectionPoint)
    {
        // Define initial lowest and highest
        float lowest = 0.0f;
        float highest = 1.0f;

        // Default intersection point
        intersectionPoint = Vector3.zero;

        // Intersection test on each axis
        if (!IntersectingAxis(Vector3.right, circle, startPoint, endPoint, ref lowest, ref highest))
            return false;

        if (!IntersectingAxis(Vector3.up, circle, startPoint, endPoint, ref lowest, ref highest))
            return false;

        // Calculate intersection point through interpolation
        intersectionPoint = VectorMaths.VectorLerp(startPoint, endPoint, lowest);

        return true;
    }

    public static bool IntersectingAxis(Vector3 axis, BoundingCircle circle, Vector3 startPoint, Vector3 endPoint, ref float lowest, ref float highest)
    {
        // Calculate min and max based on current axis
        float minimum = 0.0f;
        float maximum = 1.0f;

        Vector3 centrePoint = circle.centrePoint;
        float radius = circle.radius;

        Vector3 circleLeft = new Vector3(centrePoint.x - radius, centrePoint.y, centrePoint.z);
        Vector3 circleRight = new Vector3(centrePoint.x + radius, centrePoint.y, centrePoint.z);
        Vector3 circleBottom = new Vector3(centrePoint.x, centrePoint.y - radius, centrePoint.z);
        Vector3 circleTop = new Vector3(centrePoint.x, centrePoint.y + radius, centrePoint.z);


        if (axis == Vector3.right)
        {
            minimum = (circleLeft.x - startPoint.x) / (endPoint.x - startPoint.x);
            maximum = (circleRight.x - startPoint.x) / (endPoint.x - startPoint.x);
        }
        else if (axis == Vector3.up)
        {
            minimum = (circleBottom.y - startPoint.y) / (endPoint.y - startPoint.y);
            maximum = (circleTop.y - startPoint.y) / (endPoint.y - startPoint.y);
        }
        //else if (axis == Vector3.forward)
        //{
        //    minimum = (circle.back - startPoint.z) / (endPoint.z - startPoint.z);
        //    maximum = (circle.front - startPoint.z) / (endPoint.z - startPoint.z);
        //}


        if (maximum < minimum)
        {
            // Swapping values
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
        }

        // Eliminate non-intersections early
        if (maximum < lowest)
            return false;

        if (minimum > highest)
            return false;

        lowest = Mathf.Max(minimum, lowest);
        highest = Mathf.Min(maximum, highest);

        if (lowest > highest)
            return false;

        return true;
    }
}

public class BoundingCapsule
{
    public Vector3 A; // Start Point
    public Vector3 B; // End Point
    public float radius;

    public BoundingCapsule(Vector3 A, Vector3 B, float radius)
    {
        this.A = A;
        this.B = B;
        this.radius = radius;
    }

    public bool Intersects(BoundingCircle otherCircle)
    {
        // Square the sum of both radii
        float CombinedRadiusSq = (radius + otherCircle.radius) * (radius + otherCircle.radius);

        // Check if square distance is less than square radius, return result
        // True means both objects are intersecting
        return VectorMaths.SqDistanceFromPointToVector(A, B, otherCircle.centrePoint) <= CombinedRadiusSq;
    }
}

public class AABB
{
    Vector3 minExtent;
    Vector3 maxExtent;

    public AABB(Vector3 min, Vector3 max)
    {
        minExtent = min;
        maxExtent = max;
    }

    public float top
    {
        get { return maxExtent.y; }
    }

    public float bottom
    {
        get { return minExtent.y; }
    }

    public float left
    {
        get { return minExtent.x; }
    }

    public float right
    {
        get { return maxExtent.x; }
    }

    public float front
    {
        get { return maxExtent.z; }
    }

    public float back
    {
        get { return minExtent.z; }
    }

    public bool Intersects(AABB box1, AABB box2)
    {
        return !(box2.left > box1.right
            || box2.right < box1.left
            || box2.top < box1.bottom
            || box2.bottom > box1.top
            || box2.back > box1.front
            || box2.front < box1.back);
    }

    public bool IntersectWithBoundingCircle(AABB box, BoundingCircle circle)
    {
        float circleLeft = circle.centrePoint.x - circle.radius;
        float circleRight = circle.centrePoint.x + circle.radius;
        float circleTop = circle.centrePoint.y + circle.radius;
        float circleBottom = circle.centrePoint.y - circle.radius;


        return !(circleLeft > box.right
            || circleRight < box.left
            || circleTop < box.bottom
            || circleBottom > box.top);
    }

    public static bool LineIntersection(AABB box, Vector3 startPoint, Vector3 endPoint, out Vector3 intersectionPoint)
    {
        // Define initial lowest and highest
        float lowest = 0.0f;
        float highest = 1.0f;

        // Default intersection point
        intersectionPoint = Vector3.zero;

        // Intersection test on each axis
        if (!IntersectingAxis(Vector3.right, box, startPoint, endPoint, ref lowest, ref highest))
            return false;

        if (!IntersectingAxis(Vector3.up, box, startPoint, endPoint, ref lowest, ref highest))
            return false;

        // Calculate intersection point through interpolation
        intersectionPoint = VectorMaths.VectorLerp(startPoint, endPoint, lowest);

        return true;
    }

    public static bool IntersectingAxis(Vector3 axis, AABB box, Vector3 startPoint, Vector3 endPoint, ref float lowest, ref float highest)
    {
        // Calculate min and max based on current axis
        float minimum = 0.0f;
        float maximum = 1.0f;

        if (axis == Vector3.right)
        {
            minimum = (box.left - startPoint.x) / (endPoint.x - startPoint.x);
            maximum = (box.right - startPoint.x) / (endPoint.x - startPoint.x);
        }
        else if (axis == Vector3.up)
        {
            minimum = (box.bottom - startPoint.y) / (endPoint.y - startPoint.y);
            maximum = (box.top - startPoint.y) / (endPoint.y - startPoint.y);
        }
        else if (axis == Vector3.forward)
        {
            minimum = (box.back - startPoint.z) / (endPoint.z - startPoint.z);
            maximum = (box.front - startPoint.z) / (endPoint.z - startPoint.z);
        }


        if (maximum < minimum)
        {
            // Swapping values
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
        }

        // Eliminate non-intersections early
        if (maximum < lowest)
            return false;

        if (minimum > highest)
            return false;

        lowest = Mathf.Max(minimum, lowest);
        highest = Mathf.Min(maximum, highest);

        if (lowest > highest)
            return false;

        return true;
    }
}

