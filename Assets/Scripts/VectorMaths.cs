using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix4by4
{
    public Matrix4by4(Vector4 column1, Vector4 column2, Vector4 column3, Vector4 column4)
    {
        values = new float[4, 4];

        // Column 1
        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = column1.w;

        // Column 2
        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = column2.w;

        // Column 3
        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = column3.w;

        // Column 4
        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = column4.w;
    }

    public Matrix4by4(Vector3 column1, Vector3 column2, Vector3 column3, Vector3 column4)
    {
        values = new float[4, 4];

        // Column 1
        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = 0;

        // Column 2
        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = 0;

        // Column 3
        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = 0;

        // Column 4
        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = 1;
    }

    public float[,] values;

    public static Vector4 operator *(Matrix4by4 lhs, Vector4 vec)
    {
        Vector4 MultipliedVec = new Vector4(0, 0, 0, 0);

        vec.w = 1;
        // Multiply rows of the 4x4 Matrix by the columns of the 4x1 Matrix (Vec4)
        // Rows = [0, 0], [0, 1], [0, 2] etc...

        MultipliedVec.x = (lhs.values[0, 0] * vec.x) + (lhs.values[0, 1] * vec.y) + (lhs.values[0, 2] * vec.z) + (lhs.values[0, 3] * vec.w);
        MultipliedVec.y = (lhs.values[1, 0] * vec.x) + (lhs.values[1, 1] * vec.y) + (lhs.values[1, 2] * vec.z) + (lhs.values[1, 3] * vec.w);
        MultipliedVec.z = (lhs.values[2, 0] * vec.x) + (lhs.values[2, 1] * vec.y) + (lhs.values[2, 2] * vec.z) + (lhs.values[2, 3] * vec.w);
        MultipliedVec.w = (lhs.values[3, 0] * vec.x) + (lhs.values[3, 1] * vec.y) + (lhs.values[3, 2] * vec.z) + (lhs.values[3, 3] * vec.w);

        return MultipliedVec;
    }

    public static Matrix4by4 operator *(Matrix4by4 lhs, Matrix4by4 rhs)
    {
        Matrix4by4 MultipliedMatrix = new Matrix4by4(Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero);

        MultipliedMatrix.values[0, 0] = (lhs.values[0, 0] * rhs.values[0, 0]) + (lhs.values[0, 1] * rhs.values[1, 0]) + (lhs.values[0, 2] * rhs.values[2, 0]) + (lhs.values[0, 3] * rhs.values[3, 0]);
        MultipliedMatrix.values[0, 1] = (lhs.values[0, 0] * rhs.values[0, 1]) + (lhs.values[0, 1] * rhs.values[1, 1]) + (lhs.values[0, 2] * rhs.values[2, 1]) + (lhs.values[0, 3] * rhs.values[3, 1]);
        MultipliedMatrix.values[0, 2] = (lhs.values[0, 0] * rhs.values[0, 2]) + (lhs.values[0, 1] * rhs.values[1, 2]) + (lhs.values[0, 2] * rhs.values[2, 2]) + (lhs.values[0, 3] * rhs.values[3, 2]);
        MultipliedMatrix.values[0, 3] = (lhs.values[0, 0] * rhs.values[0, 3]) + (lhs.values[0, 1] * rhs.values[1, 3]) + (lhs.values[0, 2] * rhs.values[2, 3]) + (lhs.values[0, 3] * rhs.values[3, 3]);

        MultipliedMatrix.values[1, 0] = (lhs.values[1, 0] * rhs.values[0, 0]) + (lhs.values[1, 1] * rhs.values[1, 0]) + (lhs.values[1, 2] * rhs.values[2, 0]) + (lhs.values[1, 3] * rhs.values[3, 0]);
        MultipliedMatrix.values[1, 1] = (lhs.values[1, 0] * rhs.values[0, 1]) + (lhs.values[1, 1] * rhs.values[1, 1]) + (lhs.values[1, 2] * rhs.values[2, 1]) + (lhs.values[1, 3] * rhs.values[3, 1]);
        MultipliedMatrix.values[1, 2] = (lhs.values[1, 0] * rhs.values[0, 2]) + (lhs.values[1, 1] * rhs.values[1, 2]) + (lhs.values[1, 2] * rhs.values[2, 2]) + (lhs.values[1, 3] * rhs.values[3, 2]);
        MultipliedMatrix.values[1, 3] = (lhs.values[1, 0] * rhs.values[0, 3]) + (lhs.values[1, 1] * rhs.values[1, 3]) + (lhs.values[1, 2] * rhs.values[2, 3]) + (lhs.values[1, 3] * rhs.values[3, 3]);

        MultipliedMatrix.values[2, 0] = (lhs.values[2, 0] * rhs.values[0, 0]) + (lhs.values[2, 1] * rhs.values[1, 0]) + (lhs.values[2, 2] * rhs.values[2, 0]) + (lhs.values[2, 3] * rhs.values[3, 0]);
        MultipliedMatrix.values[2, 1] = (lhs.values[2, 0] * rhs.values[0, 1]) + (lhs.values[2, 1] * rhs.values[1, 1]) + (lhs.values[2, 2] * rhs.values[2, 1]) + (lhs.values[2, 3] * rhs.values[3, 1]);
        MultipliedMatrix.values[2, 2] = (lhs.values[2, 0] * rhs.values[0, 2]) + (lhs.values[2, 1] * rhs.values[1, 2]) + (lhs.values[2, 2] * rhs.values[2, 2]) + (lhs.values[2, 3] * rhs.values[3, 2]);
        MultipliedMatrix.values[2, 3] = (lhs.values[2, 0] * rhs.values[0, 3]) + (lhs.values[2, 1] * rhs.values[1, 3]) + (lhs.values[2, 2] * rhs.values[2, 3]) + (lhs.values[2, 3] * rhs.values[3, 3]);

        MultipliedMatrix.values[3, 0] = (lhs.values[3, 0] * rhs.values[0, 0]) + (lhs.values[3, 1] * rhs.values[1, 0]) + (lhs.values[3, 2] * rhs.values[2, 0]) + (lhs.values[3, 3] * rhs.values[3, 0]);
        MultipliedMatrix.values[3, 1] = (lhs.values[3, 0] * rhs.values[0, 1]) + (lhs.values[3, 1] * rhs.values[1, 1]) + (lhs.values[3, 2] * rhs.values[2, 1]) + (lhs.values[3, 3] * rhs.values[3, 1]);
        MultipliedMatrix.values[3, 2] = (lhs.values[3, 0] * rhs.values[0, 2]) + (lhs.values[3, 1] * rhs.values[1, 2]) + (lhs.values[3, 2] * rhs.values[2, 2]) + (lhs.values[3, 3] * rhs.values[3, 2]);
        MultipliedMatrix.values[3, 3] = (lhs.values[3, 0] * rhs.values[0, 3]) + (lhs.values[3, 1] * rhs.values[1, 3]) + (lhs.values[3, 2] * rhs.values[2, 3]) + (lhs.values[3, 3] * rhs.values[3, 3]);

        return MultipliedMatrix;
    }

    public static Matrix4by4 Identity
    {
        get
        {
            return new Matrix4by4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1));
        }
    }

    public Vector4 GetRow(int rowNum)
    {
        Vector4 Row = new Vector4(values[rowNum, 0], values[rowNum, 1], values[rowNum, 2], values[rowNum, 3]);

        return Row;
    }

    public Matrix4by4 TranslationInverse()
    {
        Matrix4by4 InvertedMatrix = Identity;

        InvertedMatrix.values[0, 3] = -values[0, 3];
        InvertedMatrix.values[1, 3] = -values[1, 3];
        InvertedMatrix.values[2, 3] = -values[2, 3];

        return InvertedMatrix;
    }

    public Matrix4by4 RotationInverse()
    {
        Matrix4by4 InvertedMatrix = new Matrix4by4(GetRow(0), GetRow(1), GetRow(2), GetRow(3));

        return InvertedMatrix;
    }

    public Matrix4by4 ScaleInverse()
    {
        Matrix4by4 InvertedMatrix = Identity;

        InvertedMatrix.values[0, 0] = 1.0f / values[0, 0];
        InvertedMatrix.values[1, 1] = 1.0f / values[1, 1];
        InvertedMatrix.values[2, 2] = 1.0f / values[2, 2];

        return InvertedMatrix;
    }
}

