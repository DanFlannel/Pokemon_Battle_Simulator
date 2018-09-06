using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuaternionExtensions {

    //Get an average (mean) from more then two quaternions (with two, slerp would be used).
    //Note: this only works if all the quaternions are relatively close together.
    //Usage: 
    //-Cumulative is an external Vector4 which holds all the added x y z and w components.
    //-newRotation is the next rotation to be added to the average pool
    //-firstRotation is the first quaternion of the array to be averaged
    //-addAmount holds the total amount of quaternions which are currently added
    //This function returns the current average quaternion
    private static Quaternion AverageQuaternion(ref Vector4 cumulative, Quaternion newRotation, Quaternion firstRotation, int addAmount)
    {

        float w = 0.0f;
        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        //Before we add the new rotation to the average (mean), we have to check whether the quaternion has to be inverted. Because
        //q and -q are the same rotation, but cannot be averaged, we have to make sure they are all the same.
        if (!AreQuaternionsClose(newRotation, firstRotation))
        {
            newRotation = newRotation.inverseSign();
        }

        //Average the values
        float addDet = 1f / (float)addAmount;
        cumulative.w += newRotation.w;
        w = cumulative.w * addDet;
        cumulative.x += newRotation.x;
        x = cumulative.x * addDet;
        cumulative.y += newRotation.y;
        y = cumulative.y * addDet;
        cumulative.z += newRotation.z;
        z = cumulative.z * addDet;

        //note: if speed is an issue, you can skip the normalization step
        //return new Quaternion(x, y, z, w).normalize();
        return normalize(x, y, z, w);
    }

    public static Quaternion average(List<Quaternion> list)
    {
        Vector4 cumulative = Vector4.zero;
        Quaternion average = new Quaternion();
        for (int i = 0; i < list.Count; i++)
        {
            average = AverageQuaternion(ref cumulative, list[i], list[0], i);
        }
        return average;
    }

    public static Quaternion average(Quaternion[] arr)
    {
        Vector4 cumulative = Vector4.zero;
        Quaternion average = new Quaternion();
        for (int i = 0; i < arr.Length; i++)
        {
            average = AverageQuaternion(ref cumulative, arr[i], arr[0], i);
        }
        return average;
    }

    public static Quaternion normalize(this Quaternion q)
    {
        float lengthD = 1f / ((q.w * q.w) + (q.x * q.x) + (q.y * q.y) + (q.z * q.z));
        float w = q.w * lengthD;
        float x = q.x * lengthD;
        float y = q.w * lengthD;
        float z = q.w * lengthD;
        return new Quaternion(x, y, z, w);
    }

    public static Quaternion normalize(float x, float y, float z, float w)
    {

        float lengthD = 1.0f / (w * w + x * x + y * y + z * z);
        w *= lengthD;
        x *= lengthD;
        y *= lengthD;
        z *= lengthD;

        return new Quaternion(x, y, z, w);
    }

    //Changes the sign of the quaternion components. This is not the same as the inverse.
    public static Quaternion inverseSign(this Quaternion q)
    {
        return new Quaternion(-q.x, -q.y, -q.z, -q.w);
    }

    //Returns true if the two input quaternions are close to each other. This can
    //be used to check whether or not one of two quaternions which are supposed to
    //be very similar but has its component signs reversed (q has the same rotation as
    //-q)
    public static bool AreQuaternionsClose(Quaternion q1, Quaternion q2)
    {

        float dot = Quaternion.Dot(q1, q2);

        if (dot < 0.0f)
        {
            return false;
        }

        else
        {
            return true;
        }
    }

    public static Quaternion subtract(this Quaternion q1, Quaternion q2)
    {
        return q1 * Quaternion.Inverse(q2);
    }

    public static Quaternion add(this Quaternion q1, Quaternion q2)
    {
        return q1 * q2;
    }

    public static Quaternion inverse(this Quaternion q)
    {
        return Quaternion.Inverse(q);
    }

    public static bool isValid(Quaternion q)
    {
        return !float.IsNaN(q.x) && !float.IsNaN(q.y) && !float.IsNaN(q.z) && !float.IsNaN(q.w);
    }
}
