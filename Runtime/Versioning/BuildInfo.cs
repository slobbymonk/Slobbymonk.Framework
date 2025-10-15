using UnityEngine;

namespace Framework
{

    [CreateAssetMenu(fileName = "BuildInfo", menuName = "Build/Build Info")]
    public class BuildInfo : ScriptableObject
    {
        public int buildNumber = 0;
        public string version = "1.0.0";
    }

}
