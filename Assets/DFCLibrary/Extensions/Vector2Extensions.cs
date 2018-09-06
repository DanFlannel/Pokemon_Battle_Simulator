using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extensions{

    //.. subtract

    public static Vector2 subtract(this Vector2 v1, Vector2 v2)
    {
        Vector2 result;
        result.x = v1.x - v2.x;
        result.y = v1.y - v2.y;
        return result;
    }

    public static Vector2 subtract(this Vector2 v1, float n)
    {
        Vector2 result;
        result.x = v1.x - n;
        result.y = v1.y - n;
        return result;
    }

    public static Vector2 subtract(this Vector2 v1, int i)
    {
        float f = (float)i;
        return subtract(v1, f);
    }

    //.. Add

    public static Vector2 add(this Vector2 v1, Vector2 v2)
    {
        Vector2 result;
        result.x = v1.x + v2.x;
        result.y = v1.y + v2.y;
        return result;
    }

    public static Vector2 add(this Vector2 v1, float n)
    {
        Vector2 result;
        result.x = v1.x + n;
        result.y = v1.y + n;
        return result;
    }

    public static Vector2 add(this Vector2 v1, int i)
    {
        float f = (float)i;
        return add(v1, f);
    }

    //... Multiply

    public static Vector2 multiply(this Vector2 v1, float n)
    {
        Vector2 result;
        result.x = v1.x * n;
        result.y = v1.y * n;
        return result;
    }

    public static Vector2 multiply(this Vector2 v1, int i)
    {
        float n = i;
        Vector2 result;
        result.x = v1.x * n;
        result.y = v1.y * n;
        return result;
    }

    public static Vector2 multiply(this Vector2 v1, Vector2 v2)
    {
        Vector2 result;
        result.x = v1.x * v2.x;
        result.y = v1.y * v2.y;
        return result;
    }

    //.. Divide

    public static Vector2 divide(this Vector2 v1, float n)
    {
        Vector2 result;
        result.x = v1.x / n;
        result.y = v1.y / n;
        return result;
    }

    public static Vector2 divide(this Vector2 v1, int i)
    {
        float n = i;
        Vector2 result;
        result.x = v1.x / n;
        result.y = v1.y / n;
        return result;
    }

    public static Vector2 divide(this Vector2 v1, Vector2 v2)
    {
        Vector2 result;
        result.x = v1.x / v2.x;
        result.y = v1.y / v2.y;
        return result;
    }

    //.. Averaging

    public static Vector2 average(this Vector2[] positions)
    {
        if (positions.Length == 0)
            return Vector2.zero;

        Vector2 final = Vector2.zero;
        foreach (Vector2 pos in positions)
        {
            final.x += pos.x;
            final.y += pos.y;
        }

        return divide(final, positions.Length);
    }

    public static Vector2 average(this List<Vector2> positions)
    {
        Vector2[] arr = positions.ToArray();
        return average(arr);
    }

    //.. Others

    public static Vector2 rounded(this Vector2 v1, int places)
    {
        Vector2 final;
        final.x = (float)decimal.Round((decimal)v1.x, places);
        final.y = (float)decimal.Round((decimal)v1.y, places);
        return final;
    }

    public static Vector2 inverse(this Vector2 v1)
    {
        Vector2 final;
        final.x = -v1.x;
        final.y = -v1.y;
        return final;
    }

    //.. Validation

    public static bool isValid(Vector2 v1)
    {
        return !float.IsNaN(v1.x) && !float.IsNaN(v1.y);
    }
}