public class Quat
{
    public float w, x, y, z;

    public Quat(float Angle, Vector3 Axis)
    {
        float halfAngle = Angle / 2;
        w = Mathf.Cos(halfAngle);
        x = Axis.x * Mathf.Sin(halfAngle);
        y = Axis.y * Mathf.Sin(halfAngle);
        z = Axis.z * Mathf.Sin(halfAngle);
    }

    public Quat(Vector3 VectorPos)
    {
        x = VectorPos.x;
        y = VectorPos.y;
        z = VectorPos.z;
    }

    public static Quat Identity()
    {
        Quat Identity = new Quat(Vector3.zero);
        Identity.w = 1.0f;

        return Identity;
    }

    public static Quat operator *(Quat lhs, Quat rhs)
    {
        Quat newQuat = Identity();

        newQuat.x = lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y + lhs.w * rhs.x;
        newQuat.y = -lhs.x * rhs.z + lhs.y * rhs.w + lhs.z * rhs.x + lhs.w * rhs.y;
        newQuat.z = lhs.x * rhs.y - lhs.y * rhs.x + lhs.z * rhs.w + lhs.w * rhs.z;
        newQuat.w = -lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z + lhs.w * rhs.w;

        return newQuat;
    }

    public Quat Inverse()
    {
        Quat newQuat = Identity();

        newQuat.w = w;

        newQuat.SetAxis(-GetAxis());

        return newQuat;
    }

