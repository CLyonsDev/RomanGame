using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {

	public static Vector3 MultiplyV3(Vector3 a, Vector3 b)
    {
        return (new Vector3(a.x * b.x, a.y * b.y, a.z * b.z));
    }

    public static Vector3 EulerAnglesBetween(Quaternion from, Quaternion to)
    {
        Vector3 delta = to.eulerAngles - from.eulerAngles;

        if (delta.x > 180)
            delta.x -= 360;
        else if (delta.x < -180)
            delta.x += 360;

        if (delta.y > 180)
            delta.y -= 360;
        else if (delta.y < -180)
            delta.y += 360;

        if (delta.z > 180)
            delta.z -= 360;
        else if (delta.z < -180)
            delta.z += 360;

        return delta;
    }

    public static Vector3 V3Float(float f)
    {
        return new Vector3(f, f, f);
    }

    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        var result = aParent.Find(aName);

        if (result != null)
            return result;

        foreach(Transform child in aParent)
        {
            result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }
}
