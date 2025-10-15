using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework
{
    public static class TemporaryDisabler
    {
        public static void DisableTemporarily(MonoBehaviour component, float disableDuration)
        {
            _= HandleTemporaryDisabling(component, disableDuration);
        }

        async static Task HandleTemporaryDisabling(MonoBehaviour component, float disableDuration)
        {
            component.enabled = false;
            await UniTask.WaitForSeconds(disableDuration);
            component.enabled = true;
        }

        public static void DisableTemporarily(GameObject component, float disableDuration)
        {
            _ = HandleTemporaryDisabling(component, disableDuration);
        }

        async static Task HandleTemporaryDisabling(GameObject component, float disableDuration)
        {
            component.SetActive(false);
            await UniTask.WaitForSeconds(disableDuration);
            component.SetActive(true);
        }
    }
    public static class TemporaryEnabler
    {
        public static void EnableTemporarily(MonoBehaviour component, float disableDuration)
        {
            _ = HandleTemporaryEnabling(component, disableDuration);
        }

        async static Task HandleTemporaryEnabling(MonoBehaviour component, float disableDuration)
        {
            component.enabled = true;
            await UniTask.WaitForSeconds(disableDuration);
            component.enabled = false;
        }

        public static void EnableTemporarily(GameObject component, float disableDuration)
        {
            _ = HandleTemporaryEnabling(component, disableDuration);
        }

        async static Task HandleTemporaryEnabling(GameObject component, float disableDuration)
        {
            component.SetActive(true);
            await UniTask.WaitForSeconds(disableDuration);
            component.SetActive(false);
        }
    }
}
