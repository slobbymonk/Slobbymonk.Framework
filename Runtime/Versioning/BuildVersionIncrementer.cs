using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Framework
{
    public class BuildVersionIncrementer : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            var buildInfo = AssetDatabase.LoadAssetAtPath<BuildInfo>("Assets/BuildInfo.asset");
            if (buildInfo != null)
            {
                buildInfo.buildNumber++;
                EditorUtility.SetDirty(buildInfo);
                AssetDatabase.SaveAssets();

                Debug.Log($"Incremented build number to {buildInfo.buildNumber}");
            }
            else
            {
                Debug.LogWarning("BuildInfo.asset not found. Please create one in Assets/");
            }
        }
    }
}
