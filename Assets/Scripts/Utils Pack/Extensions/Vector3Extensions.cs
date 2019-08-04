using UnityEngine;

namespace UtilsExtensions
{
    public static class Vector3Extensions
    {
        public static Vector2 Xz2xy(this Vector3 v) { return new Vector2(v.x, v.z); }
    }
}
