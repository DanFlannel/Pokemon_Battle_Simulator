using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions
{
    //.. vertex crap that'll be useful later

    public static Vector3 GetVertexWorldPosition(Vector3 vertex, Transform owner)
    {
        return owner.localToWorldMatrix.MultiplyPoint3x4(vertex);
    }

    //.. Ray stuff

    public enum RayDirection
    {
        right,
        left,
        up,
        down,
        forward,
        back
    };

    public static Vector3 GetDirection(RayDirection dir, GameObject target)
    {
        Vector3 localDir = Vector3.zero;

        switch (dir)
        {
            case RayDirection.back:
                localDir = -target.transform.forward;
                break;

            case RayDirection.down:
                localDir = -target.transform.up;
                break;

            case RayDirection.forward:
                localDir = target.transform.forward;
                break;

            case RayDirection.left:
                localDir = -target.transform.right;
                break;

            case RayDirection.right:
                localDir = target.transform.right;
                break;

            case RayDirection.up:
                localDir = target.transform.up;
                break;
        }
        return localDir;
    }
    
    //.. distance

    public static float distance(Vector3 v1, Vector3 v2)
    {
        float x = v1.x - v2.x;
        float y = v1.y - v2.y;
        float z = v1.z - v2.z;
        float dist = x * x + y * y + z * z;
        return dist;
    }

    public static bool distance(Vector3 v1, Vector2 v2, float dist, bool isGreater)
    {
        float act = distance(v1, v2);
        dist = dist * dist;
        if(isGreater)
        {
            return act > dist;
        }
        else
        {
            return act < dist;
        }
    }
    
    public static bool distanceEquals(Vector3 v1, Vector3 v2, float dist)
    {
        float act = distance(v1, v2);
        return act == (dist * dist);
    }

    //.. subtract

    public static Vector3 subtract(this Vector3 v1, Vector3 v2)
    {
        Vector3 result;
        result.x = v1.x - v2.x;
        result.y = v1.y - v2.y;
        result.z = v1.z - v2.z;
        return result;
    }

    public static Vector3 subtract(this Vector3 v1, float n)
    {
        Vector3 result;
        result.x = v1.x - n;
        result.y = v1.y - n;
        result.z = v1.z - n;
        return result;
    }

    public static Vector3 subtract(this Vector3 v1, int i)
    {
        float f = (float)i;
        return subtract(v1, f);
    }

    //.. Add

    public static Vector3 add(this Vector3 v1, Vector3 v2)
    {
        Vector3 result;
        result.x = v1.x + v2.x;
        result.y = v1.y + v2.y;
        result.z = v1.z + v2.z;
        return result;
    }

    public static Vector3 add(this Vector3 v1, float n)
    {
        Vector3 result;
        result.x = v1.x + n;
        result.y = v1.y + n;
        result.z = v1.z + n;
        return result;
    }

    public static Vector3 add(this Vector3 v1, int i)
    {
        float f = (float)i;
        return add(v1, f);
    }

    //... Multiply

    public static Vector3 multiply(this Vector3 v1, float n)
    {
        Vector3 result;
        result.x = v1.x * n;
        result.y = v1.y * n;
        result.z = v1.z * n;
        return result;
    }

    public static Vector3 multiply(this Vector3 v1, int i)
    {
        float n = i;
        Vector3 result;
        result.x = v1.x * n;
        result.y = v1.y * n;
        result.z = v1.z * n;
        return result;
    }

    public static Vector3 multiply(this Vector3 v1, Vector3 v2)
    {
        Vector3 result;
        result.x = v1.x * v2.x;
        result.y = v1.y * v2.y;
        result.z = v1.z * v2.z;
        return result;
    }

    //.. Divide

    public static Vector3 divide(this Vector3 v1, float n)
    {
        Vector3 result;
        result.x = v1.x / n;
        result.y = v1.y / n;
        result.z = v1.z / n;
        return result;
    }

    public static Vector3 divide(this Vector3 v1, int i)
    {
        float n = i;
        Vector3 result;
        result.x = v1.x / n;
        result.y = v1.y / n;
        result.z = v1.z / n;
        return result;
    }

    public static Vector3 divide(this Vector3 v1, Vector3 v2)
    {
        Vector3 result;
        result.x = v1.x / v2.x;
        result.y = v1.y / v2.y;
        result.z = v1.z / v2.z;
        return result;
    }

    //.. Averaging

    public static Vector3 average(this Vector3[] positions)
    {
        if (positions.Length == 0)
            return Vector3.zero;

        Vector3 final = Vector3.zero;
        foreach (Vector3 pos in positions)
        {
            final.x += pos.x;
            final.y += pos.y;
            final.z += pos.z;
        }

        return divide(final, positions.Length);
    }

    public static Vector3 average(this List<Vector3> positions)
    {
        Vector3[] arr = positions.ToArray();
        return average(arr);
    }

    //.. Others

    public static Vector3 rounded(this Vector3 v1, int places)
    {
        Vector3 final;
        final.x = (float)decimal.Round((decimal)v1.x, places);
        final.y = (float)decimal.Round((decimal)v1.y, places);
        final.z = (float)decimal.Round((decimal)v1.z, places);
        return final;
    }

    public static Vector3 inverse(this Vector3 v1)
    {
        Vector3 final;
        final.x = -v1.x;
        final.y = -v1.y;
        final.z = -v1.z;
        return final;
    }

    //.. Validation

    public static bool isValid(Vector3 v1)
    {
        return !float.IsNaN(v1.x) && !float.IsNaN(v1.y) && !float.IsNaN(v1.z);
    }
}