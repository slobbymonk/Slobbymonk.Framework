using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Framework
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute { }

#if UNITY_EDITOR
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonDrawer : Editor
    {
        private readonly System.Collections.Generic.Dictionary<string, object> _paramCache = new();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var targetType = target.GetType();
            var methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            foreach (var method in methods)
            {
                if (method.GetCustomAttribute<ButtonAttribute>() == null)
                    continue;

                var parameters = method.GetParameters();
                string methodName = ObjectNames.NicifyVariableName(method.Name);

                if (parameters.Length == 0)
                {
                    if (GUILayout.Button(methodName))
                    {
                        method.Invoke(method.IsStatic ? null : target, null);
                    }
                }
                else if (parameters.Length == 1)
                {
                    var param = parameters[0];
                    string key = $"{targetType.FullName}.{method.Name}";

                    if (!_paramCache.ContainsKey(key))
                    {
                        if (param.ParameterType == typeof(int)) _paramCache[key] = 0;
                        else if (param.ParameterType == typeof(float)) _paramCache[key] = 0f;
                        else if (param.ParameterType == typeof(bool)) _paramCache[key] = false;
                    }

                    if (param.ParameterType == typeof(bool))
                    {
                        if (GUILayout.Button($"{methodName} (True)"))
                            method.Invoke(method.IsStatic ? null : target, new object[] { true });

                        if (GUILayout.Button($"{methodName} (False)"))
                            method.Invoke(method.IsStatic ? null : target, new object[] { false });
                    }
                    else if (param.ParameterType == typeof(int))
                    {
                        _paramCache[key] = EditorGUILayout.IntField($"{methodName} Param", (int)_paramCache[key]);

                        if (GUILayout.Button(methodName))
                        {
                            method.Invoke(method.IsStatic ? null : target, new object[] { (int)_paramCache[key] });
                        }
                    }
                    else if (param.ParameterType == typeof(float))
                    {
                        _paramCache[key] = EditorGUILayout.FloatField($"{methodName} Param", (float)_paramCache[key]);

                        if (GUILayout.Button(methodName))
                        {
                            method.Invoke(method.IsStatic ? null : target, new object[] { (float)_paramCache[key] });
                        }
                    }
                }
            }
        }
    }
#endif
}