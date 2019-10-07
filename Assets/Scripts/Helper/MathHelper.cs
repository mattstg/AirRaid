using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MathHelper : MonoBehaviour
{
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
    public static Vector2 RadianToVector2(float radian, float length)
    {
        return RadianToVector2(radian) * length;
    }
    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
    public static Vector2 DegreeToVector2(float degree, float length)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad) * length;
    }

    public static Vector2 DegreeToVector2Normalized(float degree)
    {
        return (RadianToVector2(degree * Mathf.Deg2Rad) * 0.5f) + new Vector2(0.5f, 0.5f);
    }
    //Returns true/false if found intercept, use out to get value -- This seems pricy
    public static bool CalculateIntercept(Vector2 src, Vector2 dst, float v, Vector2 dstVelo, out Vector2 interceptPt)
    {
        float tx = dst.x - src.x;
        float ty = dst.y - src.y;
        float tvx = dstVelo.x;
        float tvy = dstVelo.y;

        // Get quadratic equation components
        var a = tvx * tvx + tvy * tvy - v * v;
        var b = 2 * (tvx * tx + tvy * ty);
        var c = tx * tx + ty * ty;

        // Solve quadratic
        Vector2 sol;

        if (quad(a, b, c, out sol))
        {
            var t0 = sol[0];
            var t1 = sol[1];
            var t = Mathf.Min(t0, t1);
            if (t < 0) t = Mathf.Max(t0, t1);
            if (t > 0)
            {
                sol = new Vector2(dst.x + dstVelo.x * t, dst.y + dstVelo.y * t);
                interceptPt = sol;
                return true; //found intercept
            }
        }
        interceptPt = sol;
        return false; //failed no intercept
    }


    /**
     * Return solutions for quadratic
     */
    static bool quad(float a, float b, float c, out Vector2 result)
    {
        Vector2 sol = new Vector2();
        if (Mathf.Abs(a) < 1e-6)
        {
            if (Mathf.Abs(b) < 1e-6)
            {
                if (Mathf.Abs(c) < 1e-6)
                    sol = new Vector2(0, 0);
                else
                {
                    sol = new Vector2(0, 0);
                    //Debug.LogError("This was spouse to make the interception null?");
                    result = sol;
                    return false;
                }
            }
            else
            {
                sol = new Vector2(-c / b, -c / b);
            }
        }
        else
        {
            var disc = b * b - 4 * a * c;
            if (disc >= 0)
            {
                disc = Mathf.Sqrt(disc);
                a = 2 * a;
                sol = new Vector2((-b - disc) / a, (-b + disc) / a);
            }
        }
        result = sol;
        return true;
    }

    public static Color RGB255A1(float r, float g, float b, float a = 1f)
    {
        return new Color(r / 255f, g / 255f, b / 255f, a);
    }

    public static float SigmoidVelocity(float progress)
    {
        if (progress >= 0f && progress <= 1f)
            return -4f * Mathf.Abs(progress - 0.5f) + 2f;
        else
            return 0f;
    }

    public static float Sigmoid(float progress)
    {
        if (progress <= 0f)
            return 0f;
        else if (progress >= 1f)
            return 1f;
        else if (progress <= 0.5f)
            return progress * SigmoidVelocity(progress) / 2f;
        else
            return 0.5f + ((progress - 0.5f) * (SigmoidVelocity(progress) + 2f) / 2f);
    }

    public static float BounceFactor(float progress)
    {
        progress = Mathf.Clamp(progress, 0, 1);
        return 4 * progress - 4 * Mathf.Pow(progress, 2);
    }

    public static float WaveFactor(float progress)
    {
        return -0.5f * Mathf.Cos(2f * Mathf.PI * progress) + 0.5f;
    }

    public static float ZoomBounceIn(float progress, float peakScale = 1.2f)
    {
        float k = Mathf.Max(peakScale, 1.01f);
        float h = 1f - ((k - 1f) * (Mathf.Sqrt((1f / (k - 1f)) + 1f) - 1f));
        float a = (1f - k) / Mathf.Pow((1f - h), 2f);
        if (progress <= 0f)
            return 0f;
        else if (progress >= 1f)
            return 1f;
        else if (progress >= h)
            return Mathf.Clamp(((1f - k) / 2f) * Mathf.Cos((Mathf.PI * (progress - 1f)) / (1f - h)) + ((1f + k) / 2f), 0f, k);
        else
            return Mathf.Clamp(a * Mathf.Pow(progress - h, 2f) + k, 0f, k);
    }

    public static float ZoomBounceOut(float progress, float peakScale = 1.2f)
    {
        float k = Mathf.Max(peakScale, 1.01f);
        float h = (k - 1f) * (Mathf.Sqrt((1f / (k - 1f)) + 1f) - 1f);
        float a = (1f - k) / Mathf.Pow(h, 2f);
        if (progress <= 0f)
            return 1f;
        else if (progress >= 1f)
            return 0f;
        else if (progress <= h)
            return Mathf.Clamp(((1f - k) / 2f) * Mathf.Cos((Mathf.PI * progress) / h) + ((1f + k) / 2f), 0f, k);
        else
            return Mathf.Clamp(a * Mathf.Pow(progress - h, 2f) + k, 0f, k);
    }

    public static T[,] Make2DArray<T>(T[] input, int height, int width)
    {
        T[,] output = new T[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                output[i, j] = input[i * width + j];
            }
        }
        return output;
    }

   

    public static List<string> GetStringArrOfEnums<T>() where T : struct, System.IConvertible
    {
        return ConvertArrToString<T>(GetArrOfEnums<T>()).ToList();
    }

    static Dictionary<System.Type, System.Enum[]> bakedEnumDict = new Dictionary<System.Type, System.Enum[]>();
    public static T[] GetArrOfEnums<T>() where T : struct, System.IConvertible
    {
        //Not sure if micro opt, but bakes the enums in a dict first time retrieval, since we retreieve some of them alot of times
        if (!bakedEnumDict.ContainsKey(typeof(T)))
            bakedEnumDict.Add(typeof(T), System.Enum.GetValues(typeof(T)).Cast<System.Enum>().ToArray<System.Enum>());

        return bakedEnumDict[typeof(T)].Cast<T>().ToArray<T>();
    }

    public static string[] ConvertArrToString<T>(T[] arr)
    {
        string[] toRet = new string[arr.Length];
        for (int i = 0; i < arr.Length; i++)
            toRet[i] = arr[i].ToString();
        return toRet;
    }


    public static Vector2 RotateV2(Vector2 v, float deg)
    {
        float sin = Mathf.Sin(deg * Mathf.Deg2Rad);
        float cos = Mathf.Cos(deg * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public static Vector3 RotateV3(Vector3 v, float deg)
    {
        float sin = Mathf.Sin(deg * Mathf.Deg2Rad);
        float cos = Mathf.Cos(deg * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        v.z = 0;
        return v;
    }

    public static void SetAlpha(Color color, float alpha)
    {
        color = new Color(color.r, color.g, color.b, alpha);
    }

    public static float SmoothVelocity(float progress)
    {
        if (progress >= 0 && progress <= 1)
            return -4 * Mathf.Abs(progress - 0.5f) + 2;
        else
            return 0;
    }

    public static float SmoothIntegral(float progress)
    {
        if (progress < 0)
            return 0;
        else if (progress > 1)
            return 1;
        else if (progress <= 0.5)
            return progress * SmoothVelocity(progress) / 2;
        else
            return 0.5f + ((progress - 0.5f) * (SmoothVelocity(progress) + 2) / 2);
    }

    public static ContactPoint2D[] ExtractContactPoints(Collision2D coli, int maxArrayCount = 10)
    {
        ContactPoint2D[] contactPoints = new ContactPoint2D[maxArrayCount];
        int num = coli.GetContacts(contactPoints);
        ContactPoint2D[] toRet = new ContactPoint2D[num];
        System.Array.Copy(contactPoints, toRet, num);
        //if (num == 0)
        //    Debug.LogError("Error! Zero contact points detected!");
        return toRet;

        //IF ARRAY IS ZERO COUNT, CHECK THE COLLIDERS PERSONNALLY FOR COLISION POINTS, IF THAT IS EMPTY TOO, DELETE C DRIVE, INSTALL UNITY ON BIOS, RUN IN DEBUG MODE & USE TWO KEYBOARDS
    }

    public static bool IsBetween(float v, float min, float max, bool inclusive = true)
    {
        if (!inclusive && (v == min || v == max))
            return false;
        else
            return (v >= min && v <= max);
    }

    /// <summary>
    /// Returns 1 or -1 (True or False)
    /// </summary>
    public static int BooleanSign(bool b)
    {
        return (b) ? 1 : -1;
    }

    public static float Angle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

    public static float AngleAlt(Vector2 vector)
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        if (vector.y < 0f)
            angle += 360f;
        return angle % 360f;
    }

    public static float UnAngleAlt(Vector2 vector)
    {
        float angle = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
        if (vector.y < 0f)
            angle += 360f;
        return angle % 360f;
    }

    public static float GaussianFactor(float t, float center = 0.5f, float width = 0.125f)
    {
        t = Mathf.Clamp01(t);
        if (Mathf.Approximately(width, 0f))
            width = 0.125f;
        return Mathf.Exp(-1f * Mathf.Pow(t - center, 2f) / (2f * Mathf.Pow(width, 2f)));
    }

    public static bool CoinFlip()
    {
        return Random.Range(0, 2) == 0;
    }

    public static int IncreaseLoopingValueWithMod(int v, int modV)
    {
        return (v + 1) % modV;
    }

    public static int gcf(int a, int b) //Greatest common factor
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static int lcm(params int[] a) //Lowest common multiple
    {
        if (a.Length == 1)
            return a[0];

        int runninglcm = a[0];
        for (int i = 1; i < a.Length; i++)
            runninglcm = (runninglcm / gcf(runninglcm, a[i])) * a[i];
        return runninglcm;
        //return (a / gcf(a, b)) * b;   //a = runninglcm  & b = a[i]
    }



    public static Quaternion FaceObject(Vector2 startingPosition, Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - startingPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static Quaternion FaceObject(Vector2 direction)
    {

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static void SetObjectRotation(Transform _fakeHand, Vector2 vecAng)
    {
        float ang = 0;
        ang = -Angle(vecAng.normalized) + 270;
        if (ang < 0)
            ang += 360;
        _fakeHand.localEulerAngles = new Vector3(0, 0, ang);
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    public static Quaternion EulerZToQuat(float z)
    {
        return EulerToQuat(new Vector3(0, 0, z));
    }

    public static Quaternion EulerToQuat(Vector3 toQuat)
    {
        Quaternion temp = Quaternion.identity;
        temp.eulerAngles = new Vector3(toQuat.x, toQuat.y, toQuat.z);
        return temp;
    }

    public static T GetRandomElemFromArr<T>(T[] arr, bool allowNull = false)
    {
        //In a perfect world, the array does not have null elements. We do not live in that world. Tool safety with error logging
        if (arr == null || arr.Length == 0)
        {
            Debug.LogError("Error, GetRandomElemFromArr was passed a null or empty array, returning a default t");
            return (T)System.Activator.CreateInstance(typeof(T));  //This creates a default instance, for example, if an int, 0, if a class, that classes default constructor, this is not gaureteened to work, this is an attempted catch
        }

        int length = arr.Length;
        int randIndex = UnityEngine.Random.Range(0, length);
        if (allowNull || arr[randIndex] != null)
            return arr[randIndex];
        else
        {
            for (int attempts = 0; attempts < 100; attempts++)
            {
                T toRet = arr[randIndex];
                if (toRet != null)
                    return toRet;
                else
                    randIndex = UnityEngine.Random.Range(0, length);
            }
            Debug.LogError("Get random elem from array could not randomly find a non-null element after many attempts, returning the first element, which may be null");
            return arr[0];
        }
    }

    public static float SurfaceAreaOfMesh(Mesh mesh)
    {

        float xSum = 0;
        float ySum = 0;
        int numOfVertices = mesh.vertices.Length;
        if (numOfVertices <= 0)
            Debug.LogError("whoa there, less/equal than 0 vertices");

        for (int i = 0; i < numOfVertices - 1; i++)
        {
            xSum += mesh.vertices[i].x * mesh.vertices[i + 1].y;
            ySum += mesh.vertices[i].y * mesh.vertices[i + 1].x;
        }

        xSum += mesh.vertices[numOfVertices - 1].x * mesh.vertices[0].y;
        ySum += mesh.vertices[numOfVertices - 1].y * mesh.vertices[0].x;

        return Mathf.Abs((xSum - ySum) / 2);
    }

    public static float ApproxDistance(Vector2 p1, Vector2 p2) //not that good
    {
        float x = p2.x - p1.x;
        float y = p2.y - p1.y;
        return 1.426776695f * Mathf.Min(0.7071067812f * (Mathf.Abs(x) + Mathf.Abs(y)), Mathf.Max(Mathf.Abs(x), Mathf.Abs(y)));
    }

    public static Color BuildRandomSaturatedColour()
    {
        //fully saturated colours always have one maxed channel, one min channel, and one channel with any value
        float[] channels = new float[3]; //indices 0, 1, 2, correspond to r, g, b respectively

        int maxSatIndex = Random.Range(0, 3);  //this next code block determines which channels are min, max, and random
        int minSatIndex = Random.Range(0, 3);
        while (minSatIndex == maxSatIndex)
            minSatIndex = Random.Range(0, 3);
        int randSatIndex = Random.Range(0, 3);
        while (randSatIndex == maxSatIndex || randSatIndex == minSatIndex)
            randSatIndex = Random.Range(0, 3);

        float randSat = Random.Range(0f, 1f); //value of the random channel

        channels[maxSatIndex] = 1; //this block populates the array according to the determined indices
        channels[minSatIndex] = 0;
        channels[randSatIndex] = randSat;

        Color color = new Color(channels[0], channels[1], channels[2]); //builds colour from array
        return color;
    }

    public static string SpliceCamelCase(string str, string separator)
    {
        string newStr = str;
        for (int i = str.Length - 1; i > 0; i--)
            if (char.IsUpper(str[i]) && !char.IsWhiteSpace(str[i - 1]) && !char.IsUpper(str[i - 1]))
                newStr = newStr.Insert(i, separator);
        return newStr;
    }

    public static string RemoveCharacter(string str, string character)
    {
        return str.Replace(character, "");
    }
}