    public Vector3 GetAxis()
    {
        Vector3 Axis = Vector3.zero;

        Axis.x = x;
        Axis.y = y;
        Axis.z = z;

        return Axis;
    }

    public void SetAxis(Vector3 Axis)
    {
        x = Axis.x;
        y = Axis.y;
        z = Axis.z;
    }

    public Vector4 GetAxisAngle()
    {
        Vector4 newVec = new Vector4();

        // Inverse cosine to get half angle back
        float halfAngle = Mathf.Acos(w);
        newVec.w = halfAngle * 2;           // Full angle

        // Simple calculations to get our axis angle back using half angle
        newVec.x = x / Mathf.Sin(halfAngle);
        newVec.y = y / Mathf.Sin(halfAngle);
        newVec.z = z / Mathf.Sin(halfAngle);

        return newVec;
    }

    public static Quat SLERP(Quat q, Quat r, float t)
    {
        t = Mathf.Clamp(t, 0.0f, 1.0f);

        Quat d = r * q.Inverse();
        Vector4 axisAngle = d.GetAxisAngle();
        Quat dT = new Quat(axisAngle.w * t, new Vector3(axisAngle.x, axisAngle.y, axisAngle.z));

        return dT * q;
    }
}

public class VectorMaths
{
    public static Vector2 AddVectors(Vector2 vec1, Vector2 vec2)
    {
        Vector2 newVector = Vector2.zero;

        newVector.x = vec1.x + vec2.x;
        newVector.y = vec1.y + vec2.y;

        return newVector;
    }

    public static Vector3 AddVectors(Vector3 vec1, Vector3 vec2)
    {
        Vector3 newVector = Vector3.zero;

        newVector.x = vec1.x + vec2.x;
        newVector.y = vec1.y + vec2.y;
        newVector.z = vec1.z + vec2.z;

        return newVector;
    }

    public static Vector2 SubtractVectors(Vector2 vec1, Vector2 vec2)
    {
        Vector2 newVector = Vector2.zero;

        newVector.x = vec1.x - vec2.x;
        newVector.y = vec1.y - vec2.y;

        return newVector;
    }

    public static Vector3 SubtractVectors(Vector3 vec1, Vector3 vec2)
    {
        Vector3 newVector = Vector3.zero;

        newVector.x = vec1.x - vec2.x;
        newVector.y = vec1.y - vec2.y;
        newVector.z = vec1.z - vec2.z;

        return newVector;
    }

    public static float VectorLength(Vector2 vec)
    {
        float len;

        len = Mathf.Sqrt((vec.x * vec.x) + (vec.y * vec.y));

        return len;
    }

    public static float VectorLength(Vector3 vec)
    {
        float len;

        len = Mathf.Sqrt((vec.x * vec.x) + (vec.y * vec.y) + (vec.z * vec.z));

        return len;
    }

    public static float VectorLengthSq(Vector2 vec)
    {
        float len;

        len = (vec.x * vec.x) + (vec.y * vec.y);

        return len;
    }

    public static float VectorLengthSq(Vector3 vec)
    {
        float Len;

        Len = (vec.x * vec.x) + (vec.y * vec.y) + (vec.z * vec.z);

        return Len;
    }

    public static Vector2 MultiplyVector(Vector2 vec, float scalar)
    {
        Vector2 newVector = Vector2.zero;

        newVector.x = vec.x * scalar;
        newVector.y = vec.y * scalar;

        return newVector;
    }

    public static Vector3 MultiplyVector(Vector3 vec, float scalar)
    {
        Vector3 newVector = Vector3.zero;

        newVector.x = vec.x * scalar;
        newVector.y = vec.y * scalar;
        newVector.z = vec.z * scalar;

        return newVector;
    }

