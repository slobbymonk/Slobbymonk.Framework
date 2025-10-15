using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T child) where T : Component
    {
      child = gameObject.GetComponentInChildren<T>();

      return child != null;
    }
    public static GameObject FindChildByName(this GameObject gameObject, string name)
    {
        return gameObject.transform.Find(name)?.gameObject;
    }
    public static void ToggleActive(this GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    /// <summary>
    /// Tries to get a component, if the component is not found it adds it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }
    public static GameObject FindDeepChildByName(this GameObject parent, string name)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.name == name)
                return child.gameObject;

            var result = child.gameObject.FindDeepChildByName(name);
            if (result != null)
                return result;
        }
        return null;
    }
    public static Transform[] ToTransform(this GameObject[] objects)
    {
        Transform[] transforms = new Transform[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            transforms[i] = objects[i].transform;
        }

        return transforms;
    }
}
public static class ArrayExtensions
{
    public static int GetIndexOf<T>(this T[] array, T item)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (EqualityComparer<T>.Default.Equals(array[i], item))
            {
                return i;
            }
        }
        return -1;
    }
}