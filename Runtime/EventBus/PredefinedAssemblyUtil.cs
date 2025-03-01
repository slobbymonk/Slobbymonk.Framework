using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// This class is responsible for retrieving all types that implement a given interface from Unity's predefined assemblies
/// (Assembly-CSharp, Assembly-CSharp-FirstPass, etc.).
/// Static class: It only contains utility methods and is not meant to be instantiated.
/// </summary>
public static class PredefinedAssemblyUtil
{
    /// <summary>
    /// Assembly-CSharp: The main runtime assembly where most game logic exists.
    /// Assembly-CSharp-Editor: Used for Unity Editor scripts.
    /// Assembly-CSharp-FirstPass: Used for compiled plugins and external dependencies.
    enum AssemblyType
    {
        AssemblyCSharp,
        AssemblyCSharpEditor,
        AssemblyCSharpEditorEditorFirstPass,
        AssemblyCSharpFirstPass
    }

    /// <summary>
    /// Maps an assembly name to its corresponding AssemblyType enum.
    /// Uses a nullable enum (AssemblyType?) to return null for unknown assemblies.
    static AssemblyType? GetAssemblyType(string assemblyName)
    {
        return assemblyName switch
        {
            "Assembly-CSharp" => AssemblyType.AssemblyCSharp,
            "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
            "Assembly-CSharp-Editor-EditorFirstPass" => AssemblyType.AssemblyCSharpEditorEditorFirstPass,
            "Assembly-CSharp-FirstPass" => AssemblyType.AssemblyCSharpFirstPass,
            _ => null
        };
    }

    /// <summary>
    /// Adds types that implement a specific interface to a given list.
    /// Why? This helps us collect all message types that implement IMessage.
    static void AddTypesFromAssembly(Type[] assembly, ICollection<Type> types, Type interfaceType)
    {
        if(assembly == null)
        {
            return;
        }

        for (int i = 0; i < assembly.Length; i++)
        {
            Type type  = assembly[i];
            if(type != interfaceType && interfaceType.IsAssignableFrom(type))
            {
                types.Add(type);
            }
        }
    }

    /// <summary>
    /// Finds all types that implement a given interface from known Unity assemblies.
    /// Why? This is used to find all messages(IMessage implementations) at runtime.
    public static List<Type> GetTypes(Type interfaceType)
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        Dictionary<AssemblyType, Type[]> assemblyTypes = new Dictionary<AssemblyType, Type[]>();
        List<Type> types = new List<Type>();
        for (int i = 0; i < assemblies.Length; i++)
        {
            AssemblyType? assemblyType = GetAssemblyType(assemblies[i].GetName().Name);
            if(assemblyType != null)
            {
                assemblyTypes.Add((AssemblyType) assemblyType, assemblies[i].GetTypes());
            }
        }

        if (assemblyTypes.ContainsKey(AssemblyType.AssemblyCSharp))
        {
            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharp], types, interfaceType);
        }
        if (assemblyTypes.ContainsKey(AssemblyType.AssemblyCSharpFirstPass))
        {
            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharpFirstPass], types, interfaceType);
        }

        return types;
    }
}