    public static Vector2 DivideVector(Vector2 vec, float divisor)
    {
        Vector2 newVector = Vector2.zero;

        newVector.x = vec.x / divisor;
        newVector.y = vec.y / divisor;

        return newVector;
    }

    public static Vector3 DivideVector(Vector3 vec, float divisor)
    {
        Vector3 newVector = Vector3.zero;

        newVector.x = vec.x / divisor;
        newVector.y = vec.y / divisor;
        newVector.z = vec.z / divisor;

        return newVector;
    }

    public static Vector2 VectorNormalized(Vector2 vec)
    {
        Vector2 newVector = Vector2.zero;

        //Normalize inputted vector
        newVector = DivideVector(vec, VectorLength(vec));

        return newVector;
    }

    public static Vector3 VectorNormalized(Vector3 vec)
    {
        Vector3 newVector = Vector3.zero;

        //Normalize inputted vector
        newVector = DivideVector(vec, VectorLength(vec));

        return newVector;
    }

    public static float VectorDotProduct(Vector3 vec1, Vector3 vec2, bool shouldNormalize = true)
    {
        float newFloat = 0.0f;

        Vector3 A = new Vector3(vec1.x, vec1.y, vec1.z);
        Vector3 B = new Vector3(vec2.x, vec2.y, vec2.z);

        if (shouldNormalize)
        {
            A = VectorNormalized(A);
            B = VectorNormalized(B);
        }

        newFloat = (A.x * B.x) + (A.y * B.y) + (A.z * B.z);

        return newFloat;
    }

    public static Vector2 VectorLerp(Vector2 A, Vector2 B, float t)
    {
        Vector2 lerpedVector = Vector2.zero;

        lerpedVector = A * (1.0f - t) + B * t;

        return lerpedVector;
    }

    public static Vector3 VectorLerp(Vector3 A, Vector3 B, float t)
    {
        Vector3 lerpedVector = Vector3.zero;

        lerpedVector = A * (1.0f - t) + B * t;

        return lerpedVector;
    }

    public static float SqDistanceFromPointToVector(Vector3 A, Vector3 B, Vector3 C)
    {
        float squaredDistance = 0.0f;

        Vector3 AB = SubtractVectors(B, A);
        Vector3 AC = SubtractVectors(C, A);

        squaredDistance = VectorLengthSq(AC) - VectorDotProduct(AC, AB) * VectorDotProduct(AC, AB) / VectorLengthSq(AB);

        return squaredDistance;
    }

    public static float VectorToRadians(Vector2 Vec)
    {
        float angle = 0.0f;

        angle = Mathf.Atan2(Vec.y, Vec.x);

        return angle;
    }

    public static Vector2 RadiansToVector(float angle)
    {
        Vector2 Vec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        return Vec;
    }

    public static Vector3 EulerAnglesToDirection(Vector3 EulerAngles)
    {
        Vector3 Direction = new Vector3(0, 0, 0);

        // Converting Inputted Euler Angles to Radians from Degrees
        EulerAngles = EulerAngles * Mathf.Deg2Rad;

        // Need to adjust formula so it works correctly with Unity's coordinate system
        Direction.x = Mathf.Cos(-EulerAngles.x) * Mathf.Sin(EulerAngles.y);
        Direction.y = Mathf.Sin(-EulerAngles.x);
        Direction.z = Mathf.Cos(EulerAngles.y) * Mathf.Cos(-EulerAngles.x);
        // Switched x and z forumlas to work with the Unity coord system
        // Made EulerAngles.x negative value as Unity uses a left-handed coordinate system

        return Direction;
    }

    public static Vector3 VectorCrossProduct(Vector3 A, Vector3 B)
    {
        Vector3 CrossProduct = new Vector3(0, 0, 0);

        CrossProduct.x = (B.y * A.z) - (B.z * A.y);
        CrossProduct.y = (B.z * A.x) - (B.x * A.z);
        CrossProduct.z = (B.x * A.y) - (B.y * A.x);

        // A & B swapped to ensure it works with Unity's left-handed coord system

        return CrossProduct;
    }

    public static Vector3 RotateVertexAroundAxis(float Angle, Vector3 Axis, Vector3 Vertex)
    {
        // Rodrigues Rotation Formula
        // Make sure angle is in radians

        Angle = Angle * Mathf.Deg2Rad;

        Vector3 NewVector = (Vertex * Mathf.Cos(Angle)) +
            VectorDotProduct(Vertex, Axis) * Axis * (1 - Mathf.Cos(Angle)) +
            VectorCrossProduct(Axis, Vertex) * Mathf.Sin(Angle);

        return NewVector;
    }
}
