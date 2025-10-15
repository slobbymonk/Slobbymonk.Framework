using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Still work in progress, but since I don't need it right now I'm gonna work on this later.
public static class DependencyInjector
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InjectDependencies<T>(T dependency)
    {
        IEnumerable<IDependencyInjection<T>> scriptsUsingDependency = GameObject.FindObjectsOfType<MonoBehaviour>()
                     .OfType<IDependencyInjection<T>>();
        
        if(scriptsUsingDependency == null)
        {
            return;
        }
        
        foreach (var injectable in scriptsUsingDependency)
        {
            injectable.InjectDependency(dependency);
        }
    }
}

public interface IDependencyInjection<T>
{
    void InjectDependency(T dependency);
}