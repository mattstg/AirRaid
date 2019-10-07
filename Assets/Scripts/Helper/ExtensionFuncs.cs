using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class ExtensionFuncs
{

    static System.Random rnd = new System.Random();

    public static Vector2 V2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static void DeleteAllChildren(this Transform t)
    {
        Transform[] childarr = t.ChildrenArray();
        foreach (Transform child in childarr)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static T ToEnum<T>(this string s)
        where T : struct, IConvertible, IComparable, IFormattable
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("String conversion to enum failed, T was not enum type, must be an enum.");
        }
        T toRet;
        if (Enum.TryParse<T>(s, out toRet))
        {
            return toRet;
        }
        else
        {
            System.Type st = typeof(T);
            Debug.LogError($"ERROR, Conversion from string to enum for string:{s} and enumType:{st} failed!! Returning default value for enum");
            return default(T);
        }

    }


    public static Transform[] ChildrenArray(this Transform t)
    {
        return t.Cast<Transform>().ToArray();
    }

    public static Transform ChildByName(this Transform t, string _name)
    {
        return t.ChildrenArray().FirstOrDefault(tchild => tchild.name == _name);
    }

    public static float SurfaceArea(this Vector2 v)
    {
        return v.x * v.y;
    }

    public static Vector3 V3(this Vector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }

    public static Vector3 SetSign(this Vector3 v, bool setX, bool setY)
    {
        return new Vector3(Mathf.Abs(v.x) * MathHelper.BooleanSign(setX), Mathf.Abs(v.y) * MathHelper.BooleanSign(setY), v.z);
    }

    public static Vector3 FlipSign(this Vector3 v, bool flipX, bool flipY)
    {
        return new Vector3((flipX) ? v.x * -1 : v.x, (flipY) ? v.y * -1 : v.y, v.z);
    }

    public static Vector3 SetXSign(this Vector3 v, bool positive)
    {
        return new Vector3(Mathf.Abs(v.x) * MathHelper.BooleanSign(positive), v.y, v.z);
    }

    public static Vector3 Abs(this Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }

    public static Vector2 Mult(this Vector2 v, Vector2 multBy)
    {
        return new Vector2(v.x * multBy.x, v.y * multBy.y);
    }

    public static T[] GetAllCompRecursive<T>(this Transform t) where T : Component
    {
        List<T> toFill = new List<T>();
        _GetAllCompRecursive<T>(t, toFill);
        return toFill.ToArray();
    }

    private static void _GetAllCompRecursive<T>(this Transform t, List<T> allComp) where T : Component
    {
        if (t.childCount > 0)
            foreach (Transform child in t)
                _GetAllCompRecursive<T>(child, allComp);
        T compToAdd = t.GetComponent<T>();
        if (compToAdd)
            allComp.Add(compToAdd);
    }

    public static Vector3 SetYSign(this Vector3 v, bool positive)
    {
        return new Vector3(v.x, Mathf.Abs(v.y) * MathHelper.BooleanSign(positive), v.z);
    }

    public static Vector2 SetXSign(this Vector2 v, bool positive)
    {
        return new Vector2(Mathf.Abs(v.x) * MathHelper.BooleanSign(positive), v.y);
    }

    public static float Random(this Vector2 v)
    {
        return UnityEngine.Random.Range(v.x, v.y);
    }

    public static bool IsBetween(this Vector2 v, float v2)
    {
        return v2 >= v.x && v2 <= v.y;
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null) throw new ArgumentNullException("source");
        if (action == null) throw new ArgumentNullException("action");

        foreach (T item in source)
            action(item);
    }

    public delegate bool conditionalDelg<T>(T element);
    //public Predicate( )

    public static List<T> GetAll<T>(this IEnumerable<T> source, Predicate<T> action)
    {
        List<T> toReturn = new List<T>();
        foreach (T t in source)
        {
            if (action.Invoke(t))
                toReturn.Add(t);
        }
        return toReturn;
    }


    public static T GetRandomElement<T>(this System.Collections.Generic.ICollection<T> iCollec)
    {
        return iCollec.ElementAt(UnityEngine.Random.Range(0, iCollec.Count));
    }

    public static T GetRandom<T>(this T[] v)
    {
        return v[UnityEngine.Random.Range(0, v.Length)];
    }

    public static List<T> RandomizeOrder<T>(this List<T> v)
    {
        var result = v.OrderBy(item => rnd.Next());
        return result.ToList();
    }

    public static JointAngleLimits2D SetLimits(this JointAngleLimits2D target, Vector2 value)
    {
        target.min = value.x;
        target.max = value.y;
        return target;
    }

    /// <summary>
    /// EX) Given a percent range of .5 and a value of 10 will return a value between 5 and 15, Given zero, it is the same value
    /// </summary>
    /// <param name="v"></param>
    /// <param name="withinPercentRange"></param>
    /// <returns></returns>
    public static float RandomizeByPercent(this float v, float withinPercentRange)
    {
        if (withinPercentRange == 0)
            return v;
        return v + UnityEngine.Random.Range(-v * withinPercentRange, v * withinPercentRange);
    }

    public static string[] CollectionToStringArray<T>(this System.Collections.Generic.ICollection<T> v)
    {
        string[] toRet = new string[v.Count];
        int i = 0; //cannot use for, for this situtation
        foreach (T elem in v)
        {
            toRet[i] = elem.ToString();
            i++;
        }
        return toRet;
    }

    public static Vector2 AngToV2(this float v)
    {
        return MathHelper.DegreeToVector2(v);
    }
    //JointLimitState2D
}
