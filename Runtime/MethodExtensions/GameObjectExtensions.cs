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
}