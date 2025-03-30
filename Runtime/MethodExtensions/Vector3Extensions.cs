using UnityEngine;

public static class Vector3Extensions
{
    /// <summary>
    /// Offsets the vector by the given values.
    /// Just write axis:value, for example: Offset(x: 5, z: 10)
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 Offset(this Vector3 vector, float x = 0, float y = 0, float z = 0)
    {
        return new Vector3(x: vector.x + x, y: vector.y + y, z: vector.z + z);
    }
    /// <summary>
    /// Allows you to set specific vector values without changing the original vector.
    /// Just write axis:value, for example: With(x: 5, z: 10)
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
    }
}
