using TMPro;
using UnityEngine;

namespace Framework
{
    public class BuildDisplay : MonoBehaviour
    {
        public TextMeshProUGUI buildText;
        public BuildInfo buildInfo;

        void Start()
        {
            if (buildText != null && buildInfo != null)
            {
                buildText.text = $"Version {buildInfo.version} (Build {buildInfo.buildNumber})";
            }
        }
    }
}
