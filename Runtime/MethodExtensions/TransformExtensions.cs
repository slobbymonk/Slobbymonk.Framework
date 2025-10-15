using UnityEngine;

namespace Framework
{
    public static class TransformExtensions
    {
        public static bool IsAParent(this Transform parent, Transform child)
        {
            if (parent == null || child == null) return false;

            Transform current = child;
            while (current != null)
            {
                if (current == parent)
                {
                    return true;
                }
                current = current.parent;
            }
            return false;
        }
    }
}
