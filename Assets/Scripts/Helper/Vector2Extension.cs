using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class Vector2Extension
{

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public enum V2AngleMode { Angle, AngleAlt, ThirdType, CCW_AngleAlt }
    public static float ToAng(this Vector2 v, V2AngleMode angMode = V2AngleMode.Angle)
    {
        switch (angMode)
        {
            case V2AngleMode.Angle:
                return MathHelper.Angle(v);
            case V2AngleMode.AngleAlt:
                return MathHelper.AngleAlt(v);
            case V2AngleMode.CCW_AngleAlt:
                return MathHelper.UnAngleAlt(v);
            case V2AngleMode.ThirdType:
                var vector2 = v - new Vector2();
                var vector1 = new Vector2(1, 0); // 12 o'clock == 0°, assuming that y goes from bottom to top
                return Mathf.Rad2Deg * (Mathf.Atan2(v.y, v.x) - Mathf.Atan2(vector1.y, vector1.x));
            default:
                Debug.Log("Unhandled switch, returning normal angle mode");
                goto case V2AngleMode.Angle;
        }
    }

    public static float Angle(this Vector2 v)
    {
        return Vector2.Angle(Vector2.right, v);
    }

    public static float SignedAngle(Vector2 v1, Vector2 v2)
    {
        return Vector2.Angle(v1, v2) * Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
    }
}
