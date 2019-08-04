using UnityEngine;

namespace UtilsExtensions
{
    public static class Vector2Extensions
    {
        public static Vector3 Xy2xz(this Vector2 v) { return new Vector3(v.x, 0, v.y); }

        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }
    }
}
